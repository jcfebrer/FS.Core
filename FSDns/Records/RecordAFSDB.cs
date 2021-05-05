namespace FSDns
{
    public class RecordAFSDB : Record
    {
        public string HOSTNAME;
        public int SUBTYPE;

        public RecordAFSDB(RecordReader rr)
        {
            SUBTYPE = rr.ReadUInt16();
            //HOSTNAME = rr.ReadString();
            HOSTNAME = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                                 SUBTYPE,
                                 HOSTNAME);
        }
    }
}