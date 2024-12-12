using System.Windows;

namespace ST.EplAddin.UserConfigurationService.Views
{
    /// <summary>
    /// Interaction logic for SchemesView.xaml
    /// </summary>
    public partial class SchemesView : Window
    {
        public SchemesView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
