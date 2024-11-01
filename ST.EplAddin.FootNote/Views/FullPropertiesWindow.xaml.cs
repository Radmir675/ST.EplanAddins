using SVGImage.SVG;
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
            SVG sVG = new SVG();
            InitializeComponent();
        }
    }
}
