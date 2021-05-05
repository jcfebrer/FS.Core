namespace FSDns
{
    public class RecordMF : Record
    {
        public string MADNAME;

        public RecordMF(RecordReader rr)
        {
            MADNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return MADNAME;
        }
    }
}