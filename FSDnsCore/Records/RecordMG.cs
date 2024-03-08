namespace FSDnsCore
{
    public class RecordMG : Record
    {
        public string MGMNAME;

        public RecordMG(RecordReader rr)
        {
            MGMNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return MGMNAME;
        }
    }
}