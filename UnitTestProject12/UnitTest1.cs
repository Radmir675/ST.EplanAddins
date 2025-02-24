using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.FootNote.ViewModels;
using ST.EplAddin.FootNote.Views;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest1
    {


        //[TestMethod]
        //public void StartMainPropertyWindow()
        //{
        //    new System.Windows.Application();
        //    var app = new App();
        //    app.StartWindow();


        //}

        [TestMethod]
        public void StartPropertyWindow()
        {
            FullPropertiesWindow fullPropertiesWindow = new FullPropertiesWindow();

            fullPropertiesWindow.DataContext = new FullPropertiesWindowVM();
            fullPropertiesWindow.ShowDialog();


        }
    }
}
