using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    internal static class JsonProvider<T>
    {
        public static void SaveData(ObservableCollection<T> data)
        {
            string serializedObject = JsonConvert.SerializeObject(data);
            Properties.Settings.Default.SavedSettings = serializedObject;
            Properties.Settings.Default.Save();
        }
        public static T GetData()
        {
            var deSerializedObject = JsonConvert.DeserializeObject<T>(Properties.Settings.Default.SavedSettings);
            return deSerializedObject;
        }
    }
}
