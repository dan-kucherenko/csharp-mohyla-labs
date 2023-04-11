using KMA.Lab04.Kucherenko.ViewModels;
using System.Windows.Controls;

namespace KMA.Lab04.Kucherenko.Views
{
    /// <summary>
    /// Interaction logic for NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
            DataContext = new NavigationViewModel();
        }
    }
}
