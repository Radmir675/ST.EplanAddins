using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.CheckCableAccesorities.Help;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            //WPF_Form form = new WPF_Form();
            //form.Show();
            ExcelHelper excelHelper = new ExcelHelper();
            var data = excelHelper.GetData();
        }
    }
}
