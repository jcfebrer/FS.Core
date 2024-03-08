namespace FSDnsCore
{
    public class RecordNSAPPTR : Record
    {
        public string OWNER;

        public RecordNSAPPTR(RecordReader rr)
        {
            OWNER = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0}", OWNER);
        }
    }
}