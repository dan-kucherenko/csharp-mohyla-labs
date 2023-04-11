using KMA.Lab04.Kucherenko.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KMA.Lab04.Kucherenko.ViewModels
{
    internal enum NavigationViewTypes
    {
        DataViewModel,
        FormViewModel,
        UpdateFormViewModel
    }

    internal class NavigationViewModel : INotifyPropertyChanged
    {
        private INavigatable _currentViewModel;

        public NavigationViewModel()
        {
            Navigate(NavigationViewTypes.DataViewModel);
        }

        public INavigatable CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                NotifyPropertyChanged();
            }
        }

        #region Navigation Methods

        internal void Navigate(NavigationViewTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.ViewType == type)
                return;
            INavigatable viewModel = GetVM(type);
            if (viewModel == null)
                return;
            CurrentViewModel = viewModel;
        }

        private INavigatable GetVM(NavigationViewTypes type)
        {
            INavigatable viewModel;
            switch (type)
            {
                case NavigationViewTypes.DataViewModel:
                    viewModel = new DataViewModel(()=> Navigate(NavigationViewTypes.FormViewModel),
                        () => Navigate(NavigationViewTypes.UpdateFormViewModel));
                    break;
                case NavigationViewTypes.FormViewModel:
                    viewModel = new FormViewModel(() => Navigate(NavigationViewTypes.DataViewModel));
                    break;
                case NavigationViewTypes.UpdateFormViewModel:
                    viewModel = new UpdateFormViewModel(() => Navigate(NavigationViewTypes.DataViewModel));
                    break;
                default:
                    return null;
            }
            return viewModel;
        }

        #endregion
        
        #region PropChangedImplementation

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}