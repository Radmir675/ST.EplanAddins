using ST.EplAddin.ComparisonOfProjectProperties.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ST.EplAddin.ComparisonOfProjectProperties.Views
{
    public partial class MainWindow : Window
    {
        private readonly ChangesRecord changesRecord;
        public MainWindow()
        {
            InitializeComponent();
            changesRecord = new ChangesRecord();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => Keyboard.ClearFocus();

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void RightSync_Button_KeyUp(object sender, KeyEventArgs e)
        {
            var changedItems = changesRecord.GetChangesList();
            if (changedItems.Any())
            {
                foreach (var item in changedItems)
                {
                    var data = SystemTwo_ListView.ItemsSource.Cast<KeyValuePair<int, PropertyData>>();

                }
            }

        }
    }
}
