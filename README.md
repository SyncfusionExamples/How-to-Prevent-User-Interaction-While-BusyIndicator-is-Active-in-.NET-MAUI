This article guides you on how to prevent user interactions on a page within a .NET MAUI app when the [.NET MAUI BusyIndicator](https://www.syncfusion.com/maui-controls/maui-busy-indicator) is active.

To prevent user interactions while the `BusyIndicator` is active, you can set the [InputTransparent](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.visualelement.inputtransparent?view=net-maui-9.0) property of the main content's  parent layout based on the [IsRunning](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Core.SfBusyIndicator.html#Syncfusion_Maui_Core_SfBusyIndicator_IsRunning) property of the BusyIndicator. The `InputTransparent` property ensures that user input is ignored when the BusyIndicator is running.

**Model**

```
public class Model
{
    public string? Title {  get; set; }
    public string? Description {  get; set; }
}
```

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

    public ObservableCollection<Model>? NewsHeadlines { get; set; }
    public ICommand? RefreshNewsCommand { get; }

    public MainViewModel()
    {
        NewsHeadlines = new ObservableCollection<Model>
        {
            new Model(){ Title="Breaking: ", Description="Market hits all-time high"},
            new Model(){ Title="Local Sports: ", Description="Team wins championship"},
            new Model(){ Title="Weather Update: ", Description="Storm approaching"},
            new Model(){ Title="Technology: ", Description="New smartphone released"},
        };
        RefreshNewsCommand = new Command(async () => await RefreshNewsAsync());
    }

    private async Task RefreshNewsAsync()
    {
        IsBusy = true;
        await Task.Delay(2000); 
        NewsHeadlines?.Add(new Model() { Title = "Health: ", Description = "New fitness trends for 2025" });
        NewsHeadlines?.Add(new Model() { Title = "Finance: ", Description = "Stocks see major surge" });
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
    <Grid x:Name="grid" RowDefinitions="Auto,100" HorizontalOptions="Center" InputTransparent="{Binding IsBusy}" >
        <Border WidthRequest="300" Padding="10">
            <Border.StrokeShape>
                <RoundRectangle CornerRadius="6"/>
            </Border.StrokeShape>
            <ListView ItemsSource="{Binding NewsHeadlines}" HorizontalOptions="Center" RowHeight="30">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <HorizontalStackLayout Spacing="10">
                                <Label Text="{Binding Title}" FontAttributes="Bold" HorizontalOptions="Center" />
                                <Label Text="{Binding Description}" HorizontalOptions="Center" />
                            </HorizontalStackLayout>
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

**Output**

![BusyIndicator_Interaction.gif](https://support.syncfusion.com/kb/agent/attachment/article/19037/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjM2NDMyIiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.kBAOZgZcXZju2XyyNWo-LJGgzsLZneV4Iq_HNP88Iro)
