namespace FSDns
{
    public class RecordDNAME : Record
    {
        public string TARGET;

        public RecordDNAME(RecordReader rr)
        {
            TARGET = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return TARGET;
        }
    }
}