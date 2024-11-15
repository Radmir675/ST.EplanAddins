using System.Windows;
using System.Windows.Input;

namespace ST.EplAddin.ComparisonOfProjectProperties.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => Keyboard.ClearFocus();

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
