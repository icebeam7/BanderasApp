using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BanderasApp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set { isBusy = value; OnPropertyChanged(); }
        }
    }
}
