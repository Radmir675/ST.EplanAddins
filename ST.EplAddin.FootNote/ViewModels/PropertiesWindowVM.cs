using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace ST.EplAddin.FootNote.ViewModels
{
    internal class PropertiesWindowVM
    {
        public string Title { get; set; } = "Условный объект";
        private void ShowCharacterMap()
        {
            try
            {
                Process.Start("charmap.exe");
            }
            catch (Win32Exception e)
            {
                MessageBox.Show("Таблица символов не установлена!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
