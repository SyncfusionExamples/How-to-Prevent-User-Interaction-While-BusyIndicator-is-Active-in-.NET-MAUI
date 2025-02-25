using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BusyIndicator
{
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
}
