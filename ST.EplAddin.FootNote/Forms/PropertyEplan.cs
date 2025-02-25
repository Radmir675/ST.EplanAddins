namespace ST.EplAddin.FootNote.Forms
{
    public class PropertyEplan
    {
        public PropertyEplan(string property, string value, int propertyIndex, string description = "")
        {
            Property = property;
            Value = value;
            PropertyIndex = propertyIndex;
            Description = description;
        }

        public string Property { get; set; }
        public string Value { get; set; }
        public int PropertyIndex { get; set; }
        public string Description { get; set; }
    }
}
