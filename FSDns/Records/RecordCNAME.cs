namespace FSDns
{
    public class RecordCNAME : Record
    {
        public string CNAME;

        public RecordCNAME(RecordReader rr)
        {
            CNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return CNAME;
        }
    }
}