using System.IO;
using System.Reflection;
using System.Windows;

namespace ST.EplAddin.FootNote.Views
{
    /// <summary>
    /// Interaction logic for FullPropertiesWindow.xaml
    /// </summary>
    public partial class FullPropertiesWindow : Window
    {
        public FullPropertiesWindow()
        {
            Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MaterialDesignThemes.Wpf.dll"));
            Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "MaterialDesignColors.dll"));
            InitializeComponent();
        }

        private void ListBoxItem_OnSelected(object sender, RoutedEventArgs e)
        {

        }
    }
}
