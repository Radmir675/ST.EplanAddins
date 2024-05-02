namespace ST.EplAddin.PlcEdit.View
{
    public class CsvFileDataModelViews : CsvFileDataModelView
    {
        public bool IsChecked { get; set; }
        public CsvFileDataModelViews(CsvFileDataModelView csvFileDataModelView, bool isChecked)
        {
            SymbolicAdress = csvFileDataModelView.SymbolicAdress;
            BitNumber = csvFileDataModelView.BitNumber;
            Unit = csvFileDataModelView.Unit;
            FunctionText = csvFileDataModelView.FunctionText;
            PLCAdress = csvFileDataModelView.PLCAdress;
            DeviceNameShort = csvFileDataModelView.DeviceNameShort;
            IsChecked = isChecked;
        }
    }
}
