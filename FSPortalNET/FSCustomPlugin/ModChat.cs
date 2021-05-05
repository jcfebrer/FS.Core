using FSPlugin;
using FSPortal;

namespace FSCustomPlugin
{
    public class ModChat : IPlugin
    {
        public string Execute(params string[] p)
        {
            return Chat();
        }

        public string Name
        {
            get { return "ModChat"; }
        }

        public int Parameters
        {
            get { return 0; }
        }


        public static string Chat()
        {
            string modChatReturn = "<form action='" + Variables.App.directorioPortal +
                            "chat/chatWin.aspx' id='frmChat' name='frmChat' method='post' onsubmit='return checkField(frmChat.userName.value);'>" +
                            "\r\n";
            modChatReturn = modChatReturn + @"Sala: <input type=""text"" size=""10"" name=""Channel"" />" + Ui.Lf();
            modChatReturn = modChatReturn + @"Usuario: <input type=""text"" size=""10"" name=""userName"" />" + Ui.Lf() +
                            "<input type='submit' class='botonplano' value='Entrar!' />" + "\r\n";
            modChatReturn = modChatReturn + "</form>" + "\r\n";
            return modChatReturn;
        }
    }
}