using System;

namespace ST.EplAddin.PlcEdit
{
    public class PlcDataModelView : ICloneable
    {

        public string DevicePointDescription { get; set; }//DI3
        public string PLCAdress { get; set; }//I131.7
        public string Datatype { get; set; }//BOOL
        public string SymbolicAdress { get; set; }//DO_xLinAct2CP
        public string FunctionText { get; set; }//Линейный актуатор 2 в позицию калибровки
        public string DT { get; set; }//+S2-2A5
        public string DevicePointDesignation { get; set; }//-X10:34
        public string FunctionDefinition { get; set; }//Вывод устройства ПЛК, Дискретный вход
        public string SymbolicAdressDefined { get; set; }//сиволический адрес определен если он не пустой значит вывод куда-то присвоен
        public string FunctionType { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
