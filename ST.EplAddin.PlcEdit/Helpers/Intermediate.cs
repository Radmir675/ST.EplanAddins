namespace ST.EplAddin.PlcEdit
{
    public class Intermediate
    {
        public Intermediate(string functionOldName, string functionNewName)
        {
            FunctionOldName = functionOldName;
            FunctionNewName = functionNewName;
        }

        public string FunctionOldName { get; set; }
        public string FunctionNewName { get; set; }
    }
}
