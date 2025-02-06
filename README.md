This article guides you on how to prevent user interactions on a page within a .NET MAUI app when the [.NET MAUI BusyIndicator](https://www.syncfusion.com/maui-controls/maui-busy-indicator) is active.

To prevent user interactions while the `BusyIndicator` is active, you can set the [InputTransparent](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.visualelement.inputtransparent?view=net-maui-9.0) property of the main content's  parent layout based on the [IsRunning](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Core.SfBusyIndicator.html#Syncfusion_Maui_Core_SfBusyIndicator_IsRunning) property of the BusyIndicator. The `InputTransparent` property ensures that user input is ignored when the BusyIndicator is running.

**ViewModel**

```
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ObservableCollection<string>? NewsHeadlines { get; set; }
        public ICommand? RefreshNewsCommand { get; }

        public MainViewModel()
        {
            NewsHeadlines = new ObservableCollection<string>
            {
                "Breaking: Market hits all-time high",
                "Local Sports: Team wins championship",
                "Weather Update: Storm approaching",
                "Technology: New smartphone released"
            };
            RefreshNewsCommand = new Command(async () => await RefreshNewsAsync());
        }

        private async Task RefreshNewsAsync()
        {
            IsBusy = true;
            await Task.Delay(2000); 
            NewsHeadlines?.Add("Health: New fitness trends for 2025");
            NewsHeadlines?.Add("Finance: Stocks see major surge");
            IsBusy = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
```

**XAML**

```
<Grid>
    <Grid x:Name="grid" RowDefinitions="Auto,100" InputTransparent="{Binding IsBusy}" >
        <Border WidthRequest="300" Padding="10">
            <Border.StrokeShape>
                <RoundRectangle CornerRadius="6"/>
            </Border.StrokeShape>
            <ListView ItemsSource="{Binding NewsHeadlines}" RowHeight="30">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <VerticalStackLayout Spacing="10">
                                <Label Text="{Binding}" HorizontalOptions="Center" />
                            </VerticalStackLayout>
                        </ViewCell>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </Border>

        <Button Grid.Row="1" Text="Refresh News" Command="{Binding RefreshNewsCommand}"
                VerticalOptions="Center" HorizontalOptions="Center"/>
    </Grid>

    <indicator:SfBusyIndicator x:Name="indicator"
                               IsRunning="{Binding IsBusy}"
                               Title="Loading...">
        <indicator:SfBusyIndicator.OverlayFill>
            <Color x:FactoryMethod="FromRgba">
                <x:Arguments>
                    <x:Int32>211</x:Int32>
                    <x:Int32>211</x:Int32>
                    <x:Int32>211</x:Int32>
                    <x:Int32>155</x:Int32>
                </x:Arguments>
            </Color>
        </indicator:SfBusyIndicator.OverlayFill>
    </indicator:SfBusyIndicator>
</Grid>
```

By using the `InputTransparent` property in conjunction with the `IsRunning` property of the BusyIndicator, you can effectively block user interactions on a page within a .NET MAUI application when needed. This approach ensures that users have a clear indication of ongoing tasks and minimizes potential disruptions or errors from unintended user actions.

**Output**

![BusyIndicator.gif](https://support.syncfusion.com/kb/agent/attachment/article/19037/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM1NDYyIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.tengmMXFZRA3j34RtHLCrY2X0Z8oYqlByE0l7W2BgFE)
