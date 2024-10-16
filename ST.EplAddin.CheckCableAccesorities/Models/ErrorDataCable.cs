namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class ErrorDataCable
    {
        public string Name { get; set; }
        public int PartNumber { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string GroupName { get; set; }
        public ErrorDataCable(string cableName, int partNumber, string type, string name, string groupName)
        {
            Name = cableName;
            PartNumber = partNumber;
            Type = type;
            Message = name;
            GroupName = groupName;
        }
    }
}

