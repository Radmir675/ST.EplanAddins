namespace ST.EplAddin.PlcEdit.View
{
    public class CsvFileDataModelViews
    {

        public CsvFileDataModelView CsvFileDataModelView { get; set; }
        public bool IsChecked { get; set; }
        public CsvFileDataModelViews(CsvFileDataModelView csvFileDataModelView, bool isChecked)
        {
            CsvFileDataModelView = csvFileDataModelView;
            IsChecked = isChecked;
        }
    }
}
