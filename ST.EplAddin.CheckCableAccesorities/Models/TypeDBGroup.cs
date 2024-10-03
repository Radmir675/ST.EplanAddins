namespace ST.EplAddin.CheckCableAccesorities.Models
{
    public class TypeDBGroup
    {
        public string Description { get; private set; }
        public int Id { get; private set; }
        public TypeDBGroup(string description, int id)
        {
            Description = description;
            Id = id;
        }
    }
}
