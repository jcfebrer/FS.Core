namespace FSDnsCore
{
    public class RecordRP : Record
    {
        public string MBOXDNAME;
        public string TXTDNAME;

        public RecordRP(RecordReader rr)
        {
            //MBOXDNAME = rr.ReadString();
            MBOXDNAME = rr.ReadDomainName();
            TXTDNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                                 MBOXDNAME,
                                 TXTDNAME);
        }
    }
}