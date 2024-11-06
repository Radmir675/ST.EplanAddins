namespace ST.EplAddin.FootNote.ProperyBrowser
{
    public class PropertyEplan
    {
        public PropertyEplan()
        {
        }

        public PropertyEplan(string property, string value, int propertyIndex)
        {
            Property = property;
            Value = value;
            PropertyIndex = propertyIndex;
        }

        public string Property { get; set; }
        public string Value { get; set; }
        public int PropertyIndex { get; }
    }
}
