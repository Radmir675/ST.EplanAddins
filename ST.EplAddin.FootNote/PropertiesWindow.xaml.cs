using SVGImage.SVG;
using System.Windows;

namespace ST.EplAddin.Footnote.Views
{
    /// <summary>
    /// Interaction logic for PropertiesWindow.xaml
    /// </summary>
    public partial class PropertiesWindow : Window
    {

        public PropertiesWindow()
        {
            SVG sVG = new SVG();
            InitializeComponent();
        }
    }
}
