
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System;
using System.Collections;
using System.Windows.Forms;


using System.Net.NetworkInformation;
using System.Net;
using FSNetwork;

namespace FSGestion
{
	public partial class frmAsistente
	{
		
		
		public void frmAsistente_ValidatePage(System.Int32 piPageNumber, ref System.Boolean cancel)
		{
			if (piPageNumber == 2)
			{
				MessageBox.Show("Prueba de validación.");
				cancel = true;
			}
		}
		
		public frmAsistente()
		{
			
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			
			NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();
			
			foreach (NetworkInterface n in ni)
			{
				TreeNode lvi = new TreeNode(n.Name + " - " + n.OperationalStatus.ToString());
				
				foreach (MulticastIPAddressInformation multi in n.GetIPProperties().MulticastAddresses)
				{
					lvi.Nodes.Add(string.Format("  Multicast Address ....................... : {0} {1} {2}", multi.Address, multi.IsTransient ? "Transient" : "", multi.IsDnsEligible ? "DNS Eligible" : ""));
				}
				foreach (UnicastIPAddressInformation uni in n.GetIPProperties().UnicastAddresses)
				{
					lvi.Nodes.Add(string.Format("  Multicast Address ....................... : {0} {1} {2}", uni.Address, uni.IsTransient ? "Transient" : "", uni.IsDnsEligible ? "DNS Eligible" : ""));
				}
				foreach (IPAddressInformation any in n.GetIPProperties().AnycastAddresses)
				{
					lvi.Nodes.Add(string.Format("  Multicast Address ....................... : {0} {1} {2}", any.Address, any.IsTransient ? "Transient" : "", any.IsDnsEligible ? "DNS Eligible" : ""));
				}
				
				TreeView1.Nodes.Add(lvi);
			}
			
			//Dim sHostName As String = Dns.GetHostName()
			//Dim ipE As IPHostEntry = Dns.GetHostByName(sHostName)
			//Dim IpA() As IPAddress = ipE.AddressList
			
			//For Each ip As IPAddress In IpA
			//    TreeView1.Nodes.Add(ip.ToString())
			//Next
			
			PingAsync ping = new PingAsync();
			ping.CompleteCallback += new PingAsync.PingCompleted(PingComplete);
			ping.Ping("83.213.0.1");
			
			System.Net.IPAddress[] ips = FSNetwork.Net.GetIPAddress();
			
			bool isok = default(bool);
			foreach (System.Net.IPAddress ip in ips)
			{
				if (ip.ToString().StartsWith("192.168"))
				{
					isok = true;
				}
			}
			
			if (isok)
			{
				MessageBox.Show("IP Local correcta.");
			}
			else
			{
				MessageBox.Show("Debes configurar tu red local con una ip del tipo 192.168.x.x");
			}
			
			
			
			//Dim http As New FSFormControls.DBHttp()
			
			//lectura del status
			//MsgBox(http.GetHTTPCamera("http://192.168.1.56/get_status.cgi"))
			//cambio de ip
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/set_network.cgi?next_url=rebootme.htm&ip=192.168.1.56&mask=255.255.0.0&gateway=192.168.1.1&dns=192.168.1.1&port=80"))
			//establecer ip con dhcp
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/set_network.cgi?next_url=rebootme.htm&ip=&mask=&gateway=&dns=&port=80"))
			//lectura de parámetros
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/get_params.cgi"))
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/get_misc.cgi"))
			//reboot
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/reboot.cgi?next_url=reboot.htm"))
			//sanpshot
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/snapshot.cgi"))
			//videstream1
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/videostream.cgi"))
			//set wifi wpa_psk
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/set_wifi.cgi?next_url=rebootme.htm&channel=5&mode=0&enable=1&ssid=nombre_wifi&encrypt=2&authtype=0&keyformat=0&defkey=0&key1=&key2=&key3=&key4=&key1_bits=0&key2_bits=0&key3_bits=0&key4_bits=0&wpa_psk=password_psk"))
			//set wifi wep
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/set_wifi.cgi?next_url=rebootme.htm&channel=5&mode=0&enable=1&ssid=iune%20ta%20jon&encrypt=1&authtype=0&keyformat=0&defkey=0&key1=clavewep1&key2=&key3=&key4=&key1_bits=0&key2_bits=0&key3_bits=0&key4_bits=0&wpa_psk="))
			//activar detector de movimiento
			//MsgBox(http.GetHttpCamera("http://192.168.1.56/set_alarm.cgi?next_url=alarm.htm&motion_armed=1&input_armed=1&motion_sensitivity=5&iolinkage=0&mail=0&upload_interval=0&schedule_enable=0&schedule_sun_0=0&schedule_sun_1=0&schedule_sun_2=0&schedule_mon_0=0&schedule_mon_1=0&schedule_mon_2=0&schedule_tue_0=0&schedule_tue_1=0&schedule_tue_2=0&schedule_wed_0=0&schedule_wed_1=0&schedule_wed_2=0&schedule_thu_0=0&schedule_thu_1=0&schedule_thu_2=0&schedule_fri_0=0&schedule_fri_1=0&schedule_fri_2=0&schedule_sat_0=0&schedule_sat_1=0&schedule_sat_2=0"))
		}
		
		public void PingComplete(object sender, EventArgs e, string message)
		{
			MessageBox.Show(message);
		}
		
	}
	
}
