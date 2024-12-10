using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ST.EplAddin.UserConfigurationService.Services
{
    internal static class JsonProvider<T>
    {
        public static void SaveData(ObservableCollection<T> data)
        {
            string serializedObject = JsonConvert.SerializeObject(data);
            Properties.Settings.Default.Configurations = serializedObject;
            Properties.Settings.Default.Save();
        }
        public static T GetData()
        {
            var deSerializedObject = JsonConvert.DeserializeObject<T>(Properties.Settings.Default.Configurations);
            return deSerializedObject;
        }
    }
}
