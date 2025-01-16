using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.FootNote.Views;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod()
        {
            new System.Windows.Application();
            //var app = new App();
            new FullPropertiesWindow().ShowDialog();
        }
    }
}
