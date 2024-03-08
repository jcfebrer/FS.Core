namespace FSDnsCore
{
    public class RecordMINFO : Record
    {
        public string EMAILBX;
        public string RMAILBX;

        public RecordMINFO(RecordReader rr)
        {
            RMAILBX = rr.ReadDomainName();
            EMAILBX = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", RMAILBX, EMAILBX);
        }
    }
}