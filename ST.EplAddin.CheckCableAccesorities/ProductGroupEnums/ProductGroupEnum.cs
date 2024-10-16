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

        [Description("Принадлежности для внутренней установки")]
        MechanicsHousingaccessoriesIn = 51,

        [Description("Распределитель/соединитель проводов")]
        ElectricalConnectionSplicer = 129,

        [Description("Электрошкаф")]
        MechanicsCabinet = 55,

        [Description("Соединения")]
        ElectricalWire = 100,

        [Description("Корпус")]
        MechanicsHousing = 49,

        [Description("Любое")]
        Any = 999
    }
}
