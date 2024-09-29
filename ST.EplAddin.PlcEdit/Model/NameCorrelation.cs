namespace ST.EplAddin.PlcEdit
{
    public class NameCorrelation
    {
        public NameCorrelation(string functionOldName, string functionNewName)
        {
            FunctionOldName = functionOldName;
            FunctionNewName = functionNewName;
        }
        public string FunctionOldName { get; set; }
        public string FunctionNewName { get; set; }
    }
}
