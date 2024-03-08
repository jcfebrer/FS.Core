namespace FSDnsCore
{
    public class RecordMD : Record
    {
        public string MADNAME;

        public RecordMD(RecordReader rr)
        {
            MADNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return MADNAME;
        }
    }
}