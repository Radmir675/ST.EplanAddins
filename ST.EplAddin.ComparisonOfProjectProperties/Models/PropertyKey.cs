namespace ST.EplAddin.ComparisonOfProjectProperties
{
    public struct PropertyKey
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public PropertyKey(int id, int index)
        {
            Id = id;
            Index = index;
        }
    }
}
