using System;
using System.Xml;
using System.Text;
using WiworBrowser.Objects;
using System.IO;
using System.Reflection;
using System.Collections.Generic;


namespace WiworBrowser.Common
{
    /// <summary>
    /// Application configuration
    /// </summary>
    public static class Configuration
    {
        // configuration file name
        private static string SettingsFile;
        private static string FavoritesFile;
        private static string HistoryFile;

        private static string BlackListZipFile;
        private static string BlackListTxtFile;


        // Favorites groups
        static public readonly GroupCollection FavoritesGroups = new GroupCollection();

        // Favorites
        static public readonly FavoriteCollection Favorites = new FavoriteCollection();

        // History
        static public readonly HistoryCollection Historys = new HistoryCollection();

        // Settings
        static public readonly Settings Settings = new Settings();

        // Constructor
        static Configuration()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().FullName);

            SettingsFile = Path.Combine(path, "settings.xml");
            FavoritesFile = Path.Combine(path, "favorites.xml");
            HistoryFile = Path.Combine(path, "history.xml");
            BlackListZipFile = Path.Combine(path, "blacklist.zfs");
            BlackListTxtFile = Path.Combine(path, "blacklist.txt");
        }

        #region "Favoritos"

        // Add new Favorite group
        static public void AddFavoritesGroup(Group group)
        {
            FavoritesGroups.Add(group);

            // save
            SaveFavorites();
        }

        // Get Favorites group by name
        static public Group GetFavoritesGroupByName(string name)
        {
            return FavoritesGroups.GetGroupByName(name);
        }

        // Check Favorite group
        // check if there is already a group with such name
        // return true, if there is no such group
        static public bool CheckFavoritesGroup(Group group)
        {
            foreach (Group g in FavoritesGroups)
            {
                if ((group.Name == g.Name) && (group.Parent == g.Parent))
                    return false;
            }
            return true;
        }

        static public int TotalFavorites(Group group)
        {
            int total = 0;
            foreach (Favorite f in Favorites)
            {
                if (f.Parent == group) total++;
            }

            return total;
        }

        static public int TotalGroups(Group group)
        {
            int total = 0;
            foreach (Group fg in FavoritesGroups)
            {
                if (fg.Parent == group) total++;
            }

            return total;
        }

        // Delete Favorites group
        // delete Favorites group if it is empty or return false otherwise
        static public bool DeleteFavoritesGroup(Group group, bool deleteFavorites)
        {
            // check if there are subgroups in the group
            foreach (Group g in FavoritesGroups)
            {
                if (g.Parent == group)
                    return false;
            }


            if (deleteFavorites)
            {
                DeleteFavorites(group);
            }
            else
            {
                // check if there are Favorites in the group
                foreach (Favorite c in Favorites)
                {
                    if (c.Parent == group)
                    {
                        return false;
                    }
                }
            }

            FavoritesGroups.Remove(group);
            // save
            SaveFavorites();
            return true;
        }

        // Add new Favorite
        static public void AddFavorite(Favorite favorite)
        {
            Favorites.Add(favorite);

            // save
            SaveFavorites();
        }

        // Get Favorite by name
        static public Favorite GetFavoriteByName(string name)
        {
            string[] nameParts = name.Split('\\');
            Group group = null;

            // get group
            if (nameParts.Length > 1)
                group = FavoritesGroups.GetGroupByName(string.Join("\\", nameParts, 0, nameParts.Length - 1));

            // get Favorite
            return Favorites.GetFavorite(nameParts[nameParts.Length - 1], group);
        }

        // Check Favorite
        // check if there is already a Favorite with such name
        // return true, if there is no such Favorite
        static public bool CheckFavorite(Favorite favorite)
        {
            foreach (Favorite c in Favorites)
            {
                if ((favorite.Url == c.Url) && (favorite.Parent == c.Parent))
                    return false;
            }
            return true;
        }

        // Delete Favorites
        static public bool DeleteFavorites()
        {
            Favorites.Clear();
            // save
            SaveFavorites();
            return true;
        }

        // Delete Favorite
        static public bool DeleteFavorite(Favorite favorite)
        {
            Favorites.Remove(favorite);
            // save
            SaveFavorites();
            return true;
        }


        // Delete group Favorites
        static public void DeleteFavorites(Group group)
        {
            int totalFav = Favorites.Count -1;

            for (int i = totalFav; i >= 0; i--)
            {
                if(Favorites[i].Parent == group)
                {
                    Favorites.Remove(Favorites[i]);
                }
            }
        }

        // Save Favorites list to file
        static public void SaveFavorites()
        {
            // open file
            FileStream fs = new FileStream(FavoritesFile, FileMode.Create);
            // create XML writer
            XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.UTF8);

            // use indenting for readability
            xmlOut.Formatting = Formatting.Indented;

            // start document
            xmlOut.WriteStartDocument();

            // main node
            xmlOut.WriteStartElement("Favorites");

            // save all Favorites
            SaveFavorites(xmlOut, null);

            // close "Favorites" node
            xmlOut.WriteEndElement();

            // close file
            xmlOut.Close();
        }

        // save Favorites of the parent group
        static private void SaveFavorites(XmlTextWriter writer, Group parent)
        {
            foreach (Group group in FavoritesGroups)
            {
                if (group.Parent == parent)
                {
                    // new "Group" node
                    writer.WriteStartElement("Group");

                    // write node
                    writer.WriteAttributeString("name", group.Name);
                    writer.WriteAttributeString("desc", group.Description);

                    // write child groups
                    SaveFavorites(writer, group);

                    // close "Group" node
                    writer.WriteEndElement();
                }
            }
            foreach (Favorite favorite in Favorites)
            {
                if (favorite.Parent == parent)
                {
                    // new "Favorite" node
                    writer.WriteStartElement("Favorite");

                    // write node
                    writer.WriteAttributeString("url", favorite.Url);
                    writer.WriteAttributeString("desc", favorite.Description);

                    // close "Favorite" node
                    writer.WriteEndElement();
                }
            }
        }

        // Load Favorites collection from file
        static public void LoadFavorites()
        {
            // check file existance
            if (File.Exists(FavoritesFile))
            {
                FileStream fs = null;
                XmlTextReader xmlIn = null;

                try
                {
                    // open file
                    fs = new FileStream(FavoritesFile, FileMode.Open);
                    // create XML reader
                    xmlIn = new XmlTextReader(fs);

                    xmlIn.WhitespaceHandling = WhitespaceHandling.None;
                    xmlIn.MoveToContent();

                    // check for main node
                    if (xmlIn.Name.ToLower() != "favorites")
                        throw new ApplicationException("");

                    // move to next node
                    xmlIn.Read();
                    if (xmlIn.NodeType == XmlNodeType.EndElement)
                        xmlIn.Read();

                    // load Favorites
                    LoadFavorites(xmlIn, null);
                }
                // catch any exceptions
                catch (Exception)
                {
                }
                finally
                {
                    if (xmlIn != null)
                        xmlIn.Close();
                }
            }
        }

        /// <summary>
        /// Cargamos los favoritos y sus categorias
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="parent"></param>
        static private void LoadFavorites(XmlTextReader reader, Group parent)
        {
            // load all groups
            while (reader.Name.ToLower() == "group")
            {
                int depth = reader.Depth;

                // create new group
                Group group = new Group(reader.GetAttribute("name"));
                group.Description = reader.GetAttribute("desc");
                group.Parent = parent;

                // add group
                FavoritesGroups.Add(group);

                // move to next node
                reader.Read();

                // move to next element node
                while (reader.NodeType == XmlNodeType.EndElement)
                    reader.Read();
                // read children
                if (reader.Depth > depth)
                    LoadFavorites(reader, group);
                //
                if (reader.Depth < depth)
                    return;
            }
            // load all Favorites
            while (reader.Name.ToLower() == "favorite")
            {
                int depth = reader.Depth;

                // create new Favorite
                Favorite Favorite = new Favorite(reader.GetAttribute("url"));

                Favorite.Description = reader.GetAttribute("desc");
                Favorite.Parent = parent;

                // add Favorite
                Favorites.Add(Favorite);

                // move to next node
                reader.Read();

                // move to next element node
                while (reader.NodeType == XmlNodeType.EndElement)
                    reader.Read();
                if (reader.Depth < depth)
                    return;
            }
        }

        #endregion

        #region "Historys"

        // Add new History
        static public int AddHistory(History history)
        {
            int times = 1;
            History his = Historys.GetHistory(history.Url);
            if (his != null)
                times = his.Times++;
            else
            {
                Historys.Add(history);
            }

            // save
            SaveHistorys();

            return times;
        }

        // Delete all Historys
        static public bool DeleteHistorys()
        {
            Historys.Clear();
            // save
            SaveHistorys();
            return true;
        }

        // Delete Favorite
        static public bool DeleteHistory(History history)
        {
            Historys.Remove(history);
            // save
            SaveHistorys();
            return true;
        }

        // Save Historys list to file
        static public void SaveHistorys()
        {
            // open file
            FileStream fs = new FileStream(HistoryFile, FileMode.Create);
            // create XML writer
            XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.UTF8);

            // use indenting for readability
            xmlOut.Formatting = Formatting.Indented;

            // start document
            xmlOut.WriteStartDocument();

            // main node
            xmlOut.WriteStartElement("History");

            // save all Historys
            SaveHistorys(xmlOut, null);

            // close "History" node
            xmlOut.WriteEndElement();

            // close file
            xmlOut.Close();
        }

        // save Historys
        static private void SaveHistorys(XmlTextWriter writer, Group parent)
        {
            foreach (History history in Historys)
            {
                // new "Item" node
                writer.WriteStartElement("Item");

                // write node
                writer.WriteAttributeString("url", history.Url);
                writer.WriteAttributeString("lastVisited", history.LastVisited.ToString());
                writer.WriteAttributeString("times", history.Times.ToString());

                // close "Item" node
                writer.WriteEndElement();
            }
        }

        // Load History collection from file
        static public void LoadHistory()
        {
            // check file existance
            if (File.Exists(HistoryFile))
            {
                FileStream fs = null;
                XmlTextReader xmlIn = null;

                try
                {
                    // open file
                    fs = new FileStream(HistoryFile, FileMode.Open);
                    // create XML reader
                    xmlIn = new XmlTextReader(fs);

                    xmlIn.WhitespaceHandling = WhitespaceHandling.None;
                    xmlIn.MoveToContent();

                    // check for main node
                    if (xmlIn.Name.ToLower() != "history")
                        throw new ApplicationException("");

                    // move to next node
                    xmlIn.Read();
                    if (xmlIn.NodeType == XmlNodeType.EndElement)
                        xmlIn.Read();

                    // load History
                    LoadHistory(xmlIn, null);
                }
                // catch any exceptions
                catch (Exception)
                {
                }
                finally
                {
                    if (xmlIn != null)
                        xmlIn.Close();
                }
            }
        }

        // Load History
        static private void LoadHistory(XmlTextReader reader, Group parent)
        {
            // load all History
            while (reader.Name.ToLower() == "item")
            {
                int depth = reader.Depth;

                // create new History
                History history = new History(reader.GetAttribute("url"));

                history.LastVisited = Convert.ToDateTime(reader.GetAttribute("lastVisited"));
                history.Times = Convert.ToInt32(reader.GetAttribute("times"));

                // add History
                Historys.Add(history);

                // move to next node
                reader.Read();

                // move to next element node
                while (reader.NodeType == XmlNodeType.EndElement)
                    reader.Read();
                if (reader.Depth < depth)
                    return;
            }
        }

        #endregion

        #region "Settings"

        /// <summary>
        /// Cargamos la configuración
        /// </summary>
        static public void LoadSettings()
        {
            // check file existance
            if (File.Exists(SettingsFile))
            {
                FileStream fs = null;
                XmlTextReader xmlIn = null;

                try
                {
                    // open file
                    fs = new FileStream(SettingsFile, FileMode.Open);
                    // create XML reader
                    xmlIn = new XmlTextReader(fs);

                    xmlIn.WhitespaceHandling = WhitespaceHandling.None;
                    xmlIn.MoveToContent();

                    // check for main node
                    if (xmlIn.Name.ToLower() != "settings")
                        throw new ApplicationException("");


                    Settings.HomePage = xmlIn.GetAttribute("HomePage");
                    Settings.UpdatePage = xmlIn.GetAttribute("UpdatePage");
                    Settings.MenuBar = Convert.ToBoolean(xmlIn.GetAttribute("MenuBar"));
                    Settings.AdressBar = Convert.ToBoolean(xmlIn.GetAttribute("AdressBar"));
                    Settings.LinkBar = Convert.ToBoolean(xmlIn.GetAttribute("LinkBar"));
                    Settings.DropDown = Convert.ToInt32(xmlIn.GetAttribute("DropDown"));
                    Settings.FavoritesPanel = Convert.ToBoolean(xmlIn.GetAttribute("FavoritesPanel"));
                    Settings.SplashScreen = Convert.ToBoolean(xmlIn.GetAttribute("SplashScreen"));
                    Settings.AllowFileDownload = Convert.ToBoolean(xmlIn.GetAttribute("AllowFileDownload"));
                    Settings.AllowIFrames = Convert.ToBoolean(xmlIn.GetAttribute("AllowIFrames"));
                    Settings.AllowImageExternalLinks = Convert.ToBoolean(xmlIn.GetAttribute("AllowImageExternalLinks"));
                    Settings.AllowNewWindow = Convert.ToBoolean(xmlIn.GetAttribute("AllowNewWindow"));
                    Settings.CheckIsChildValidPage = Convert.ToBoolean(xmlIn.GetAttribute("CheckIsChildValidPage"));
                    Settings.IsWebBrowserContextMenuEnabled =
                        Convert.ToBoolean(xmlIn.GetAttribute("IsWebBrowserContextMenuEnabled"));
                    Settings.ProtectWithPassword = Convert.ToBoolean(xmlIn.GetAttribute("ProtectWithPassword"));
                    Settings.RemoveContextMenu = Convert.ToBoolean(xmlIn.GetAttribute("RemoveContextMenu"));
                    Settings.RemoveFlashBanner = Convert.ToBoolean(xmlIn.GetAttribute("RemoveFlashBanner"));
                    Settings.ScriptErrorsSuppressed = Convert.ToBoolean(xmlIn.GetAttribute("ScriptErrorsSuppressed"));
                    Settings.SettingsPassword = xmlIn.GetAttribute("SettingsPassword");
                    Settings.WebBrowserShortcutsEnabled =
                        Convert.ToBoolean(xmlIn.GetAttribute("WebBrowserShortcutsEnabled"));
                    Settings.CheckContent = Convert.ToBoolean(xmlIn.GetAttribute("CheckContent"));
                    Settings.PasswordOnExit = Convert.ToBoolean(xmlIn.GetAttribute("PasswordOnExit"));
                    string bad = xmlIn.GetAttribute("BadWords");
                    if (bad != null)
                    {
                        Settings.BadWords.AddRange(bad.Split(','));
                    }

                    LoadBlackList();
                }

                // catch any exceptions
                catch (Exception)
                {
                }
                finally
                {
                    if (xmlIn != null)
                        xmlIn.Close();
                }
            }
        }

        // Save application settings
        static public void SaveSettings()
        {
            // open file
            FileStream fs = new FileStream(SettingsFile, FileMode.Create);
            // create XML writer
            XmlTextWriter xmlOut = new XmlTextWriter(fs, Encoding.UTF8);

            // use indenting for readability
            xmlOut.Formatting = Formatting.Indented;

            // start document
            xmlOut.WriteStartDocument();
            xmlOut.WriteComment("Configuration file");

            // main node
            xmlOut.WriteStartElement("Settings");

            xmlOut.WriteAttributeString("HomePage", Settings.HomePage);
            xmlOut.WriteAttributeString("UpdatePage", Settings.UpdatePage);
            xmlOut.WriteAttributeString("AdressBar", Settings.AdressBar.ToString());
            xmlOut.WriteAttributeString("DropDown", Settings.DropDown.ToString());
            xmlOut.WriteAttributeString("FavoritesPanel", Settings.FavoritesPanel.ToString());
            xmlOut.WriteAttributeString("LinkBar", Settings.LinkBar.ToString());
            xmlOut.WriteAttributeString("MenuBar", Settings.MenuBar.ToString());
            xmlOut.WriteAttributeString("SplashScreen", Settings.SplashScreen.ToString());

            xmlOut.WriteAttributeString("AllowFileDownload", Settings.AllowFileDownload.ToString());
            xmlOut.WriteAttributeString("AllowIFrames", Settings.AllowIFrames.ToString());
            xmlOut.WriteAttributeString("AllowImageExternalLinks", Settings.AllowImageExternalLinks.ToString());
            xmlOut.WriteAttributeString("AllowNewWindow", Settings.AllowNewWindow.ToString());
            xmlOut.WriteAttributeString("CheckIsChildValidPage", Settings.CheckIsChildValidPage.ToString());
            xmlOut.WriteAttributeString("IsWebBrowserContextMenuEnabled", Settings.IsWebBrowserContextMenuEnabled.ToString());
            xmlOut.WriteAttributeString("ProtectWithPassword", Settings.ProtectWithPassword.ToString());
            xmlOut.WriteAttributeString("RemoveContextMenu", Settings.RemoveContextMenu.ToString());
            xmlOut.WriteAttributeString("RemoveFlashBanner", Settings.RemoveFlashBanner.ToString());
            xmlOut.WriteAttributeString("ScriptErrorsSuppressed", Settings.ScriptErrorsSuppressed.ToString());
            xmlOut.WriteAttributeString("SettingsPassword", Settings.SettingsPassword);
            xmlOut.WriteAttributeString("WebBrowserShortcutsEnabled", Settings.WebBrowserShortcutsEnabled.ToString());
            xmlOut.WriteAttributeString("CheckContent", Settings.CheckContent.ToString());
            xmlOut.WriteAttributeString("PasswordOnExit", Settings.PasswordOnExit.ToString());
            xmlOut.WriteAttributeString("BadWords", ToStringComma(Settings.BadWords.ToArray()));

            xmlOut.WriteEndElement();

            // close file
            xmlOut.Close();
        }

        /// <summary>
        /// Cargamos la lista "negra" de urls
        /// </summary>
        static private void LoadBlackList()
        {
            ExtractZipFile(BlackListZipFile);

            // check file existance
            if (File.Exists(BlackListTxtFile))
            {
                StreamReader sr = null;

                try
                {
                    // open file
                    sr = File.OpenText(BlackListTxtFile);

                    string input = null;
                    List<string> list = new List<string>();

                    while ((input = sr.ReadLine()) != null)
                    {
                        if (input != "")
                            list.Add(input);
                    }

                    Settings.BlackList = list;
                }
                // catch any exceptions
                catch (Exception)
                {
                }
                finally
                {
                    if (sr != null)
                        sr.Close();

                    File.Delete(BlackListTxtFile);
                }
            }
        }

        #endregion

        /// <summary>
        /// Extraemos los ficheros de un fichero zip en la carpeta de la aplicación.
        /// </summary>
        /// <param name="zipFile"></param>
        public static void ExtractZipFile(string zipFile)
        {
            // Opens existing zip file
            WiworBrowser.Controls.DBZip zip = WiworBrowser.Controls.DBZip.Open(zipFile, FileAccess.Read);

            // Read all directory contents
            List<WiworBrowser.Controls.DBZip.ZipFileEntry> dir = zip.ReadCentralDir();

            // Extract all files in target directory
            string path;
            bool result;
            foreach (WiworBrowser.Controls.DBZip.ZipFileEntry entry in dir)
            {
                //no procesamos los ficheros xml (son propios de la cada instalación)
                if (!entry.FilenameInZip.EndsWith(".xml"))
                {
                    //borramos el fichero .old si existe
                    if (File.Exists(entry.FilenameInZip + ".old")) File.Delete(entry.FilenameInZip + ".old");

                    //renombramos los ficheros .exe, .dll a .exe.old, .dll.old, .pdb
                    if (entry.FilenameInZip.EndsWith(".exe") || entry.FilenameInZip.EndsWith(".dll") || entry.FilenameInZip.EndsWith(".pdb"))
                    {
                        if (File.Exists(entry.FilenameInZip))
                            File.Move(entry.FilenameInZip, entry.FilenameInZip + ".old");
                    }

                    path = Path.Combine(".", Path.GetFileName(entry.FilenameInZip));
                    result = zip.ExtractFile(entry, path);
                }
            }
            zip.Close();
        }

        /// <summary>
        /// Borramos los ficheros temporales de una actualización
        /// </summary>
        public static void DeleteTempFiles()
        {
            DirectoryInfo di = new DirectoryInfo(".");
            FileInfo[] rgFiles = di.GetFiles("*.old");
            foreach (FileInfo fi in rgFiles)
            {
                try
                {
                    File.Delete(fi.Name);
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Devuelve true si hay ficheros temporales de una actualización
        /// </summary>
        /// <returns></returns>
        public static bool RestartPending()
        {
            DirectoryInfo di = new DirectoryInfo(".");
            FileInfo[] rgFiles = di.GetFiles("*.old");

            if (rgFiles.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string CheckServerVersion()
        {
            string fileVersion = "";

            try
            {
                WiworBrowser.Controls.DBHttp http = new WiworBrowser.Controls.DBHttp();
                fileVersion = http.GetHTTP(Settings.UpdatePage + "/AutoUpdate/version.txt");
            }
            catch
            {
                fileVersion = Common.AssemblyFunctions.AssemblyVersion;
            }

            return fileVersion;
        }

        private static string ToStringComma(string[] p)
        {
            string cad = "";

            foreach (string s in p)
            {
                cad += s + ",";
            }

            return cad.Substring(0, cad.Length - 1);
        }

    }
}
