namespace FSDns
{
    public class RecordTXT : Record
    {
        public string TXT;

        public RecordTXT(RecordReader rr)
        {
            TXT = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("\"{0}\"", TXT);
        }
    }
}