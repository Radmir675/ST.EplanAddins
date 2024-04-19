using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ST.EplAddin.PlcEdit
{
    public partial class PropertiesForm : Form
    {
        private readonly List<string> columnsName;
        private EplanSettings settings;
        public static event EventHandler<List<string>> SettingsChanged;

        public PropertiesForm(List<string> columnsName)
        {
            InitializeComponent();
            this.columnsName = columnsName;
            settings = new EplanSettings();
        }
        public PropertiesForm(List<string> columnsName, List<string> visibleColumnsName) : this(columnsName)
        {
            settings.SetCheckedColumns(visibleColumnsName);
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OK_button_Click(object sender, EventArgs e)
        {
            var checkedItems = checkedListBox.CheckedItems.Cast<string>().ToList();
            settings.SetCheckedColumns(checkedItems);
            this.Close();
            SettingsChanged?.Invoke(this, checkedItems);
        }

        private void PropertiesForm_Load(object sender, EventArgs e)
        {
            checkedListBox.Items.AddRange(columnsName.ToArray());//получаем список столбцов
            var checkedItems = settings.TryGetSelectedColumns();
            if (checkedItems.Any())
            {
                SetItemsCheck(checkedItems);//их нужно зачекать
            }
        }

        private void SetItemsCheck(List<string> checkedItems)
        {
            var itemsIncheckBoxList = checkedListBox.Items.OfType<string>();
            List<int> indexes = new List<int> { };

            foreach (var item in itemsIncheckBoxList)
            {
                if (checkedItems.Contains(item))
                {
                    var index = checkedListBox.Items.IndexOf(item);
                    indexes.Add(index);
                }
            }
            indexes.ForEach(index => checkedListBox.SetItemChecked(index, true));
        }
    }
}
