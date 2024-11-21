using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.FootNote.Views;

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
