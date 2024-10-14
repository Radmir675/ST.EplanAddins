namespace ST.EplAddin.CheckCableAccesorities.Models
{
    internal class ErrorDataCable
    {
        public string Name { get; set; }
        public int PartNumber { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }

        public ErrorDataCable(string name, int partNumber, string type = null, string message = null)
        {
            Name = name;
            PartNumber = partNumber;
            Type = type;
            Message = message;
        }
    }
}

