using KMA.Lab02.Kucherenko.ViewModels;
using System;
using System.Windows.Controls;

namespace KMA.Lab02.Kucherenko.Views
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        private ResultViewModel _viewModel;

        public ResultView(Action gotoFormView)
        {
            InitializeComponent();
            DataContext = _viewModel = new ResultViewModel(gotoFormView);
        }
    }
}