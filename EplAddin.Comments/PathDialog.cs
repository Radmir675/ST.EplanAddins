using System.IO;
using System.Windows.Forms;

namespace ST.EplAddin.Comments
{
    internal class PathDialog
    {
        public static string TryGetSavePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.xml) | *.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                return null;
            // получаем путь выбранного файла
            string filename = saveFileDialog.FileName;
            return filename;
        }
        public static string TryGetReadPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.xml) | *.xml";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return null;
            // получаем путь выбранного файла
            string filename = openFileDialog.FileName;
            return filename;
        }
        public static string TryGetFileName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        public static string TryGetFileNameWithType(string path)
        {
            return Path.GetFileName(path);
        }
    }
}
