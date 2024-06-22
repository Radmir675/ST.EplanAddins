using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.Footnote.ProperyBrowser;

namespace UnitTestProjectFootnote
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod11()
        {
            PropertySelectDialogForm propertySelectDialogForm = new PropertySelectDialogForm();
            propertySelectDialogForm.ShowDialog();
        }
    }
}
