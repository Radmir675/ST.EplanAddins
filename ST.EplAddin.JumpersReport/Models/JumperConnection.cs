namespace ST.EplAddin.JumpersReport
{
    internal record JumperConnection
    {
        public string StartFullDeviceName;//"+S1-KL8"
        public string EndFullDeviceName;
        public string StartLocation;//"S1"
        public string EndLocation;
        public string StartPinDesignation;//"A2"
        public string EndPinDesignation;
        public string StartLiteralDT;//"KL"
        public string EndLiteralDT;
        public int StartDTCounter;//9
        public int EndDTCounter;
        public int StartSubDTCounter;//9
        public int EndSubDTCounter;
        public string StartSubLiteralDT;//"KL"
        public string EndSubLiteralDT;
    }
}
