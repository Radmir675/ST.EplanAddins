using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel.EObjects;
using ST.EplAddin.PlcEdit.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.PlcEdit
{
    public static class Mapper
    {

        public static List<PlcDataModelView> GetPlcData(Terminal[] plcTerminals, bool getFullData = false)
        {
            List<PlcDataModelView> plcDataModelView = new List<PlcDataModelView>();
            foreach (var terminal in plcTerminals)
            {
                if (terminal != null)
                {
                    var mappedPlc = new PlcDataModelView()
                    {
                        DevicePointDescription = terminal.Properties.FUNC_TERMINALDESCRIPTION[1].ToString(ISOCode.Language.L_ru_RU),
                        PLCAdress = terminal.Properties.FUNC_PLCADDRESS.ToString(ISOCode.Language.L_ru_RU),
                        Datatype = terminal.Properties.FUNC_PLCDATATYPE.ToString(ISOCode.Language.L_ru_RU),
                        SymbolicAdress = terminal.Properties.FUNC_PLCSYMBOLICADDRESS_AUTOMATIC.ToString(ISOCode.Language.L_ru_RU),
                        FunctionText = terminal.Properties.FUNC_TEXT_AUTOMATIC.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU),
                        DT = terminal.Properties.FUNC_FULLNAME.ToString(ISOCode.Language.L_ru_RU),
                        DevicePointDesignation = terminal.Properties.FUNC_PLCAUTOPLUG_AND_CONNPTDESIGNATION.ToString(ISOCode.Language.L_ru_RU),
                        FunctionDefinition = terminal.Properties.FUNC_COMPONENTTYPE.ToString(ISOCode.Language.L_ru_RU),
                        SymbolicAdressDefined = terminal.Properties.FUNC_PLCSYMBOLICADDRESS_CALCULATED.ToString(ISOCode.Language.L_ru_RU),
                        FunctionType = (terminal.Properties.FUNC_TYPE).GetDisplayString().GetString(ISOCode.Language.L_ru_RU),
                        TerminalId = terminal.ToStringIdentifier(),
                        DeviceNameShort = terminal.Properties.FUNC_IDENTDEVICETAGWITHOUTSTRUCTURES.ToString(ISOCode.Language.L_ru_RU),
                        DevicePinNumber = terminal.Properties.FUNC_ALLCONNECTIONDESIGNATIONS.ToString(ISOCode.Language.L_ru_RU)
                    };
                    plcDataModelView.Add(mappedPlc);
                }
            }
            if (getFullData == true)
            {
                return plcDataModelView;
            }
            var output = plcDataModelView.GroupBy(f => f.DevicePointDesignation);//тут происходит поиск главной функции если ее нет берется обзор
            var result = new List<PlcDataModelView>();
            foreach (var entry in output)
            {
                var terminal = entry.FirstOrDefault(item => item.FunctionType == "Многополюсный") ?? entry.First();
                result.Add(terminal);
            }

            return result.OrderBy(x => Convert.ToInt32(x.DevicePinNumber == "" ? "0" : x.DevicePinNumber)).ToList();
        }


        public static List<FromCsvModelView> ConvertDataFromCsvModel(List<CsvFileDataModelView> csvFiles)
        {
            List<FromCsvModelView> result = new List<FromCsvModelView>(csvFiles.Count);
            foreach (var csvFile in csvFiles)
            {
                FromCsvModelView CsvModelView = new FromCsvModelView()
                {
                    SymbolicAdress = csvFile.SymbolicAdress,
                    FunctionText = csvFile.FunctionText,
                    PLCAdress = csvFile.PLCAdress,
                    DeviceNameShort = csvFile.DeviceNameShort,//подумать
                };
                result.Add(CsvModelView);
            }
            return result;
        }
        public static List<CsvFileDataModelView> ConvertDataToCsvModel(List<PlcDataModelView> plcDataModelViews)
        {
            List<CsvFileDataModelView> result = new List<CsvFileDataModelView>(plcDataModelViews.Count);
            int bitNumber = 0;
            foreach (var plcDataModelView in plcDataModelViews)
            {
                CsvFileDataModelView csvFileDataModelView = new()
                {
                    SymbolicAdress = plcDataModelView.SymbolicAdress,
                    BitNumber = string.Join("", "Bit", $"{bitNumber.ToString()}"),
                    Unit = string.Empty,
                    FunctionText = plcDataModelView.FunctionText,
                    PLCAdress = plcDataModelView.PLCAdress,
                    DeviceNameShort = plcDataModelView.DeviceNameShort,
                };
                bitNumber++;

                result.Add(csvFileDataModelView);
            }
            return result;
        }
        public static List<CsvFileDataModelViews> ConvertDataToCsvCompare(List<CsvFileDataModelView> csvFileDataModelView)
        {
            return csvFileDataModelView.Select(p => new CsvFileDataModelViews(p, false)).ToList();
        }
    }

}
