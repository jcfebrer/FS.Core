namespace FSDns
{
    public class RecordHINFO : Record
    {
        public string CPU;
        public string OS;

        public RecordHINFO(RecordReader rr)
        {
            CPU = rr.ReadString();
            OS = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("CPU={0} OS={1}", CPU, OS);
        }
    }
}