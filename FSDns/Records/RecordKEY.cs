namespace FSDns
{
    public class RecordKEY : Record
    {
        public byte ALGORITHM;
        public int FLAGS;
        public byte PROTOCOL;
        public string PUBLICKEY;

        public RecordKEY(RecordReader rr)
        {
            FLAGS = rr.ReadUInt16();
            PROTOCOL = rr.ReadByte();
            ALGORITHM = rr.ReadByte();
            PUBLICKEY = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} \"{3}\"",
                                 FLAGS,
                                 PROTOCOL,
                                 ALGORITHM,
                                 PUBLICKEY);
        }
    }
}