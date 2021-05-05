namespace FSDns
{
    public class RecordDNSKEY : Record
    {
        public byte[] RDATA;

        public RecordDNSKEY(RecordReader rr)
        {
            // re-read length
            int RDLENGTH = rr.ReadUInt16(-2);
            RDATA = rr.ReadBytes(RDLENGTH);
        }

        public override string ToString()
        {
            return string.Format("not-used");
        }
    }
}