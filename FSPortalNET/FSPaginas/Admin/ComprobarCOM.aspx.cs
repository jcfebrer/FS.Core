// <fileheader>
// <copyright file="comprobarCOM.aspx.cs" company="Febrer Software">
//     Fecha: 19/06/2015
//     Path: admin\comprobarCOM.aspx.cs
//     Copyright (c) 2015 Febrer Software. Todos los derechos reservados.
//     http://www.febrersoftware.com
// </copyright>
// </fileheader>

using System;
using FSPortal;
using FSLibrary;

namespace FSPaginas.Admin
{

    public class ComprobarCom : BasePage
    {
        public string cat;
        public string[] catA;
        public string[] comA;
        public int installedCoMs = 0;
        public int onNum = 0;
        public int show;
        public int showCat;

        public void Inicio()
        {
            Response.Buffer = true;
            Server.ScriptTimeout = 1000;

            //const string lastUpdate = "4/3/2003";

            string com = "CDONTS.NewMail|http://www.microsoft.com|CDONTS (free)|1|";
            com = com + "\n\r" +
                  "MSWC.NextLink|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp7pmc.asp|Microsoft Content Linking Component|0|";
            com = com + "\n\r" +
                  "MSWC.BrowserType|http://msdn.microsoft.com/library/default.asp?url=/library/en-us/iisref/html/psdk/asp/comp3xx0.asp|Microsoft Browser Capability|2|";
            com = com + "\n\r" +
                  "MSWC.ContentRotator|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp09dg.asp|Microsoft Content Rotator|0|";
            com = com + "\n\r" +
                  "MSWC.AdRotator|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp59f8.asp|Microsoft Ad Rotator|0|";
            com = com + "\n\r" +
                  "MSWC.PermissionChecker|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp3hf8.asp|Microsoft Permission Checker Component|0|";
            com = com + "\n\r" +
                  "MSWC.Status|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp1qt0.asp|Microsoft Status Component|0|";
            com = com + "\n\r" +
                  "MSWC.Tools|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp7g8k.asp|Microsoft Tools Component|0|";
            com = com + "\n\r" +
                  "MSWC.PageCounter|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp00vo.asp|Microsoft Page Counter Component|0|";
            com = com + "\n\r" +
                  "MSWC.IISLog|http://msdn.microsoft.com/library/en-us/iisref/html/psdk/asp/comp6i5w.asp|Microsoft Logging Utility Component|0|";
            com = com + "\n\r" +
                  "MSXML2.ServerXMLHTTP|http://msdn.microsoft.com/library/en-us/xmlsdk30/htm/xmobjxmldomserverxmlhttp_using_directly.asp|Microsoft ServerXMLHTTP|13|";
            com = com + "\n\r" + "Microsoft.XMLDOM|http://www.microsoft.com|Microsoft XMLDOM Component|13|";
            com = com + "\n\r" + "Microsoft.XMLHTTP|http://www.microsoft.com|Microsoft XMLHTTP Component|13|";
            com = com + "\n\r" + "Scripting.FileSystemObject|http://www.microsoft.com|MicrosoftFileSystem Object|6|";
            com = com + "\n\r" + "ADOX.Catalog|http://www.microsoft.com|MicroSoft ADOX Catalog|0|";
            com = com + "\n\r" + "WScript.Shell|http://www.microsoft.com|Windows Script Shell|0|";
            com = com + "\n\r" + "WScript.Network|http://www.microsoft.com|Windows Script Network|0|";
            com = com + "\n\r" + "ADODB.Connection|http://www.microsoft.com|ADODB.Connection|0|";
            com = com + "\n\r" + "ADODB.Command|http://www.microsoft.com|ADODB.Command|0|";
            com = com + "\n\r" + "ADODB.Recordset|http://www.microsoft.com|ADODB.Recordset|0|";
            com = com + "\n\r" + "Scripting.Dictionary|http://www.microsoft.com|Scripting.Dictionary|0|";
            com = com + "\n\r" +
                  "ASPFileUpload.File|http://support.microsoft.com/default.aspx?scid=kb;EN-US;q299692|MicroSoft File Upload|3|0";
            com = com + "\n\r" + "Scripting.Encoder|http://www.microsoft.com|Script Encoder|0|";
            com = com + "\n\r" +
                  "Msxml2.DOMDocument.3.0|http://www.microsoft.com|Microsoft XMLDOM 3.0 Component|13|";
            com = com + "\n\r" +
                  "Msxml2.DOMDocument.4.0|http://msdn.microsoft.com/downloads/default.asp?url=/downloads/topic.asp?url=/msdn-files/028/000/072/topic.xml|Microsoft XMLDOM 4.0 Component|13|";
            com = com + "\n\r" + "SMTPsvg.Mailer|http://www.serverobjects.com|Server Objects - ASPMail|1|";
            com = com + "\n\r" + "SMTPsvg.Mailer|http://www.serverobjects.com|Server Objects - ASPQMail|1|";
            com = com + "\n\r" + "AspImage.Image|http://www.serverobjects.com|Server Objects - ASPImage|4|";
            com = com + "\n\r" + "POP3svg.Mailer|http://www.serverobjects.com|Server Objects - ASPPop3|1|";
            com = com + "\n\r" + "AspNNTP.Conn|http://www.serverobjects.com|Server Objects - AspNNTP|0|";
            com = com + "\n\r" + "AspFile.FileObj|http://www.serverobjects.com|ServerObjects - AspFile|6|";
            com = com + "\n\r" + "AspConv.Expert|http://www.serverobjects.com|ServerObjects - AspConv|0|";
            com = com + "\n\r" + "AspHTTP.Conn|http://www.serverobjects.com|ServerObjects - AspHTTP|0|";
            com = com + "\n\r" + "AspDNS.Lookup|http://www.serverobjects.com|ServerObjects - AspDNS|0|";
            com = com + "\n\r" + "AspMX.Lookup|http://www.serverobjects.com|ServerObjects - AspMX|1|";
            com = com + "\n\r" + "WaitFor.Comp|http://www.serverobjects.com|ServerObjects - Waitfor (free)|0|";
            com = com + "\n\r" +
                  "LastMod.FileObj|http://www.serverobjects.com|ServerObjects - Last Modified (free)|6|";
            com = com + "\n\r" + "ImgSize.Check|http://www.serverobjects.com|ServerObjects - Image Size (free)|4|";
            com = com + "\n\r" + "GuidMakr.GUID|http://www.serverobjects.com|ServerObjects - GUID Maker (free)|0|";
            com = com + "\n\r" + "ASPsvg.Process|http://www.serverobjects.com|ServerObjects - AspProc (free)|0|";
            com = com + "\n\r" + "AspPing.Conn|http://www.serverobjects.com|ServerObjects - AspPing (free)|0|";
            com = com + "\n\r" + "AspInet.FTP|http://www.serverobjects.com|ServerObjects - AspInet (free)|0|";
            com = com + "\n\r" + "ASPExec.Execute|http://www.serverobjects.com|ServerObjects - AspExec (free)|0|";
            com = com + "\n\r" + "AspCrypt.Crypt|http://www.serverobjects.com|ServerObjects - AspCryp (free)|9|";
            com = com + "\n\r" + "Bible.Lookup|http://www.serverobjects.com|ServerObjects - AspBible (free)|0|";
            com = com + "\n\r" + "SoftArtisans.SAFile|http://www.softartisans.com|SoftArtisians Fileup|3|";
            com = com + "\n\r" + "SoftArtisans.FileManager|http://www.softartisans.com|SoftArtisians FileManager|6|";
            com = com + "\n\r" + "SoftArtisans.XFRequest|http://www.softartisans.com|SoftArtisians X-File|6|";
            com = com + "\n\r" +
                  "SoftArtisans.FileManagerTX|http://www.softartisans.com|SoftArtisians FileManagerTX|6|";
            com = com + "\n\r" +
                  "SoftArtisans.SASessionPro.1|http://www.softartisans.com|SoftArtisans SA-Session Pro|0|";
            com = com + "\n\r" +
                  "SMUM.XCheck.1|http://www.softartisans.com|SoftArtisians Check (form validator)|11|";
            com = com + "\n\r" + "Softartisans.Archive|http://www.softartisans.com|SoftArtisans Archive|6|";
            com = com + "\n\r" + "SoftArtisans.SMTPMail|http://www.softartisans.com|SoftArtisans SMTPmail|1|";
            com = com + "\n\r" + "Softartisans.ExcelWriter|http://www.softartisans.com|SoftArtisans Excel Writer|5|";
            com = com + "\n\r" + "SoftArtisans.Groups|http://www.softartisans.com|SoftArtisans.Groups (SA-Admin)|9|";
            com = com + "\n\r" +
                  "SoftArtisans.Performance|http://www.softartisans.com|SoftArtisians.Performance (SA-Admin)|9|";
            com = com + "\n\r" + "SoftArtisans.RAS|http://www.softartisans.com|SoftArtisans.RAS (SA-Admin)|9|";
            com = com + "\n\r" + "SoftArtisans.Shares|http://www.softartisans.com|SoftArtisans.Shares (SA-Admin)|9|";
            com = com + "\n\r" + "SoftArtisans.User|http://www.softartisans.com|SoftArtisans.User (SA-Admin)|9|";
            com = com + "\n\r" + "Jmail.smtpmail|http://www.dimac.net|w3 JMail|1|";
            com = com + "\n\r" + "w3sitetree.tree|http://www.dimac.net|w3 Site Tree : www.dimac.net|0|";
            com = com + "\n\r" + "w3.upload|http://www.dimac.net|w3 Upload|3|";
            com = com + "\n\r" + "w3.netutils|http://www.dimac.net|w3 Utils|0|";
            com = com + "\n\r" + "Socket.TCP|http://www.dimac.net|w3 Sockets|0|";
            com = com + "\n\r" + "w3.netutils|http://www.dimac.net|w3 NetDebug|0|";
            com = com + "\n\r" + "Persits.MailSender|http://www.persits.com|Persits - ASPEmail|1|";
            com = com + "\n\r" + "Persits.Upload.1|http://www.persits.com|Persits - ASPUpload|3|";
            com = com + "\n\r" + "Persits.Jpeg|http://www.persits.com|Persits - AspJpeg|4|";
            com = com + "\n\r" + "Persits.Grid|http://www.persits.com|Persits - AspGrid|0|";
            com = com + "\n\r" + "Persits.AspUser|http://www.persits.com|Persits - AspUser|9|";
            com = com + "\n\r" + "Persits.CryptoManager|http://www.persits.com|Persits - AspEncrypt|9|";
            com = com + "\n\r" + "ADISCON.SimpleMail.1|http://www.simplemail.adiscon.com/en|SimpleMail|1|";
            com = com + "\n\r" + "CalendarCom.CalendarStuff|http://www.devguru.com|DevGuru - dgcalendar|0|";
            com = com + "\n\r" + "dgEncrypt.Key|http://www.devguru.com|DevGuru - dgEncrypt|9|";
            com = com + "\n\r" + "dgFileUpload.dgUpload|http://www.devguru.com|DevGuru - dgFileup|3|";
            com = com + "\n\r" + "dgReport.Report|http://www.devguru.com|DevGuru - dgReport|0|";
            com = com + "\n\r" + "dgSort.QuickSort|http://www.devguru.com|DevGuru - dgSort|0|";
            com = com + "\n\r" + "dgTree.Tree|http://www.devguru.com|DevGuru - dgTree|0|";
            com = com + "\n\r" + "Dundas.Mailer|http://www.dundas.com|Dundas - ASPMailer|1|";
            com = com + "\n\r" +
                  "Dundas.PieChartServer.2|http://www.dundas.com|Dundas - Pie Chart Server Control|7|";
            com = com + "\n\r" + "Dundas.Upload|http://www.dundas.com|Dundas - Upload|3|";
            com = com + "\n\r" + "EasyMail.SMTP.5|http://www.quiksoft.com|Quicksoft - EasyMail (free)|1|";
            com = com + "\n\r" + "AspPing.Conn|http://www.15seconds.com/component/pg000229.htm|ASP Ping|0|";
            com = com + "\n\r" + "Dynu.CreditCard|http://www.dynu.com|Dynu CreditCard|10|11";
            com = com + "\n\r" + "Dynu.DateTime|http://www.dynu.com|Dynu DateTime|0|";
            com = com + "\n\r" + "Dynu.DNS|http://www.dynu.com|Dynu DNS|0|";
            com = com + "\n\r" + "Dynu.Exec|http://www.dynu.com|Dynu Exec|0|";
            com = com + "\n\r" + "Dynu.Email|http://www.dynu.com|Dynu Email|1|";
            com = com + "\n\r" + "Dynu.Encrypt|http://www.dynu.com|Dynu Encrypt|9|";
            com = com + "\n\r" + "Dynu.FileUtil|http://www.dynu.com|Dynu File|6|";
            com = com + "\n\r" + "Dynu.FTP|http://www.dynu.com|Dynu FTP|0|6";
            com = com + "\n\r" + "Dynu.HTTP|http://www.dynu.com|Dynu HTTP|0|";
            com = com + "\n\r" + "Dynu.POP3|http://www.dynu.com|Dynu POP3|1|";
            com = com + "\n\r" + "Dynu.Ping|http://www.dynu.com|Dynu Ping|0|";
            com = com + "\n\r" + "Dynu.TCPSocket|http://www.dynu.com|Dynu TCPSocket|0|";
            com = com + "\n\r" + "Dynu.StringUtil|http://www.dynu.com|Dynu String|0|";
            com = com + "\n\r" + "Dynu.Upload|http://www.dynu.com|Dynu Upload|3|";
            com = com + "\n\r" + "Dynu.Wait|http://www.dynu.com|Dynu Wait|0|";
            com = com + "\n\r" + "Dynu.Whois|http://www.dynu.com|Dynu Whois|0|";
            com = com + "\n\r" + "MP_Mikys_ASP.Password|http://www.mikys-asp.nykoping.net/Password|ASP Password|9|";
            com = com + "\n\r" + "S3Weather.Current|http://www.softshell.net|S3 Weather Component (free)|0|";
            com = com + "\n\r" +
                  "AuthNetSSLConnect.SSLPost|http://www.authorize.net|Authorize.Net Transaction COM (free)|10|11";
            com = com + "\n\r" + "HexValidEmail.Connection|http://www.hexillion.com|Hexillion - HexValidEmail|1|11";
            com = com + "\n\r" + "Hexillion.HexIcmp|http://www.hexillion.com|Hexillion - HexIcmp|0|";
            com = com + "\n\r" + "Hexillion.HexLookup|http://www.hexillion.com|Hexillion - HexLookup|0|";
            com = com + "\n\r" + "Hexillion.HexTcpQuery|http://www.hexillion.com|Hexillion - HexTcpQuery|0|";
            com = com + "\n\r" + "HexDns.Connection|http://www.hexillion.com|Hexillion - HexDSN|0|";
            com = com + "\n\r" + "ocxQmail.ocxQmailCtrl.1|http://www.flicks.com|Flicks - ocxQmail|1|";
            com = com + "\n\r" + "OCXHTTP.OCXHttpCtrl.1|http://www.flicks.com|Flicks - OCXHttp|0|";
            com = com + "\n\r" + "ocxQmail.ocxQmailCtrl.1|http://www.flicks.com|Flicks - OCXQMail|1|";
            com = com + "\n\r" + "VASPTV.ASPTreeView|http://www.visualasp.com|VisualASP - TreeView|0|";
            com = com + "\n\r" + "VASPLV.ASPListView|http://www.visualasp.com|VisualASP - ListView|0|";
            com = com + "\n\r" + "VASPMV.ASPMonthView|http://www.visualasp.com|VisualASP - MonthView|0|";
            com = com + "\n\r" + "VASPTB.ASPTabView|http://www.visualasp.com|VisualASP - TabView|0|";
            com = com + "\n\r" + "ASPWordToy.WordToy|http://www.asptoys.com|ASP Toys - WordToy (Word Converter)|6|";
            com = com + "\n\r" + "ASPTabToy.TabToy|http://www.asptoys.com|ASP Toys - TabToy|0|";
            com = com + "\n\r" + "aspZipCodeToy.ZipCodeToy|http://www.asptoys.com|ASP Toys - ASP ZipCodeToy|0|11";
            com = com + "\n\r" + "ASPCryptToy.CryptToy|http://www.asptoys.com|ASP Toys - CryptToy|9|";
            com = com + "\n\r" +
                  "Convert.t2h|http://members.home.net/pjsteele/asp|CONVERT - string/html/text manipulation (free)|0|";
            com = com + "\n\r" + "APDocConv.Object|http://www.activepdf.com|activePDF - DocConverter|5|";
            com = com + "\n\r" + "APWebGrabber.Object|http://www.activepdf.com|activePDF - WebGrabber|5|";
            com = com + "\n\r" + "APServer.Object|http://www.activepdf.com|activePDF - activePDF Server|5|";
            com = com + "\n\r" + "APSpool.Object|http://www.activepdf.com|activePDF - Spooler|5|";
            com = com + "\n\r" + "APToolkit.Object|http://www.activepdf.com|activePDF - Toolkit|5|";
            com = com + "\n\r" + "shotgraph.image|http://www.shotgraph.com|Shot Graph|7|";
            com = com + "\n\r" + "IntrChart.Chart|http://www.compsysaus.com.au|IntrChart|7|";
            com = com + "\n\r" + "IntrSQL.Query|http://www.compsysaus.com.au|IntrSQL|0|";
            com = com + "\n\r" + "IntrPWD.Validate|http://www.compsysaus.com.au|IntrPWD|9|";
            com = com + "\n\r" + "IntrCard.Credit|http://www.compsysaus.com.au|IntrCard|0|11";
            com = com + "\n\r" + "AspSmartImage.SmartImage|http://www.aspsmart.com|ASP Smart - aspSmartImage|4|";
            com = com + "\n\r" + "AspSmartChat.SmartChat|http://www.aspsmart.com|ASP Smart - aspSmartChat|0|";
            com = com + "\n\r" + "AspSmartFile.SmartFile|http://www.aspsmart.com|ASP Smart - aspSmartFile|6|";
            com = com + "\n\r" + "aspSmartMenu.SmartMenuPopUp|http://www.aspsmart.com|ASP Smart - aspSmartMenu|0|";
            com = com + "\n\r" + "AspSmartDate.SmartDate|http://www.aspsmart.com|ASP Smart - aspSmartDate|0|";
            com = com + "\n\r" + "AspSmartUpload.SmartUpload|http://www.aspsmart.com|ASP Smart - aspSmartUpload|3|";
            com = com + "\n\r" + "aspSmartMail.SmartMail|http://www.aspsmart.com|ASP Smart - aspSmartMail|1|";
            com = com + "\n\r" + "aspSmartCache.SmartCache|http://www.aspsmart.com|ASP Smart - aspSmartCache|0|";
            com = com + "\n\r" + "xAuthorize.Charge|http://www.xauthorize.com|xAuthorize CC|10|11";
            com = com + "\n\r" + "acDesktop.Desktop|http://www.activecomponents.nu|acDesktop|0|";
            com = com + "\n\r" + "acNetwork.DNS|http://www.activecomponents.nu|acNetwork|0|";
            com = com + "\n\r" + "acSMTP.Smtp|http://www.activecomponents.nu|acSMTP SSL|9|";
            com = com + "\n\r" +
                  "Temperature.Conversion|http://asp.myscripting.com/activextemp.asp|Temperature Conversion|0|";
            com = com + "\n\r" + "cyScape.browserObj|http://www.cyscape.com|BrowserHawk|2|11";
            com = com + "\n\r" + "dkQmail.Qmail||dkQMail|1|";
            com = com + "\n\r" + "Geocel.Mailer|http://www.geocel.com|GeoCel|1|";
            com = com + "\n\r" + "iismail.iismail.1||IISMail|1|";
            com = com + "\n\r" + "SmtpMail.SmtpMail.1||SMTP|1|";
            com = com + "\n\r" + "OpenX2.Connection|http://www.openx.ca|OpenX|1|";
            com = com + "\n\r" + "ABMailer.Mailman|http://www.absoftwarex.com/abmailer|ABMailer|1|";
            com = com + "\n\r" + "c2geread.Message|http://www.componentstogo.com|C2GEread|1|";
            com = com + "\n\r" + "C2G.SCM|http://www.componentstogo.com|C2GSCM|0|8";
            com = com + "\n\r" + "C2GSCM.Service|http://www.componentstogo.com|C2GSCM|8|0";
            com = com + "\n\r" + "C2G.SCAN|http://www.componentstogo.com|C2GSCAN|0|";
            com = com + "\n\r" + "C2G.whois|http://www.componentstogo.com|C2GWHOIS |0|";
            com = com + "\n\r" + "c2g.http|http://www.componentstogo.com|C2GHttp |0|";
            com = com + "\n\r" + "C2G.Ping|http://www.componentstogo.com|C2GPing|0|";
            com = com + "\n\r" + "C2G.Tracert|http://www.componentstogo.com|C2GTracert|0|";
            com = com + "\n\r" + "ANUPLOAD.OBJ|http://www.adminsystem.net/webapp/popcom|ANPOP|1|";
            com = com + "\n\r" + "ASPXP.Mail|http://aspxp.com/free_stuff/aspxpmail|ASPXPMail (free)|1|";
            com = com + "\n\r" + "ActiveMessenger.Message|http://www.infomentum.com|ActiveMessenger|1|";
            com = com + "\n\r" + "ActiveFile.Post|http://www.infomentum.com|ActiveFile|3|";
            com = com + "\n\r" + "ActiveNavigator.Toolbar|http://www.infomentum.com|ActiveNavigator|0|";
            com = com + "\n\r" + "ActiveProfile.Profile|http://www.infomentum.com|ActiveProfile|2|9";
            com = com + "\n\r" + "DartZip.Zip.1|http://www.dart.com|Dart Zip Compression Tool|6|";
            com = com + "\n\r" + "Dart.Ftp.1|http://www.dart.com|Dart FTP Tool|6|0";
            com = com + "\n\r" + "Dart.Pop.1|http://www.dart.com|Dart POP Mail|1|";
            com = com + "\n\r" + "Dart.Ping.1|http://www.dart.com|Dart Ping|0|";
            com = com + "\n\r" + "Dart.Dns.1|http://www.dart.com|Dart DNS|0|";
            com = com + "\n\r" + "Dart.Smtp.1|http://www.dart.com|Dart SMTP|1|";
            com = com + "\n\r" + "Dart.Telnet.1|http://www.dart.com|Dart PowerTCP Telnet Tool|0|";
            com = com + "\n\r" + "Dart.Http.1|http://www.dart.com|Dart HTTP|0|";
            com = com + "\n\r" + "Dart.Tcp.1|http://www.dart.com|Dart TCP|0|";
            com = com + "\n\r" + "Dart.WebPage.1|http://www.dart.com|Dart WebPage|0|";
            com = com + "\n\r" + "Dart.WebASP.1|http://www.dart.com|Dart ASP|0|";
            com = com + "\n\r" + "Dart.Message.1|http://www.dart.com|Dart Message|0|";
            com = com + "\n\r" + "Dart.Manager.1|http://www.dart.com|Dart Manager|0|";
            com = com + "\n\r" + "quicktab.quicktabs|http://www.webintel.net|Quicktab|0|";
            com = com + "\n\r" + "waspzip.waspzip|http://www.webintel.net|Wasp Zip|6|5";
            com = com + "\n\r" + "easyBarCode.aspBarCode|http://www.mitdata.com|aspEasyBarCode|7|0";
            com = com + "\n\r" + "aspZip.EasyZIP|http://www.mitdata.com|aspEasyZIP|6|5";
            com = com + "\n\r" + "aspPDF.EasyPDF|http://www.mitdata.com|aspEasyPDF|5|6";
            com = com + "\n\r" + "aspCrypt.EasyCRYPT|http://www.mitdata.com|aspEasyCRYPT|9|";
            com = com + "\n\r" + "objBarGraph.DrawChart|http://www.livesoup.com/bargraph.asp|BarGraph (free)|7|";
            com = com + "\n\r" + "LyfUpload.UploadFile|http://www.21jsp.com|LyfUpload (free)|3|";
            com = com + "\n\r" + "lyfimage.image|http://www.21jsp.com|LyfImage (free)|4|7";
            com = com + "\n\r" + "ASPControlHost.Host|http://release-systems.8m.com/asphost.html|ASPControlHost|7|4";
            com = com + "\n\r" + "GSServer.GSServerProp|http://www.graphicsserver.com|Graphics Server|4|7";
            com = com + "\n\r" + "ASPPicture.Picture|http://www.unchanged.net|ASPPicture|4|";
            com = com + "\n\r" +
                  "COMobjectsNET.IconGrabber|http://www.comobjects.net|COMobjects.NET Icon Grabber|4|";
            com = com + "\n\r" +
                  "COMobjects.NET.PictureProcessor|http://www.comobjects.net|COMobjects.NET Picture Processor|4|";
            com = com + "\n\r" +
                  "COMobjectsNET.PictureGalleryPro|http://www.comobjects.net|COMobjects.NET Picture Gallery Pro|4|";
            com = com + "\n\r" + "COMobjectsNET.Colorizer|http://www.comobjects.net|COMobjects.NET Colorizer|4|";
            com = com + "\n\r" + "COMobjectsNET.PieChart|http://www.comobjects.net|COMobjects.NET 3D Pie Chart|7|4";
            com = com + "\n\r" + "ChartDirector.API|http://www.advsofteng.com|ChartDirector|7|";
            com = com + "\n\r" + "Stonebroom.ASPointer|http://www.stonebroom.com|Stonebroom.ASPointer|13|5";
            com = com + "\n\r" + "Stonebroom.ASP2XML|http://www.stonebroom.com|Stonebroom.ASP2XML|13|5";
            com = com + "\n\r" + "Stonebroom.RegEx|http://www.stonebroom.com|Stonebroom.RegEx|0|";
            com = com + "\n\r" + "Stonebroom.RemoteZip|http://www.stonebroom.com|Stonebroom.RemoteZip|5|6";
            com = com + "\n\r" + "Stonebroom.SaveForm|http://www.stonebroom.com|Stonebroom.SaveForm|12|";
            com = com + "\n\r" + "Stonebroom.ServerZip|http://www.stonebroom.com|Stonebroom.ServerZip|5|6";
            com = com + "\n\r" + "Stonebroom.XSLTransform|http://www.stonebroom.com|Stonebroom.XSLTransform|13|5";
            com = com + "\n\r" + "OpenX.DBMail|http://www.openx.ca|OpenX DBMail|1|12";
            com = com + "\n\r" + "com.comsoltech.CGI|http://www.comsoltech.com|com.comsoltech.CGI (free)|12|";
            com = com + "\n\r" + "Datafun.FormBoy|http://www.datafun.net|FormBoy|12|10";
            com = com + "\n\r" + "AddressTools.ZIPCheck|http://www.addresstools.com|AddressTools - ZIPCheck|11|12";
            com = com + "\n\r" +
                  "AddressTools.EmailCheck|http://www.addresstools.com|AddressTools - EmailCheck|11|12";
            com = com + "\n\r" + "VisualSoft.Mail.1|http://www.visualmart.com|VisualSoft Mail|1|";
            com = com + "\n\r" + "VisualSoft.BLOWFISHCrypt.1|http://www.visualmart.com|VisualSoft Crypt|9|";
            com = com + "\n\r" + "VisualSoft.FTP.1|http://www.visualmart.com|VisualSoft FTP|6|0";
            com = com + "\n\r" + "VisualSoft.HTTP.1|http://www.visualmart.com|VisualSoft HTTP|2|0";
            com = com + "\n\r" + "VisualSoft.Chart.1|http://www.visualmart.com|VisualSoft Chart|7|";
            com = com + "\n\r" + "VisualSoft.DMXML.1|http://www.visualmart.com|VisualSoft XMLPro|13|";
            com = com + "\n\r" + "VisualSoft.DataAdmin.1|http://www.visualmart.com|VisualSoft DataAdmin|0|";
            com = com + "\n\r" + "QwerkSoft.FormSlam|http://www.qwerksoft.com|Form Slam|12|11";
            com = com + "\n\r" + "SiteAdmin.AdminTools|http://components.sitetown.com|SiteSecurity|9|";
            com = com + "\n\r" + "SiteSecurity.Login|http://components.sitetown.com|SiteSecurity|9|";
            com = com + "\n\r" + "FileDownload.Manager|http://components.sitetown.com|File Download|6|0";
            com = com + "\n\r" + "EasyDb.Database|http://components.sitetown.com|Easy DB|0|";
            com = com + "\n\r" + "AbsoluteHttp.Conn|http://www.speeq.com|AbsoluteHTTP|0|";
            com = com + "\n\r" + "ASPCharge.CC|http://www.bluesquirrel.com|A$PCharge|10|11";
            com = com + "\n\r" + "ProjectDisplay.Charts|http://www.aspkey.com|ASPkey ProjectDisplay|0|";
            com = com + "\n\r" + "IPWorksASP.SOAP|www.nsoftware.com|IP Works Soap|13|";
            com = com + "\n\r" + "IPWorksASP.FileMailer|www.nsoftware.com|IP Works FileMailer|1|6";
            com = com + "\n\r" + "IPWorksASP.FTP|www.nsoftware.com|IP Works FTP|0|";
            com = com + "\n\r" + "IPWorksASP.HTMLMailer|www.nsoftware.com|IP Works HTMLMailer|1|";
            com = com + "\n\r" + "IPWorksASP.HTTP|www.nsoftware.com|IP Works HTTP|13|0";
            com = com + "\n\r" + "IPWorksASP.ICMPPort|www.nsoftware.com|IP Works ICMPPort|0|";
            com = com + "\n\r" + "IPWorksASP.IMAP|www.nsoftware.com|IP Works IMAP|0|";
            com = com + "\n\r" + "IPWorksASP.IPInfo|www.nsoftware.com|IP Works IPInfo|0|";
            com = com + "\n\r" + "IPWorksASP.IPPort|www.nsoftware.com|IP Works IPPort|0|";
            com = com + "\n\r" + "IPWorksASP.LDAP|www.nsoftware.com|IP Works LDAP|0|";
            com = com + "\n\r" + "IPWorksASP.MCast|www.nsoftware.com|IP Works MCast|0|";
            com = com + "\n\r" + "IPWorksASP.MIME|www.nsoftware.com|IP Works MIME|1|";
            com = com + "\n\r" + "IPWorksASP.MX|www.nsoftware.com|IP Works MX|1|";
            com = com + "\n\r" + "IPWorksASP.NetClock|www.nsoftware.com|IP Works NetClock|0|";
            com = com + "\n\r" + "IPWorksASP.NetCode|www.nsoftware.com|IP Works NetCode|0|";
            com = com + "\n\r" + "IPWorksASP.NetDial|www.nsoftware.com|IP Works NetDial|0|";
            com = com + "\n\r" + "IPWorksASP.NNTP|www.nsoftware.com|IP Works NNTP|0|";
            com = com + "\n\r" + "IPWorksASP.Ping|www.nsoftware.com|IP Works Ping|0|";
            com = com + "\n\r" + "IPWorksASP.POP|www.nsoftware.com|IP Works POP|1|";
            com = com + "\n\r" + "IPWorksASP.RCP|www.nsoftware.com|IP Works RCP|6|0";
            com = com + "\n\r" + "IPWorksASP.Rexec|www.nsoftware.com|IP Works Rexec|0|";
            com = com + "\n\r" + "IPWorksASP.Rshell|www.nsoftware.com|IP Works Rshell|0|";
            com = com + "\n\r" + "IPWorksASP.SMTP|www.nsoftware.com|IP Works SMTP|1|";
            com = com + "\n\r" + "IPWorksASP.SNMP|www.nsoftware.com|IP Works SNMP|1|0";
            com = com + "\n\r" + "IPWorksASP.SNPP|www.nsoftware.com|IP Works SNPP|13|0";
            com = com + "\n\r" + "IPWorksASP.Telnet|www.nsoftware.com|IP Works Telnet|0|";
            com = com + "\n\r" + "IPWorksASP.TFTP|www.nsoftware.com|IP Works TFTP|0|";
            com = com + "\n\r" + "IPWorksASP.TraceRoute|www.nsoftware.com|IP Works TraceRoute|0|";
            com = com + "\n\r" + "IPWorksASP.UDPPort|www.nsoftware.com|IP Works UDPPort|0|";
            com = com + "\n\r" + "IPWorksASP.WebForm|www.nsoftware.com|IP Works WebForm|12|";
            com = com + "\n\r" + "IPWorksASP.WebUpload|www.nsoftware.com|IP Works WebUpload|3|";
            com = com + "\n\r" + "IPWorksASP.Whois|www.nsoftware.com|IP Works Whois|0|";
            com = com + "\n\r" + "IPWorksASP.XMLp|www.nsoftware.com|IP Works XMLp|13|";
            com = com + "\n\r" + "iisCC.cc|http://www.iiscart.com|IIS Cart - iisCARTcc|0|11";
            com = com + "\n\r" + "Coalesys.CSPanelBar.2|http://www.coalesys.com|CSPanelBar|0|";
            com = com + "\n\r" + "Coalesys.CSWebMenu.1|http://www.coalesys.com|CSWebMenu|0|3";
            com = com + "\n\r" + "TCPIP.DNS|http://www.pstruh.cz/help/tcpip/library.htm|Simple DNS+Traceroute|0|";
            com = com + "\n\r" + "DrWFM.fm|http://www.dataroad.sk/dr/drwfm/default.asp|DrWebFileManager|6|";
            com = com + "\n\r" + "id3.id3get|http://www.infinitemonkeys.ws/infinitemonkeys|Atrax ID3.ID3Get|0|";
            com = com + "\n\r" + "Atrax.ComboBox|http://www.infinitemonkeys.ws/infinitemonkeys|Atrax ComboBox|0|";
            com = com + "\n\r" +
                  "Atrax.URLGrabber|http://www.infinitemonkeys.ws/infinitemonkeys|Atrax URLGrabber|0|13";
            com = com + "\n\r" + "Atrax.Whois|http://www.infinitemonkeys.ws|Atrax Whois|0|";
            com = com + "\n\r" + "SOFTWING.ASPEventlog|http://www.alphasierrapapa.com|Asp Event log (FREE)|8|0";
            com = com + "\n\r" + "Softwing.EventLogReader|http://www.alphasierrapapa.com|Event Log Reader (FREE)|0|";
            com = com + "\n\r" + "Softwing.AspQPerfCounters|http://www.alphasierrapapa.com|AspQPerfCounters|8|0";
            com = com + "\n\r" + "SOFTWING.AspTear|http://www.alphasierrapapa.com|AspTear|8|0";
            com = com + "\n\r" + "AspTouch.TouchIt|http://www.alphasierrapapa.com|AspTouch TouchIt (FREE)|8|0";
            com = com + "\n\r" + "Softwing.FileCache.1|http://www.alphasierrapapa.com|Softwing FileCache (FREE)|8|0";
            com = com + "\n\r" + "Softwing.LocaleFormatter|http://www.alphasierrapapa.com|LocaleFormatter (FREE)|0|";
            com = com + "\n\r" + "Softwing.MacBinary|http://www.alphasierrapapa.com|MacBinary Xtraction (FREE)|6|";
            com = com + "\n\r" + "Softwing.OdbcRegTool|http://www.alphasierrapapa.com|OdbcRegTool (FREE)|8|0";
            com = com + "\n\r" +
                  "Softwing.Profiler|http://www.alphasierrapapa.com|Softwing ASP Script Speed Profiler (FREE)|0|";
            com = com + "\n\r" +
                  "AlphaSierraPapa.AspRegSvr|http://www.alphasierrapapa.com|RegServer [component registration via ASP] (FREE!!)|8|0";
            com = com + "\n\r" + "Softwing.VersionInfo|http://www.alphasierrapapa.com|VersionInfo|8|0";
            com = com + "\n\r" + "w3info.w3info.1|http://www.alphasierrapapa.com|W3 Info|0|";
            com = com + "\n\r" + "SoftwingXSB.ShoppingBag|http://www.alphasierrapapa.com|Softwing ShoppingBag|10|";
            com = com + "\n\r" + "crossoft.quickcal|http://www.quickgallery.com|Quick Calendar|0|";
            com = com + "\n\r" + "crossoft.wapsplash|http://www.quickgallery.com|QuickDeck|0|";
            com = com + "\n\r" + "crossoft.waplist|http://www.quickgallery.com|QuickDeck|0|";
            com = com + "\n\r" + "crossoft.remotescript|http://www.quickgallery.com|QuickList|0|";
            com = com + "\n\r" + "crossoft.quicklist|http://www.quickgallery.com|QuickList|0|";
            com = com + "\n\r" + "crossoft.quicktable|http://www.quickgallery.com|QuickTable|0|";
            com = com + "\n\r" + "OneTouchASP.StrFunctions|http://www.1touchasp.com|1Touch|0|";
            com = com + "\n\r" + "ZmeYsoft.Hashes.MD5|http://www.newobjects.com|ZmeYsoft MD5 Hash|9|0";
            com = com + "\n\r" + "binarysendfile.BinFileSend|http://www.newobjects.com|Binarysendfile component|0|";
            com = com + "\n\r" + "werkslib.mp3exp|http://www.marban.at/download/aspmp3.zip|werk3AT - MP3|0|";
            com = com + "\n\r" + "TreeGen.Tree|http://www.treegen.com|Tree Gen|0|";
            com = com + "\n\r" + "Text2Tree150d.tree|http://www.asp-components.de|Text2Tree|0|";
            com = com + "\n\r" + "ASPBarChart100d.chart|http://www.asp-components.de|Bar Chart|8|0";
            com = com + "\n\r" + "AspWebCal120d.webcal|http://www.asp-components.de|ASP WebCalendar|0|";
            com = com + "\n\r" + "ScriptUtils.ASPForm|http://pstruh.cz/help/ScptUtl/library.htm|Simple Upload|3|0";
            com = com + "\n\r" +
                  "ScriptUtils.ByteArray|http://pstruh.cz/help/ScptUtl/library.htm|Simple Download|0|";
            com = com + "\n\r" + "ScriptUtils.Kernel|http://pstruh.cz/help/ScptUtl/library.htm|ASP Timing|0|";
            com = com + "\n\r" + "Scribe.ScribeDOM|http://www.innuvo.com|ScribeDOM|13|";
            com = com + "\n\r" + "ANPOP.POPMSG|http://www.adminsystem.net|ANPOP |1|";
            com = com + "\n\r" + "ANSMTP.OBJ|http://www.adminsystem.net|ANSMTP|1|";
            com = com + "\n\r" + "ANUPLOAD.OBJ|http://www.adminsystem.net|ANUPLOAD (free)|3|";
            com = com + "\n\r" + "VoiceShot.VoiceShot|http://www.voiceshot.com/api/readme.htm|ASP Call|0|";
            com = com + "\n\r" + "SimplePageASP.SNPP|http://www.rushweb.com|SimplePageASP SNPP|0|";
            com = com + "\n\r" + "khttp.inet|http://www.rainfall.com|KHTTP|13|0";
            com = com + "\n\r" + "OCXHTTP.OCXHttpCtrl.1|http://www.flicks.com|Flicks OCXHttp|13|0";
            com = com + "\n\r" + "URLFetch.URLFetch|http://www.screen-scraper.com|URLFetch|13|0";
            com = com + "\n\r" + "Dundas.Mailer|http://www.dundas.com|Dundas Mailer|1|";
            com = com + "\n\r" + "Dundas.Mailer.1|http://www.dundas.com|Dundas Mailer|1|";
            com = com + "\n\r" + "Dundas.PieChartServer.1|http://www.dundas.com|Dundas PieChartServer|7|";
            com = com + "\n\r" + "Dundas.Upload|http://www.dundas.com|Dundas Upload|3|";
            com = com + "\n\r" + "Dundas.Upload.2|http://www.dundas.com|Dundas Upload|3|";
            com = com + "\n\r" + "Dundas.ChartServer|http://www.dundas.com|Dundas ChartServer|7|";
            com = com + "\n\r" + "Dundas.ChartServer2D.1|http://www.dundas.com|Dundas ChartServer 2D|7|";
            com = com + "\n\r" + "ABCUpload4.XForm|http://www.websupergoo.com|ABC Upload|3|";
            com = com + "\n\r" + "ABCpdf3.Doc|http://www.websupergoo.com|ABC PDF|0|";
            com = com + "\n\r" + "ImageGlue5.Canvas|http://www.websupergoo.com|Image Glue|4|";
            com = com + "\n\r" + "ImageEffects.FX|http://www.websupergoo.com|Image Effects|4|";
            com = com + "\n\r" + "ABCDrawHTML.Page|http://www.websupergoo.com|ABC Draw HTML|4|0";
            com = com + "\n\r" + "ABCCrypto2.Crypto|http://www.websupergoo.com|ABC Crypto|9|";
            com = com + "\n\r" + "MetaFiler2.File|http://www.websupergoo.com|MetaFiler|4|";
            com = com + "\n\r" + "XceedSoftware.XceedZip|http://www.xceedsoft.com|XceedZip|5|";
            com = com + "\n\r" + "Xceed.BinaryEncoding|http://www.xceedsoft.com|Xceed Binary Encoding|0|";
            com = com + "\n\r" + "Xceed.Base64Encoding|http://www.xceedsoft.com|Xceed Base 64 Encoding|0|";
            com = com + "\n\r" + "Xceed.Encryption|http://www.xceedsoft.com|Xceed Encryption|9|0";
            com = com + "\n\r" +
                  "Xceed.TwofishEncryptionMethod|http://www.xceedsoft.com|Xceed Two fish Encryption Method|9|0";
            com = com + "\n\r" + "Xceed.HavalHashingMethod|http://www.xceedsoft.com|Xceed Haval Hashing Method|9|0";
            com = com + "\n\r" + "XceedSoftware.XceedFtp|http://www.xceedsoft.com|Xceed Ftp|8|0";
            com = com + "\n\r" +
                  "Xceed.StreamingCompression|http://www.xceedsoft.com|Xceed Streaming Compression|0|";
            com = com + "\n\r" + "Xceed.DeflateCompression|http://www.xceedsoft.com|Xceed Deflate Compression|0|";

            comA = com.Split("\n\r".Split("".ToCharArray()), StringSplitOptions.None);


            cat = "Miscellaneous";
            cat = cat + "|Email";
            cat = cat + "|Browser";
            cat = cat + "|Upload";
            cat = cat + "|Image";
            cat = cat + "|Documents";
            cat = cat + "|File Management";
            cat = cat + "|Graphs & Charts";
            cat = cat + "|Server Management";
            cat = cat + "|Users & Security";
            cat = cat + "|E-Commerce";
            cat = cat + "|Validation";
            cat = cat + "|Forms";
            cat = cat + "|XML";

            catA = cat.Split("|".Split("".ToCharArray()), StringSplitOptions.None);

            show = (NumberUtils.IsNumeric(Request.QueryString["show"])) ? int.Parse(Request.QueryString["show"]) : 1;
            if ((show > 3))
            {
                show = 1;
            }
            if ((NumberUtils.IsNumeric(Request.QueryString["showCat"]) & Request["showCat"] != ""))
            {
                showCat = int.Parse(Request.QueryString["showCat"]);
            }
            else
            {
                showCat = 0;
            }

            if ((showCat > catA.GetUpperBound(0)))
            {
                showCat = 0;
            }

            //string checkVersion = GetHtml("http://www.pensaworks.com/tutorials/com_version.asp");
            //if ((checkVersion != lastUpdate))
            //{
            //    //newVersion = true; 
            //}
        }


        public bool IsObjInstalled(string strClassString)
        {
            try
            {
                object testObj = Server.CreateObject(strClassString);
                return testObj != null;
            }
            catch
            {
                return false;
            }
        }
    }


    public class Program
    {
        public string clsId;
        public string dllName;
        public string description;
        public string path;
        public string progId;
        public string typeLib;
        public string version;
    }


    public class ProgIdInfo
    {

        public object LoadProgId(object sProgramId)
        {
            //object loadProgIdReturn = null;
            ////object sTmpProg = null; object oTmp = null; string sRegBase = null; string sDesc = null; string sClsID = null; 
            ////string sPath = null; string sTypeLib = null; string sProgID = null; string sVers = null; string sPathSpec = null; 
            ////if ( !( WshShell == null ) ) 
            ////{ 
            ////    sCVProgID = WshShell.RegRead( @"HKCR\" + sProgramID + @"\CurVer\" ); 
            ////    sTmpProg = IIf( Information.Err().Number == 0, sCVProgID, sProgramID ); 

            ////    sRegBase = @"HKCR\" + sTmpProg; 
            ////    sDesc = WshShell.RegRead( sRegBase + @"\" ); 
            ////    sClsID = WshShell.RegRead( sRegBase + @"\clsid\" ); 
            ////    sRegBase = @"HKCR\CLSID\" + sClsID; 
            ////    sPath = WshShell.RegRead( sRegBase + @"\InprocServer32\" ); 
            ////    sPath = WshShell.ExpandEnvironmentStrings( sPath ); 
            ////    sTypeLib = WshShell.RegRead( sRegBase + @"\TypeLib\" ); 
            ////    sProgID = WshShell.RegRead( sRegBase + @"\ProgID\" ); 
            ////    sVers = oFSO.getFileVersion( sPath ); 
            ////    sPathSpec =   Text.Substring( sPath, sPath.Length - Functions.Len( sPath ) - Functions.InStrRev( sPath, @"\" ) ); 

            ////    oTmp = new Program(); 
            ////    oTmp.Description = sDesc; 
            ////    oTmp.ClsID = IIf( sClsID != "", sClsID, "undetermined" ); 
            ////    oTmp.Path = IIf( sPath != "", sPath, "undetermined" ); 
            ////    oTmp.TypeLib = IIf( sTypeLib != "", sTypeLib, "undetermined" ); 
            ////    oTmp.ProgID = IIf( sProgID != "", sProgID, "undetermined" ); 
            ////    oTmp.DLLName = IIf( sPathSpec != "", sPathSpec, "undetermined" ); 
            ////    oTmp.Version = IIf( sVers != "", sVers, "undetermined" ); 
            ////    loadProgIDReturn = oTmp; 
            ////} 
            ////else 
            ////{ 
            ////    loadProgIDReturn = null; 
            ////} 
            return null;
        }
    }
}