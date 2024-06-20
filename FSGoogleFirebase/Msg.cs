using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSGoogleFirebase
{
	public class Msg
	{
		public Device sourceId { get; set; }
		public String mensaje { get; set; }
		public Device destId { get; set; }
		public String codeMsg { get; set; }
		public DateTime dateTime { get; set; }
		public Boolean fileHttp { get; set; }
		public Msg referenceMsg { get; set; }
		public String parameter { get; set; }

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
				reference = FSCrypto.Base64.Encode(referenceMsg.ToString(), false);

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
			referenceMsg = new Msg().FromString(FSCrypto.Base64.Decode(data[6], false));
			parameter = data[7];

			return this;
		}
	}
}

