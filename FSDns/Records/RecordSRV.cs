namespace FSDns
{
    public class RecordSRV : Record
    {
        public int PORT;
        public int PRIORITY;
        public string TARGET;
        public int WEIGHT;

        public RecordSRV(RecordReader rr)
        {
            PRIORITY = rr.ReadUInt16();
            WEIGHT = rr.ReadUInt16();
            PORT = rr.ReadUInt16();
            TARGET = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}",
                                 PRIORITY,
                                 WEIGHT,
                                 PORT,
                                 TARGET);
        }
    }
}