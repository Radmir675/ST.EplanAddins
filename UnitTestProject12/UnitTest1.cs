using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PropertySelectDialogForm propertySelectDialogForm = new PropertySelectDialogForm();
            propertySelectDialogForm.ShowDialog();
            //Form1 form1 = new Form1();
            //form1.ShowDialog();
        }
    }
}
