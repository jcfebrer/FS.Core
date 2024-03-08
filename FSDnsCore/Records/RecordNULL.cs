namespace FSDnsCore
{
    public class RecordNULL : Record
    {
        public byte[] ANYTHING;

        public RecordNULL(RecordReader rr)
        {
            rr.Position -= 2;
            // re-read length
            int RDLENGTH = rr.ReadUInt16();
            ANYTHING = new byte[RDLENGTH];
            ANYTHING = rr.ReadBytes(RDLENGTH);
        }

        public override string ToString()
        {
            return string.Format("...binary data... ({0}) bytes", ANYTHING.Length);
        }
    }
}