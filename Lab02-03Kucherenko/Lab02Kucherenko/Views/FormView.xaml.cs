using System;
using KMA.Lab02.Kucherenko.ViewModels;
using System.Windows.Controls;

namespace KMA.Lab02.Kucherenko.Views
{
    /// <summary>
    /// Interaction logic for FormView.xaml
    /// </summary>
    public partial class FormView : UserControl
    {
        private FormViewModel _viewModel;

        public FormView(Action gotoResultView)
        {
            InitializeComponent();
            DataContext = _viewModel = new FormViewModel(gotoResultView);
        }
    }
}