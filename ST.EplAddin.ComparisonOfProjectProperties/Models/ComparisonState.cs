using System.ComponentModel;

namespace ST.EplAddin.ComparisonOfProjectProperties.Models
{
    internal enum ComparisonState
    {
        [Description("Отсутствует")]
        None,
        [Description("Различия")]
        Difference,
        [Description("Сходства")]
        Similarity
    }
}
