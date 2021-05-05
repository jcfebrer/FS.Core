namespace FSDns
{
    public class RecordGPOS : Record
    {
        public string ALTITUDE;
        public string LATITUDE;
        public string LONGITUDE;

        public RecordGPOS(RecordReader rr)
        {
            LONGITUDE = rr.ReadString();
            LATITUDE = rr.ReadString();
            ALTITUDE = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}",
                                 LONGITUDE,
                                 LATITUDE,
                                 ALTITUDE);
        }
    }
}