using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TheMeal.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IQueryAttributable // For navigation parameters
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(); // Notify UI of the change
                }
            }
        }

        // Navigation helper (You might need to adjust based on your app structure)
        protected async Task GoToAsync(string route, Dictionary<string, object> parameters = null)
        {
            var query = new Dictionary<string, object>();
            if (parameters != null)
            {
                foreach (var pair in parameters)
                {
                    query[pair.Key] = pair.Value?.ToString();
                }
            }

            await Shell.Current.GoToAsync(route, query);
        }

        // IQueryAttributable Implementation (To receive parameters from navigation)
        public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

