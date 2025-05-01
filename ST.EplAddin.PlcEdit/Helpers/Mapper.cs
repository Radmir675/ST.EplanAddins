using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ST.EplAddin.PlcEdit.Helpers
{
    public class PinNumberComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            int pinNumberFirst = 100;
            int.TryParse(x, out pinNumberFirst);

            int pinNumberSecond = 100;
            int.TryParse(x, out pinNumberSecond);

            return pinNumberFirst.CompareTo(pinNumberSecond);
        }
    }
    public class Mapper
    {
        public List<PlcDataModelView> GetPlcData(Terminal[] plcTerminals)
        {
            var plcDataModelView = GetFullDataPlcOutputData(plcTerminals);
            var output = plcDataModelView.GroupBy(f => (f.DT, f.DevicePointDesignation));//тут происходит поиск главной функции если ее нет берется обзор

            var result = new List<PlcDataModelView>();
            foreach (var entry in output)
            {
                var terminal = entry.FirstOrDefault(item => item.FunctionType == "Многополюсный") ?? entry.First();
                result.Add(terminal);
            }
            var sortedInformation = SortTerminalPins(result).ToList();
            return sortedInformation;
        }
        private IEnumerable<PlcDataModelView> GetFullDataPlcOutputData(Terminal[] plcTerminals)
        {
            foreach (var terminal in plcTerminals)
            {
                if (terminal == null) continue;
                var mappedPlc = new PlcDataModelView()
                {
                    DevicePointDescription = terminal.Properties.FUNC_TERMINALDESCRIPTION[1].ToString(ISOCode.Language.L_ru_RU),
                    PLCAdress = terminal.Properties.FUNC_PLCADDRESS.ToString(ISOCode.Language.L_ru_RU),
                    Datatype = terminal.Properties.FUNC_PLCDATATYPE.ToString(ISOCode.Language.L_ru_RU),
                    SymbolicAdress = terminal.Properties.FUNC_PLCSYMBOLICADDRESS_AUTOMATIC.ToString(ISOCode.Language.L_ru_RU),
                    FunctionText = terminal.Properties.FUNC_TEXT_AUTOMATIC.GetDisplayString().GetStringToDisplay(ISOCode.Language.L_ru_RU).Replace("\n", " "),
                    DT = terminal.Properties.FUNC_FULLNAME.ToString(ISOCode.Language.L_ru_RU),
                    IdenticalDT = terminal.Properties.FUNC_IDENTDEVICETAGWITHOUTSTRUCTURES,
                    DevicePointDesignation = terminal.Properties.FUNC_PLCAUTOPLUG_AND_CONNPTDESIGNATION.ToString(ISOCode.Language.L_ru_RU),
                    FunctionDefinition = terminal.Properties.FUNC_COMPONENTTYPE.ToString(ISOCode.Language.L_ru_RU),
                    SymbolicAdressDefined = terminal.Properties.FUNC_PLCSYMBOLICADDRESS_CALCULATED.ToString(ISOCode.Language.L_ru_RU),
                    FunctionType = (terminal.Properties.FUNC_TYPE).GetDisplayString().GetString(ISOCode.Language.L_ru_RU),
                    FunctionTypeId = (terminal.Properties.FUNC_TYPE).ToInt(),
                    TerminalId = terminal.ToStringIdentifier(),
                    DeviceNameShort = terminal.Properties.FUNC_IDENTDEVICETAGWITHOUTSTRUCTURES.ToString(ISOCode.Language.L_ru_RU),
                    DevicePinNumber = terminal.Properties.FUNC_ALLCONNECTIONDESIGNATIONS.ToString(ISOCode.Language.L_ru_RU),
                    StatusImage = GetImage(terminal),
                };
                yield return mappedPlc;
            }
        }

        private static IEnumerable<PlcDataModelView> SortTerminalPins(List<PlcDataModelView> result)
        {
            return result.OrderBy(f => f.IdenticalDT).ThenBy(x => x.DevicePinNumber, new PinNumberComparer());
        }

        public static Image GetImage(Terminal terminal)
        {
            switch (terminal.Properties.FUNC_TYPE.ToInt())
            {
                case 1: return Properties.Resources.plcMultyLine;

                case 3: return Properties.Resources.plcOverview;

                default: return null;//Properties.Resources.undefined;
            }
        }
        public static Image GetImageById(int id)
        {
            switch (id)
            {
                case 1: return Properties.Resources.plcMultyLine;

                case 3: return Properties.Resources.plcOverview;

                default: return null;//Properties.Resources.undefined;
            }
        }
    }

}
