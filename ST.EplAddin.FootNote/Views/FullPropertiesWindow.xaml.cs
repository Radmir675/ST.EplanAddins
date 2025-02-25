using System.Windows;
using System.Windows.Input;

namespace ST.EplAddin.FootNote.Views
{
    /// <summary>
    /// Interaction logic for FullPropertiesWindow.xaml
    /// </summary>
    public partial class FullPropertiesWindow : Window
    {
        public FullPropertiesWindow()
        {
            InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ButtonBase_OnClick(this, null);
        }

        private void Cleare_OnClick(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
        }
    }
}
