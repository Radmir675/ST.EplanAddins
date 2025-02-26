using System.ComponentModel;

namespace ST.EplAddin.FootNote.Forms
{
    public class PropertyEplan
    {
        public PropertyEplan(string propertyName, string value, int propertyIndex, string description = "")
        {
            PropertyName = propertyName;
            Value = value;
            PropertyIndex = propertyIndex;
            Description = description;
        }
        public PropertyEplan(string propertyName, string value, int propertyIndex, int indexedNumber, string description = "") : this(propertyName, value, propertyIndex, description = "")
        {
            IndexedNumber = indexedNumber;
        }

        [Description("Имя свойства")]
        public string PropertyName { get; }

        [Description("Значение свойства")]
        public string Value { get; }

        [Description("Индекс свойства")]
        public int PropertyIndex { get; }

        [Description("Подындекс для индексированных свойств")]
        public int? IndexedNumber { get; }

        [Description("Описание свойства")]
        public string Description { get; }
    }
}
