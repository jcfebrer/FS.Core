namespace FSDnsCore
{
    public class RecordUnknown : Record
    {
        public byte[] RDATA;

        public RecordUnknown(RecordReader rr)
        {
            // re-read length
            int RDLENGTH = rr.ReadUInt16(-2);
            RDATA = rr.ReadBytes(RDLENGTH);
        }
    }
}