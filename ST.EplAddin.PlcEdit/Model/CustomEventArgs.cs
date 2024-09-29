using System.Collections.Generic;

namespace ST.EplAddin.PlcEdit
{
    public class CustomEventArgs
    {
        public List<PlcDataModelView> PlcDataModelView { get; set; }

        public CustomEventArgs(List<PlcDataModelView> plcDataModelView)
        {
            PlcDataModelView = plcDataModelView;
        }
    }
}