using System.ComponentModel;

namespace ST.EplAddin.CheckCableAccesorities.Help
{
    public enum ProductTopGroupType
    {
        [Description("Электротехника")]
        Electric = 1,

        [Description("Fluid-техника")]
        Fluid = 2,

        [Description("Механика")]
        Mechanic = 3,

        [Description("Технология производственных процессов")]
        Process = 4,

        [Description("Не определено")]
        Undefined = 0,

        [Description("Любое")]
        Any = 999,
    }
}
