using System.ComponentModel;

namespace ST.EplAddin.CheckCableAccesorities.ProductGroupEnums
{
    public enum ProductSubGroupEnum
    {
        [Description("Не определено")]
        Undefined = 0,

        [Description("Общее")]
        Common = 1,

        [Description("Крепление шланга/кабеля")]
        MechanicsCableTubingClamp = 225,

        [Description("Принадлежности")]
        MechanicsAccessories = 136,

        [Description("Любое")]
        Any = 999
    }
}
