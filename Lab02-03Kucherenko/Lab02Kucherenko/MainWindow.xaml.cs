using KMA.Lab02.Kucherenko.Views;
using System.Windows;

namespace KMA.Lab02.Kucherenko
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GoToFormView();
        }

        public void GoToResultView()
        {
            Content = new ResultView(GoToFormView);
        }

        public void GoToFormView()
        {
            Content = new FormView(GoToResultView);
        }
    }
}