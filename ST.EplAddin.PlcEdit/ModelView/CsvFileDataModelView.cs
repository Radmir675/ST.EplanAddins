namespace ST.EplAddin.PlcEdit
{
    public class CsvFileDataModelView
    {
        public string SymbolicAdress { get; set; }//DO_xLinAct2CP
        public string BitNumber { get; set; }//Bit0
        public string Unit { get; set; }//empty

        public string FunctionText { get; set; }//Линейный актуатор 2 в позицию калибровки
        public string PLCAdress { get; set; }//I131.7
        public string DeviceNameShort { get; set; }//A5
        public CsvFileDataModelView()
        {

        }
        public CsvFileDataModelView(string symbolicAdress)
        {
            SymbolicAdress = symbolicAdress;
        }
    }

}
