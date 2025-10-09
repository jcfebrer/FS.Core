using FSLibrary;
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace FSDisk
{
    public class Backup
    {
        private string m_origen;
        private string m_destino;
        private AlarmClock m_alarmClock;
        private bool m_overwrite;
        private bool m_copyhidden;
        private bool m_compress;
        private string m_name;
        private bool m_Running = false;

        public event EventHandler OnBackupStart;

        public AlarmClock AlarmClock
        {
            get { return m_alarmClock; }
            set { m_alarmClock = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Origen
        {
            get { return m_origen; }
            set { m_origen = value; }
        }

        public string Destino
        {
            get { return m_destino; }
            set { m_destino = value; }
        }

        public bool Overwrite
        {
            get { return m_overwrite; }
            set { m_overwrite = value; }
        }

        public bool Compress
        {
            get { return m_compress; }
            set { m_compress = value; }
        }

        public bool CopyHidden
        {
            get { return m_copyhidden; }
            set { m_copyhidden = value; }
        }

        [XmlIgnore]
        public bool Running
        {
            get { return m_Running; }
            set { m_Running = value; }
        }

        public Backup()
        { }

        public Backup(string nombre, string origen, string destino, AlarmClock alarm, bool overwrite, bool copyhidden, bool compress)
        {
            m_name = nombre;
            m_origen = origen;
            m_destino = destino;
            m_alarmClock = alarm;
            m_overwrite = overwrite;
            m_copyhidden = copyhidden;
            m_compress = compress;

            alarm.Alarm += Alarm_Alarm;
        }

        public void Update(string nombre, string origen, string destino, AlarmClock alarm, bool overwrite, bool copyhidden, bool compress)
        {
            m_name = nombre;
            m_origen = origen;
            m_destino = destino;
            m_alarmClock = alarm;
            m_overwrite = overwrite;
            m_copyhidden = copyhidden;
            m_compress = compress;

            alarm.Alarm += Alarm_Alarm;
        }

        private void Alarm_Alarm(object sender, EventArgs e)
        {
            if (OnBackupStart != null)
                OnBackupStart(this, EventArgs.Empty);

            m_Running = false;
        }
    }


    public class BackupCollection : CollectionBase
    {
        public BackupCollection()
        {
        }

        public Backup this[int index]
        {
            get { return (Backup)List[index]; }
            set { List[index] = value; }
        }

        public void Add(Backup entry)
        {
            List.Add(entry);
        }

        public void Add(BackupCollection entries)
        {
            List.Clear();
            foreach (Backup backup in entries)
                List.Add(backup);
        }

        public void Remove(Backup entry)
        {
            List.Remove(entry);
        }

        public void Remove(string backupName)
        {
            Backup entry = Find(backupName);
            if (entry != null)
                List.Remove(entry);
        }

        public Backup Find(Backup entry)
        {
            foreach (Backup backup in List)
                if (entry == backup)
                    return backup;
            return null;
        }

        public Backup Find(string name)
        {
            foreach (Backup f in List)
                if (f.Name.ToLower() == name.ToLower())
                    return f;
            return null;
        }

        public bool Exist(string name)
        {
            foreach (Backup f in List)
                if (f.Name == name)
                    return true;
            return false;
        }

        public void Load(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(BackupCollection));
            using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open))
            {
                this.Clear();
                this.Add((BackupCollection)ser.Deserialize(fs));
            }
        }

        public void Save(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(BackupCollection));

            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                ser.Serialize(writer, (BackupCollection)this);
            }
        }
    }
}
