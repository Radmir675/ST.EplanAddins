using System.Windows;
using System.Windows.Input;

namespace ST.EplAddin.FootNote.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class MainPropertyWindow : Window
    {
        public MainPropertyWindow()
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
                    break;
            }
        }

        private void Maximize_Program(object sender, RoutedEventArgs e)
        {
            WindowNormalMaximize();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
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

