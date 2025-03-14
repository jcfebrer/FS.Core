using FSLibrary;
using FSParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSLibrary.Tests
{
    [TestClass]
    public class IniParserTests
    {
        [TestMethod()]
        public void TestScummvmIni()
        {
            IniParser ini = new IniParser("L:\\EmulationStation-DE\\Emulators\\RetroArch\\system\\scummvm.ini");


            string[] SubDirs = Directory.GetDirectories("L:\\EmulationStation-DE\\ROMs\\scummvm");
            foreach (string subDir in SubDirs)
            {
                string section = ini.FindSectionByKeyValue("path", subDir);

                if (section != null)
                {
                    //using (StreamWriter sw = File.CreateText(subDir + "\\" + System.IO.Path.GetFileName(subDir)))
                    //{
                    //    sw.Write(section.ToLower());
                    //    sw.Close();
                    //}
                }
            }

            ini.AddSetting("prueba", "nueva sección", "valor 123");
            
            //ini.SaveSettings("prueba.ini");
        }
    }
}
