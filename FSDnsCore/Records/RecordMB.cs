namespace FSDnsCore
{
    public class RecordMB : Record
    {
        public string MADNAME;

        public RecordMB(RecordReader rr)
        {
            MADNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return MADNAME;
        }
    }
}