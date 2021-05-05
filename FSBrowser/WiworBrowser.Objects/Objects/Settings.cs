namespace WiworBrowser.Objects
{
	using System;
    using System.Collections.Generic;
	
	/// <summary>
	/// Settings class
	/// </summary>
	public class Settings
	{
        public string HomePage { get; set; }
        public string UpdatePage { get; set; }
        public bool MenuBar { get; set; }
        public bool AdressBar { get; set; }
        public bool LinkBar { get; set; }
        public bool FavoritesPanel { get; set; }
        public bool SplashScreen { get; set; }
        public int DropDown { get; set; }

        public bool ProtectWithPassword { get; set; }
        public string SettingsPassword { get; set; }

        public bool AllowNewWindow { get; set; }
        public bool AllowFileDownload { get; set; }
        public bool ScriptErrorsSuppressed { get; set; }
        public bool AllowIFrames { get; set; }
        public bool RemoveFlashBanner { get; set; }
        public bool CheckIsChildValidPage { get; set; }
        public bool RemoveContextMenu { get; set; }
        public bool AllowImageExternalLinks { get; set; }
        public bool WebBrowserShortcutsEnabled { get; set; }
        public bool IsWebBrowserContextMenuEnabled { get; set; }
        public bool CheckContent { get; set; }
        public bool PasswordOnExit { get; set; }

	    public List<string> BadWords = new List<string>();
	    public List<string> BlackList = new List<string>();
	}
}
