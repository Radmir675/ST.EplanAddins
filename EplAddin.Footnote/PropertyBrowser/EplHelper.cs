using Eplan.EplApi.Base;
using System;

namespace ST.EplAddin.Footnote
{
    public class EplHelper
    {
        public static ISOCode.Language GuiLanguage { get { return new Languages().GuiLanguage.GetNumber(); } }
    }
    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }
        public IntPtr Handle
        {
            get { return _hwnd; }
        }
        private IntPtr _hwnd;
    }
}
