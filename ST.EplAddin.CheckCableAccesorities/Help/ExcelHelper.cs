namespace ST.EplAddin.CheckCableAccesorities.Help
{
    public class ExcelHelper
    {
        //private List<TypeDBGroup> typesDBGroups { get; set; } = new List<TypeDBGroup>();
        //public List<TypeDBGroup> GetData()
        //{
        //    var ObjWorkExcel = new Application();
        //    Workbook ObjWorkBook = null;
        //    try
        //    {
        //        ObjWorkBook = ObjWorkExcel.Workbooks.Open(@"C:\Users\biktimirov.rr\source\repos\EplanAddins2019\ST.EplAddin.CheckCableAccesorities\Resources\ProductGroupEnum.xlsx");
        //        //ObjWorkBook = ObjWorkExcel.Workbooks.Open(@"..\Resources\ProductGroupEnum.xlsx");
        //    }
        //    catch (Exception ee)
        //    {
        //        var eee = ee.Message;
        //        return typesDBGroups;
        //    }
        //    var ObjWorkSheet = (Worksheet)ObjWorkBook?.Sheets[1];
        //    var lastCell = ObjWorkSheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell);//последнюю ячейку

        //    int lastColumn = lastCell.Column;
        //    int lastRow = lastCell.Row;
        //    for (int i = 1; i < lastRow; i++)
        //    {
        //        int id = int.Parse(ObjWorkSheet.Cells[i + 1, 2].Text);
        //        string description = ObjWorkSheet.Cells[i + 1, 4].Text;
        //        var result = new TypeDBGroup(description, id);
        //        typesDBGroups.Add(result);
        //    }
        //    ObjWorkBook.Close(false, Type.Missing, Type.Missing);
        //    ObjWorkExcel.Quit();
        //    GC.Collect();

        //    return typesDBGroups;
        //}
    }
}