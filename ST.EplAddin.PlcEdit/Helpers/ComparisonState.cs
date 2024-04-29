using System.ComponentModel.DataAnnotations;

namespace ST.EplAddin.PlcEdit.Helpers
{
    public enum ComparisonState
    {
        [Display(Name = "None activated")]
        None = 0,

        [Display(Name = "Show differences")]
        Differences,

        [Display(Name = "Show similarities")]
        Similarities
    }
}
