namespace ST.EplAddin.UserConfigurationService.Models
{
    internal class Schema(string name, string description)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
    }
}
