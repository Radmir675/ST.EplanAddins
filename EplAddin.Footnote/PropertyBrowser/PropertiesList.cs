using System.Runtime.Serialization;

namespace ST.EplAddin.Footnote
{
    /// <summary>
    /// Перечень выводимых свойств
    /// </summary>
    public enum PropertiesList
    {
        [EnumMember(Value = "Номер легенды электрошкафа")]
        P20450 = 20450,

        [EnumMember(Value = "Номер позиции в спецификации")]
        P20487 = 20487,

        [EnumMember(Value = "ОУ идентифицируещее без структуры")]
        P20008 = 20008,

        [EnumMember(Value = "Пользовательский текст")]
        User_defined = 0,

        [EnumMember(Value = "Все доступные свойства")]
        AllAvailableProperties = 1,
    }
}

