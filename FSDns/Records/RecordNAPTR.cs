namespace FSDns
{
    public class RecordNAPTR : Record
    {
        public string FLAGS;
        public int ORDER;
        public int PREFERENCE;
        public string REGEXP;
        public string REPLACEMENT;
        public string SERVICES;

        public RecordNAPTR(RecordReader rr)
        {
            ORDER = rr.ReadUInt16();
            PREFERENCE = rr.ReadUInt16();
            FLAGS = rr.ReadString();
            SERVICES = rr.ReadString();
            REGEXP = rr.ReadString();
            REPLACEMENT = rr.ReadDomainName();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} \"{2}\" \"{3}\" \"{4}\" {5}",
                                 ORDER,
                                 PREFERENCE,
                                 FLAGS,
                                 SERVICES,
                                 REGEXP,
                                 REPLACEMENT);
        }
    }
}