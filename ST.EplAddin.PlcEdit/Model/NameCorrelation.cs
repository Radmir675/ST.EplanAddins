namespace ST.EplAddin.PlcEdit
{
    public class NameCorrelation
    {
        public NameCorrelation(string functionOldName, string functionNewName, string oldDevicePointDesignation, string newDevicePointDesignation)
        {
            FunctionOldName = functionOldName;
            FunctionNewName = functionNewName;
            OldDevicePointDesignation = oldDevicePointDesignation;
            NewDevicePointDesignation = newDevicePointDesignation;
        }
        public string FunctionOldName { get; set; }
        public string FunctionNewName { get; set; }
        public string OldDevicePointDesignation { get; }
        public string NewDevicePointDesignation { get; }
    }
}
