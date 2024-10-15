using System.ComponentModel;

namespace ST.EplAddin.CheckCableAccesorities.ProductGroupEnums
{
    public enum ProductGroupEnum
    {

        [Description("Кабели")]
        ElectricalCableConnection = 29,

        [Description("Принадлежности для маршрутизации")]
        MechanicsRoutingAccessories = 125,

        [Description("Общие")]
        Common = 1,

        [Description("Не определено")]
        Undefined = 0,
    }
}
