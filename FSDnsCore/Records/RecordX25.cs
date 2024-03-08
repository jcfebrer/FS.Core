namespace FSDnsCore
{
    public class RecordX25 : Record
    {
        public string PSDNADDRESS;

        public RecordX25(RecordReader rr)
        {
            PSDNADDRESS = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0}",
                                 PSDNADDRESS);
        }
    }
}