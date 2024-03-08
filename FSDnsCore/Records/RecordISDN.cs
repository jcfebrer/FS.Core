namespace FSDnsCore
{
    public class RecordISDN : Record
    {
        public string ISDNADDRESS;
        public string SA;

        public RecordISDN(RecordReader rr)
        {
            ISDNADDRESS = rr.ReadString();
            SA = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                                 ISDNADDRESS,
                                 SA);
        }
    }
}