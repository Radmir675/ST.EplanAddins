using System.Runtime.Serialization;



//LEGEND NOTE
namespace ST.EplAddin.Footnote
{
    public partial class FootnoteItem
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
        }
    }
}

