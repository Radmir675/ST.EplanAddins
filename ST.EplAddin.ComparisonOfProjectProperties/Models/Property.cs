namespace ST.EplAddin.ComparisonOfProjectProperties.Models
{
    public class Property /*: ViewModelBase*/
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int? Index { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool ReadOnly { get; set; }
    }
}
