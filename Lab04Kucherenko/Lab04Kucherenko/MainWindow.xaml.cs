using System.Windows;
using KMA.Lab04.Kucherenko.Views;

namespace KMA.Lab04.Kucherenko
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Content = new NavigationView();
        }
    }
}
