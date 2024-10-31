using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.Footnote.Views;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var propertiesWindow = new PropertiesWindow();
            propertiesWindow.ShowDialog();

        }
    }
}
