using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.CheckCableAccesorities;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void StartForm()
        {
            MainWindow form = new MainWindow();
            form.ShowDialog();

        }
    }
}
