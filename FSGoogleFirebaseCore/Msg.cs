using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebaseCore
{
	public class Msg
	{
		public Device sourceId;
		public String mensaje;
		public Device destId;
		public String codeMsg;
		public DateTime dateTime;
		public Boolean fileHttp;
		public Msg referenceMsg;
		public String parameter;

		public String separatorDat = ":#:";

		public Msg()
		{
		}


		public override String ToString()
		{
			String ret = "";

			String dateT = "";
			String filehttp = "false";
			String reference = "";
			if (dateTime != null)
				dateT = DateTime.Now.ToShortDateString();
			if (fileHttp)
				filehttp = fileHttp.ToString();

			if (referenceMsg != null)
				reference = FSCryptoCore.Base64.Encode(referenceMsg.ToString(), false);

			ret = sourceId + separatorDat + mensaje + separatorDat + destId
					+ separatorDat + dateT + separatorDat + codeMsg
					+ separatorDat + filehttp + separatorDat + reference + separatorDat + parameter + separatorDat + "@@";

			return ret;
		}

		public Msg FromString(String msgData)
		{
			if (msgData.Equals("")) return null;

			String[] data = msgData.Split(separatorDat.ToCharArray());

			sourceId = new Device().FromString(data[0]);
			mensaje = data[1];
			destId = new Device().FromString(data[2]);
			if (!data[3].Equals(""))
				dateTime = DateTime.Parse(data[3]);
			codeMsg = data[4];
			fileHttp = Boolean.Parse(data[5]);
			referenceMsg = new Msg().FromString(FSCryptoCore.Base64.Decode(data[6], false));
			parameter = data[7];

			return this;
		}
	}
}

