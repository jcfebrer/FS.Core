using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebase
{
	public class Device
	{
		public String androidid;
		public String email;
		public String app;
		public Boolean online;
		public String regId;

		public String separatorId = ":v:";

		public Device()
		{
		}

		public Device(String androidid, String email, String app, String regId, Boolean online)
		{
			this.androidid = androidid;
			this.email = email;
			this.app = app;
			this.regId = regId;
			this.online = online;
		}

		public Device(String androidid, String app, String regId)
		{
			this.androidid = androidid;
			this.app = app;
			this.regId = regId;
			this.online = false;
		}

		public Device(String androidid, String email, String app, String regId)
		{
			this.androidid = androidid;
			this.email = email;
			this.app = app;
			this.regId = regId;
			this.online = false;
		}

		public override String ToString()
		{
			return androidid + separatorId + email + separatorId + app + separatorId + regId; // + separatorId + online;
		}

		public String ToStringId()
		{
			return androidid + separatorId + email + separatorId + app; // + separatorId + regId + separatorId + online;
		}


		public Device FromString(String deviceData)
		{
			if (deviceData.Equals("")) return null;

			String[] device = deviceData.Split(separatorId.ToCharArray());

			if (device.Length >= 1) androidid = device[0];
			if (device.Length >= 2) email = device[1];
			if (device.Length >= 3) app = device[2];
			if (device.Length >= 4) regId = device[3];
			//if (device.Length >= 5)online = Boolean.parseBoolean(device[4]);

			return this;
		}
	}
}
