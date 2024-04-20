using Eplan.EplApi.Base;
using System.Collections.Generic;

namespace ST.EplAddin.PlcEdit
{
    public class EplanSettings
    {
        private Settings settings { get; set; }
        public EplanSettings()
        {
            settings = new Settings();
        }
        public List<string> TryGetSelectedColumns()
        {
            List<string> result = new List<string>();
            if (settings.ExistSetting("columnNames.p"))
            {
                for (int i = 0; i < settings.GetCountOfValues("columnNames.p"); i++)
                {
                    result.Add(settings?.GetStringSetting("columnNames.p", i));
                }
            }
            return result;
        }
        public void SetCheckedColumns(List<string> columnsName)
        {
            settings.AddStringSetting("columnNames.p", columnsName.ToArray(), new string[columnsName.Count], ISettings.CreationFlag.Overwrite);
        }
    }
}
