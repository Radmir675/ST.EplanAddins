using Eplan.EplApi.Base;
using Eplan.EplApi.DataModel.EObjects;
using System.Collections.Generic;
using System.Linq;

namespace ST.EplAddin.PlcEdit
{
    public static class Mapper
    {

        public static List<PlcDataModelView> GetPlcData(Terminal[] plcTerminals)
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
                        FunctionText = terminal.Properties.FUNC_TEXT_AUTOMATIC.ToString(),
                        DT = terminal.Properties.FUNC_IDENTDEVICETAG.ToString(ISOCode.Language.L_ru_RU),
                        DevicePointDesignation = terminal.Properties.FUNC_PLCAUTOPLUG_AND_CONNPTDESIGNATION.ToString(ISOCode.Language.L_ru_RU),
                        FunctionDefinition = terminal.Properties.FUNC_COMPONENTTYPE.ToString(ISOCode.Language.L_ru_RU),
                        SymbolicAdressDefined = terminal.Properties.FUNC_PLCSYMBOLICADDRESS_CALCULATED.ToString(ISOCode.Language.L_ru_RU)
                    };
                    plcDataModelView.Add(mappedPlc);
                }
            }

            var output = plcDataModelView.GroupBy(f => f.DT).ToList();//тут происходит поиск гланой функции если ее нет берется обзор
            return output;
        }
    }

}
