using Eplan.EplApi.ApplicationFramework;
using System;

namespace ST.EplAddin.FootNote
{
    internal class ShadowCopyPath : IEplAddIn, IEplAddInShadowCopy
    {
        private string m_strOriginalAssemblyPath;
        public bool OnRegister(ref Boolean bLoadOnStart)
        {
            bLoadOnStart = true;
            return true;
        }

        public bool OnUnregister()
        {
            return true;
        }


        public void OnBeforeInit(string strOriginalAssemblyPath)
        {
            m_strOriginalAssemblyPath = strOriginalAssemblyPath;
        }

        public string GetOriginalAssemblyPath()
        {
            return m_strOriginalAssemblyPath;
        }
        public bool OnInit()
        {
            return true;
        }

        public bool OnInitGui()
        {
            return true;
        }


        public bool OnExit()
        {
            return true;
        }
    }
}
