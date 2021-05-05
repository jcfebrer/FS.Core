namespace FSDns
{
    public class RecordPTR : Record
    {
        public string PTRDNAME;

        public RecordPTR(RecordReader rr)
        {
            PTRDNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return PTRDNAME;
        }
    }
}