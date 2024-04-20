using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST.EplAddin.PlcEdit
{
    public class CsvFileDataView
    {
        public string DevicePointDescription { get; set; }//DI3
        public string PLCAdress { get; set; }//I131.7
        public string SymbolicAdress { get; set; }//DO_xLinAct2CP
        public string FunctionText { get; set; }//Линейный актуатор 2 в позицию калибровки
    }
}
