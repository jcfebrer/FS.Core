namespace FSDns
{
    public class RecordMR : Record
    {
        public string NEWNAME;

        public RecordMR(RecordReader rr)
        {
            NEWNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return NEWNAME;
        }
    }
}