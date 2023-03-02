using System.Windows.Controls;
using KMA.Lab01Kucherenko.ViewModels;

namespace KMA.Lab01Kucherenko.Views
{
    /// <summary>
    /// Interaction logic for ZodiacView.xaml
    /// </summary>
    public partial class ZodiacView : UserControl
    {
        private ZodiacViewModel _viewModel;
        public ZodiacView()
        {
            InitializeComponent();
            DataContext = _viewModel = new ZodiacViewModel();
        }
    }
}
