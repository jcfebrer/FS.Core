namespace FSDns
{
    public class RecordWKS : Record
    {
        public string ADDRESS;
        public byte[] BITMAP;
        public int PROTOCOL;

        public RecordWKS(RecordReader rr)
        {
            int length = rr.ReadUInt16(-2);
            ADDRESS = string.Format("{0}.{1}.{2}.{3}",
                                    rr.ReadByte(),
                                    rr.ReadByte(),
                                    rr.ReadByte(),
                                    rr.ReadByte());
            PROTOCOL = rr.ReadByte();
            length -= 5;
            BITMAP = new byte[length];
            BITMAP = rr.ReadBytes(length);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", ADDRESS, PROTOCOL);
        }
    }
}