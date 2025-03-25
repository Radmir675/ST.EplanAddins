using ST.EplAddin.FootNote.Forms;
using System.Collections.Generic;

namespace ST.EplAddin.FootNote.Services
{
    internal interface IPropertiesStore
    {
        IEnumerable<PropertyEplan> ArticleProperties { get; set; }
        IEnumerable<PropertyEplan> ArticleReferenceProperties { get; set; }
        IEnumerable<PropertyEplan> Placement3DProperties { get; set; }
    }
}