using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST.EplAddin.UserConfigurationService.Models;
using ST.EplAddin.UserConfigurationService.Storage;

namespace UnitTestProject12
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void StartForm()
        {

            var instance = ConfigurationStorage.Instance();
            var d = instance.GetAll();
            instance.Remove("jjkl");
            var instance1 = ConfigurationStorage.Instance();
            instance1.Save(new Scheme() { Name = "S111" });

        }
    }
}
