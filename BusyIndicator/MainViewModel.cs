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
}
