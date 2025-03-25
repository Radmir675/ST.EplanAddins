using System.ComponentModel;

namespace ST.EplAddin.FootNote.Models
{
    public enum PropertyStates
    {
        [Description("Ссылка изделия")]
        ArticleReferenceProperty,
        [Description("Изделие")]
        ArticleProperty,
        [Description("Размещение изделия")]
        Placement3DProperty
    }
}
