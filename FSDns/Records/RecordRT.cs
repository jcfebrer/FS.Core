namespace FSDns
{
    public class RecordRT : Record
    {
        public string INTERMEDIATEHOST;
        public int PREFERENCE;

        public RecordRT(RecordReader rr)
        {
            PREFERENCE = rr.ReadUInt16();
            INTERMEDIATEHOST = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                                 PREFERENCE,
                                 INTERMEDIATEHOST);
        }
    }
}