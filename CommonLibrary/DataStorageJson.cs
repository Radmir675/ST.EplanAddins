using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ST.EplAddin.CommonLibrary
{
    public class DataStorageJson<T>
    {
        private string filePathToSaveData;

        public DataStorageJson(string pathfile)
        {
            filePathToSaveData = pathfile;
        }

        public T ReadAllFromStorage()
        {
            T data1;
            if (File.Exists(filePathToSaveData) == false)
            {
                var file = File.Create(filePathToSaveData);
                file.Close();
                file.Dispose();
            }
            try
            {
                var date = File.ReadAllText(filePathToSaveData);
                data1 = JsonConvert.DeserializeObject<T>(date);
                return data1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return (T)Activator.CreateInstance(typeof(T));
            }

        }

        public static T GetObject<T>() where T : new()
        {
            return new T();
        }
        public void SaveItemToStorage(T newData)
        {
            string serializedData = JsonConvert.SerializeObject(newData, Formatting.Indented);
          

                try
                {
                    File.WriteAllText(filePathToSaveData, serializedData);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            
        }
        public void RemoveJsonFile()
        {
            File.Delete(filePathToSaveData);
        }

      







    }

}

