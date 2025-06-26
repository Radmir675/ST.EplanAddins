using System;

namespace ST.EplAddin.JumpersReport
{
    internal class MessageBoxWrapper : System.Windows.Forms.IWin32Window
    {
        public MessageBoxWrapper(IntPtr handle)
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
