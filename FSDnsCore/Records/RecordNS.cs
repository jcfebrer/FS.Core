namespace FSDnsCore
{
    public class RecordNS : Record
    {
        public string NSDNAME;

        public RecordNS(RecordReader rr)
        {
            NSDNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return NSDNAME;
        }
    }
}