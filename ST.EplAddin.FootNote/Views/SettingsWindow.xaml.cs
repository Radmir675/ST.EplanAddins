using System.Windows;
using System.Windows.Input;

namespace ST.EplAddin.FootNote.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

        }

        #region WindowButtons
        private void Minimize_Program(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Program(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowNormalMaximize()
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    MaximizeProgram.Content = "🗖";
                    WindowState = WindowState.Normal;
                    TitleDrawBar.CornerRadius = new CornerRadius(6, 6, 0, 0);
                    break;
                case WindowState.Normal:
                    MaximizeProgram.Content = "🗗";
                    WindowState = WindowState.Maximized;
                    //  TitleDrawBar.CornerRadius = new CornerRadius(0);
                    break;
            }
        }

        private void Maximize_Program(object sender, RoutedEventArgs e)
        {
            WindowNormalMaximize();
        }

        private void DrawWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            switch (e.ClickCount)
            {
                case 2:
                    WindowNormalMaximize();
                    break;
            }
        }
        #endregion
    }
}

