using ST.EplAddin.CheckCableAccesorities.Help;

namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class Part
    {
        public int Number { get; set; }
        public ProductGroupType Type { get; set; }
        public Part(int number, ProductGroupType type)
        {
            Number = number;
            Type = type;
        }


    }
}
