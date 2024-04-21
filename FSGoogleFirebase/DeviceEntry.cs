using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebase
{
    public class DeviceEntry
    {
        public string androidid { get; set; }
        public string app { get; set; }
        public long heartbeat { get; set; }
        public HeartbeatDate heartbeat_date { get; set; }
        public bool online { get; set; }
        public string separatorId { get; set; }
        public string token { get; set; }
    }

    public class HeartbeatDate
    {
        public int date { get; set; }
        public int day { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int month { get; set; }
        public int seconds { get; set; }
        public long time { get; set; }
        public int timezoneOffset { get; set; }
        public int year { get; set; }
    }
}
