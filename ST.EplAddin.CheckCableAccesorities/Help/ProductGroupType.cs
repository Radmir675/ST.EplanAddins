using System.ComponentModel;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    public enum ProductGroupType
    {
        [Description("Кабели")]
        ElectricalCableConnection = 29,
        [Description("Принадлежности для маршрутизации")]
        MechanicsRoutingAccessories = 125,
        [Description("Общие")]
        Common = 1,
    }
}
