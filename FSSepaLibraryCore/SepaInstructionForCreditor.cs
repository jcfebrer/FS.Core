namespace FSSepaLibraryCore
{
    public class SepaInstructionForCreditor
    {
        public enum SepaInstructionForCreditorCode
        {
            CHQB,
            HOLD,
            PHOB,
            TELB,
        }

        public SepaInstructionForCreditorCode Code { get; set; }

        public string Comment { get; set; }

        public override string ToString()
        { 
            return Code.ToString() + " " + Comment;
        }
    }
}
