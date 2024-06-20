using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebaseCore
{
	public class Device
	{
		public string androidid { get; set; }
		public string email { get; set; }
		public string app { get; set; }
		public bool online { get; set; }

		private long _heartbeat;
        public long heartbeat {
			get { return _heartbeat; }
			set { 
				_heartbeat = value;
                heartbeatdate = FSLibraryCore.DateTimeUtil.FromUnixTime(value);
            }
		}

        public DateTime heartbeatdate;
		public string token { get; set; }

		public String separatorId = ":v:";

		public Device()
		{
		}

		public Device(string androidid, string app, string token, string email, bool online, long heartbeat)
		{
			this.androidid = androidid;
			this.email = email;
			this.app = app;
			this.token = token;
			this.online = online;
            this.heartbeat = heartbeat;
        }

		public Device(String androidid, String app, String token)
		{
			this.androidid = androidid;
			this.app = app;
			this.token = token;
			this.email = "";
			this.online = false;
            this.heartbeat = 0;
        }

		public Device(String androidid, String email, String app, String token)
		{
			this.androidid = androidid;
			this.email = email;
			this.app = app;
			this.token = token;
			this.online = false;
            this.heartbeat = 0;
        }

		public override String ToString()
		{
            return androidid + separatorId + app + separatorId + token + separatorId + email + separatorId + online + separatorId + heartbeat;
		}

		public String ToStringId()
		{
			return androidid + separatorId + app;
		}


		public Device FromString(String deviceData)
		{
			if (deviceData.Equals("")) return null;

			String[] device = deviceData.Split(separatorId.ToCharArray());

			if (device.Length >= 1) androidid = device[0];
			if (device.Length >= 2) app = device[1];
			if (device.Length >= 3) token = device[2];
			if (device.Length >= 4) email = device[3];
            if (device.Length >= 5) online = Convert.ToBoolean(device[4]);
            if (device.Length >= 6)
            {
                heartbeat = Convert.ToInt64(device[5]);
            }

            return this;
		}
	}
}
