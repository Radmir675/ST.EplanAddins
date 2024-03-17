using Eplan.EplApi.Base;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ST.EplAddin.Footnote
{
    [RefreshProperties(RefreshProperties.All)]
    public class STSettings
    {
        private static readonly Lazy<STSettings> lazy = new Lazy<STSettings>(() => new STSettings());
        public static STSettings instance { get { return lazy.Value; } }

        [CategoryAttribute("Line"), Description("Толщина линии"), ReadOnlyAttribute(false), DefaultValueAttribute("")]
        public double LINEWIDTH { get; set; } = 0.18;

        [CategoryAttribute("Text"), Description("Высота текста выноски"), ReadOnlyAttribute(false), DefaultValueAttribute("")]
        public double TEXTHEIGHT { get; set; } = 2.5;

        [CategoryAttribute("Text"), Description("Простой курсор для размещения элемента"), ReadOnlyAttribute(false), DefaultValueAttribute("")]
        public bool LINECURSOR { get; set; } = true;

        [CategoryAttribute("Text"), Description("Индекс размещаемого свойства"), ReadOnlyAttribute(false), DefaultValueAttribute("")]
        public PropertiesList PROPERTYID { get; set; } = PropertiesList.P20450;

        [CategoryAttribute("Line"), Description("Кружок на конце линии"), ReadOnlyAttribute(false), DefaultValueAttribute("")]
        public bool STARTPOINT { get; set; } = true;

        [CategoryAttribute("Line"), Description("Радиус кружока на конце линии"), ReadOnlyAttribute(false), DefaultValueAttribute("")]
        public double STARTPOINTRADIUS { get; set; } = 0.4;

        static String KEY_LINEWIDTH = "USER.ST.FOOTNOTE.LINEWIDTH";
        static String KEY_TEXTHEIGHT = "USER.ST.FOOTNOTE.TEXTHEIGHT";
        static String KEY_LINECURSOR = "USER.ST.FOOTNOTE.LINECURSOR";
        static String KEY_PROPERTYID = "USER.ST.FOOTNOTE.PROPERTYID";
        static String KEY_STARTPOINT = "USER.ST.FOOTNOTE.STARTPOINT";
        static String KEY_STARTPOINTRADIUS = "USER.ST.FOOTNOTE.STARTPOINTRADIUS";
        private STSettings() { }
        public void RemoveSettings()
        {
            Eplan.EplApi.Base.Settings oSettings = new Eplan.EplApi.Base.Settings();

            if (oSettings.ExistSetting(KEY_LINEWIDTH))
            {
                oSettings.DeleteSetting(KEY_LINEWIDTH);
                MessageBox.Show($"Settings {KEY_LINEWIDTH} will be deleted", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Settings{KEY_LINEWIDTH} not found!", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (oSettings.ExistSetting(KEY_TEXTHEIGHT))
            {
                oSettings.DeleteSetting(KEY_TEXTHEIGHT);
                MessageBox.Show($"Settings {KEY_TEXTHEIGHT} will be deleted", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Settings{KEY_TEXTHEIGHT} not found!", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (oSettings.ExistSetting(KEY_LINECURSOR))
            {
                oSettings.DeleteSetting(KEY_LINECURSOR);
                MessageBox.Show($"Settings {KEY_LINECURSOR} will be deleted", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Settings{KEY_LINECURSOR} not found!", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (oSettings.ExistSetting(KEY_PROPERTYID))
            {
                oSettings.DeleteSetting(KEY_PROPERTYID);
                MessageBox.Show($"Settings {KEY_PROPERTYID} will be deleted", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Settings{KEY_PROPERTYID} not found!", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (oSettings.ExistSetting(KEY_STARTPOINT))
            {
                oSettings.DeleteSetting(KEY_STARTPOINT);
                MessageBox.Show($"Settings {KEY_STARTPOINT} will be deleted", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Settings{KEY_STARTPOINT} not found!", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (oSettings.ExistSetting(KEY_STARTPOINTRADIUS))
            {
                oSettings.DeleteSetting(KEY_STARTPOINTRADIUS);
                MessageBox.Show($"Settings {KEY_STARTPOINTRADIUS} will be deleted", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Settings{KEY_STARTPOINTRADIUS} not found!", "Footnote addin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadSettings()
        {
            Eplan.EplApi.Base.Settings oSettings = new Eplan.EplApi.Base.Settings();
            if (oSettings.ExistSetting(KEY_LINEWIDTH))
            {
                LINEWIDTH = oSettings.GetDoubleSetting(KEY_LINEWIDTH, 0);
            }

            if (oSettings.ExistSetting(KEY_TEXTHEIGHT))
            {
                TEXTHEIGHT = oSettings.GetDoubleSetting(KEY_TEXTHEIGHT, 0);
            }

            if (oSettings.ExistSetting(KEY_LINECURSOR))
            {
                LINECURSOR = oSettings.GetBoolSetting(KEY_LINECURSOR, 0);
            }

            if (oSettings.ExistSetting(KEY_PROPERTYID))
            {
                PROPERTYID = (PropertiesList)oSettings.GetNumericSetting(KEY_PROPERTYID, 0);
            }

            if (oSettings.ExistSetting(KEY_STARTPOINT))
            {
                STARTPOINT = oSettings.GetBoolSetting(KEY_STARTPOINT, 0);
            }
            if (oSettings.ExistSetting(KEY_STARTPOINTRADIUS))
            {
                STARTPOINTRADIUS = oSettings.GetDoubleSetting(KEY_STARTPOINTRADIUS, 0);
            }
        }

        public void SaveSettings()
        {
            Eplan.EplApi.Base.Settings oSettings = new Eplan.EplApi.Base.Settings();
            if (!oSettings.ExistSetting(KEY_LINEWIDTH))
            {
                oSettings.AddDoubleSetting(KEY_LINEWIDTH,
                    new double[] { 0.8 },
                    new Range[] { new Range { FromValue = 0, ToValue = 32768 } },
                    ISettings.CreationFlag.Insert);
            }
            oSettings.SetDoubleSetting(KEY_LINEWIDTH, LINEWIDTH, 0);

            if (!oSettings.ExistSetting(KEY_TEXTHEIGHT))
            {
                oSettings.AddDoubleSetting(KEY_TEXTHEIGHT,
                    new double[] { 0.25 },
                    new Range[] { new Range { FromValue = 0, ToValue = 32768 } },
                    ISettings.CreationFlag.Insert);
            }
            oSettings.SetDoubleSetting(KEY_TEXTHEIGHT, TEXTHEIGHT, 0);

            if (!oSettings.ExistSetting(KEY_LINECURSOR))
            {
                oSettings.AddBoolSetting(KEY_LINECURSOR,
                    new bool[] { true, false },
                    ISettings.CreationFlag.Insert);
            }
            oSettings.SetBoolSetting(KEY_LINECURSOR, LINECURSOR, 0);

            if (!oSettings.ExistSetting(KEY_PROPERTYID))
            {
                oSettings.AddNumericSetting(KEY_PROPERTYID,
                    new int[] { 20450 },
                    new Range[] { new Range { FromValue = 0, ToValue = 32768 } },
                    ISettings.CreationFlag.Insert);
            }
            oSettings.SetNumericSetting(KEY_PROPERTYID, (int)PROPERTYID, 0);

            if (!oSettings.ExistSetting(KEY_STARTPOINT))
            {
                oSettings.AddBoolSetting(KEY_STARTPOINT,
                    new bool[] { true, false },
                    ISettings.CreationFlag.Insert);
            }
            oSettings.SetBoolSetting(KEY_STARTPOINT, STARTPOINT, 0);


            if (!oSettings.ExistSetting(KEY_STARTPOINTRADIUS))
            {
                oSettings.AddDoubleSetting(KEY_STARTPOINTRADIUS,
                new double[] { 0.4 },
                    new Range[] { new Range { FromValue = 0, ToValue = 32768 } },
                    ISettings.CreationFlag.Insert);
            }
            oSettings.SetDoubleSetting(KEY_STARTPOINTRADIUS, STARTPOINTRADIUS, 0);
        }
    }
}
