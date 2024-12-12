namespace ST.EplAddin.UserConfigurationService.Models
{
    internal class Scheme()
    {
        private string _name;

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name)) return "Не определено";
                return _name;
            }
            set => _name = value;
        }

        public string Description { get; set; }
        public string Database { get; set; }
        public string Catalog { get; set; }

    }
}
