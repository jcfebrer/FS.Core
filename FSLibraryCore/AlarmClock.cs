using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using System.Xml.Serialization;

namespace FSLibrary
{
    /// <summary>
    /// Clase que nos permite definir eventos de alarma.
    /// </summary>
    public class AlarmClock
    {
        private EventHandler alarmEvent;
        private Timer timer;
        private System.DateTime m_alarmTime;
        private bool m_enabled;
        private AlarmType m_alarmType;
        private ExecuteDays m_executeDays;
        private string m_name;

        /// <summary>
        /// Gets or sets the alarm time.
        /// </summary>
        /// <value>
        /// The alarm time.
        /// </value>
        public System.DateTime AlarmTime
        {
            get { return m_alarmTime; }
            set { m_alarmTime = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="AlarmClock"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        public bool Enabled
        {
            get { return m_enabled; }
            set { m_enabled = value; }
        }

        /// <summary>
        /// Gets or sets the type of the alarm.
        /// </summary>
        /// <value>
        /// The type of the alarm.
        /// </value>
        public AlarmType Alarm_Type
        {
            get { return m_alarmType; }
            set { m_alarmType = value; }
        }

        /// <summary>
        /// Gets or sets the execute days.
        /// </summary>
        /// <value>
        /// The execute days.
        /// </value>
        public ExecuteDays Execute_Days
        {
            get { return m_executeDays; }
            set { m_executeDays = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Tipo de alarma
        /// </summary>
        public enum AlarmType
        {
            /// <summary>
            /// Diaria
            /// </summary>
            Diary,
            /// <summary>
            /// Un día concreto
            /// </summary>
            Day, 
            /// <summary>
            /// Semanal
            /// </summary>
            Weekly, 
            /// <summary>
            /// Mensual
            /// </summary>
            Mounthly,
            /// <summary>
            /// Anual
            /// </summary>
            Yearthly
        }

        /// <summary>
        /// 
        /// </summary>
        public class ExecuteDays
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ExecuteDays"/> class.
            /// </summary>
            public ExecuteDays()
            {
            }

            /// <summary>
            /// The monday
            /// </summary>
            public bool Monday = false;
            /// <summary>
            /// Launch on tuesday
            /// </summary>
            public bool Tuesday = false;
            /// <summary>
            /// Launch on wednesday
            /// </summary>
            public bool Wednesday = false;
            /// <summary>
            /// Launch on thursday
            /// </summary>
            public bool Thursday = false;
            /// <summary>
            /// Launch on friday
            /// </summary>
            public bool Friday = false;
            /// <summary>
            /// Launch on saturday
            /// </summary>
            public bool Saturday = false;
            /// <summary>
            /// Launch on sunday
            /// </summary>
            public bool Sunday = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmClock"/> class.
        /// </summary>
        public AlarmClock()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmClock"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="alarmTime">The alarm time.</param>
        /// <param name="alarmType">Type of the alarm.</param>
        /// <param name="executeDays">The execute days.</param>
        public AlarmClock(string name, System.DateTime alarmTime, AlarmType alarmType, ExecuteDays executeDays)
        {
            this.m_name = name;
            this.m_alarmTime = alarmTime;
            this.m_alarmType = alarmType;
            this.m_executeDays = executeDays;

            timer = new Timer();
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 1000;
            timer.Start();

            m_enabled = true;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();

            switch (m_alarmType)
            {
                case AlarmType.Day:

                    if (m_enabled && (System.DateTime.Now.Day == m_alarmTime.Day && System.DateTime.Now.Month == m_alarmTime.Month && System.DateTime.Now.Year == m_alarmTime.Year && System.DateTime.Now.Hour == m_alarmTime.Hour && System.DateTime.Now.Minute == m_alarmTime.Minute))
                    {
                        m_enabled = false;
                        OnAlarm();
                    }
                    break;

                case AlarmType.Diary:

                    DayOfWeek day = System.DateTime.Now.DayOfWeek;
                    switch (day)
                    {
                        case DayOfWeek.Monday:
                            {
                                if (!m_executeDays.Monday) return;
                                break;
                            }
                        case DayOfWeek.Tuesday:
                            {
                                if (!m_executeDays.Tuesday) return;
                                break;
                            }
                        case DayOfWeek.Wednesday:
                            {
                                if (!m_executeDays.Wednesday) return;
                                break;
                            }
                        case DayOfWeek.Thursday:
                            {
                                if (!m_executeDays.Thursday) return;
                                break;
                            }
                        case DayOfWeek.Friday:
                            {
                                if (!m_executeDays.Friday) return;
                                break;
                            }
                        case DayOfWeek.Saturday:
                            {
                                if (!m_executeDays.Saturday) return;
                                break;
                            }
                        case DayOfWeek.Sunday:
                            {
                                if (!m_executeDays.Sunday) return;
                                break;
                            }
                    }

                    if (m_enabled && (System.DateTime.Now.Day == m_alarmTime.Day && System.DateTime.Now.Month == m_alarmTime.Month && System.DateTime.Now.Year == m_alarmTime.Year && System.DateTime.Now.Hour == m_alarmTime.Hour && System.DateTime.Now.Minute == m_alarmTime.Minute))
                    {
                        bool exitFor = false;

                        //nos aseguramos que el día seleccionado permite la execución de la alarma
                        for (int f = 0; f < 7; f++)
                        {
                            m_alarmTime = m_alarmTime.AddDays(1);
                            switch (m_alarmTime.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    {
                                        if (m_executeDays.Monday) exitFor = true;
                                        break;
                                    }
                                case DayOfWeek.Tuesday:
                                    {
                                        if (m_executeDays.Tuesday) exitFor = true;
                                        break;
                                    }
                                case DayOfWeek.Wednesday:
                                    {
                                        if (m_executeDays.Wednesday) exitFor = true;
                                        break;
                                    }
                                case DayOfWeek.Thursday:
                                    {
                                        if (m_executeDays.Thursday) exitFor = true;
                                        break;
                                    }
                                case DayOfWeek.Friday:
                                    {
                                        if (m_executeDays.Friday) exitFor = true;
                                        break;
                                    }
                                case DayOfWeek.Saturday:
                                    {
                                        if (m_executeDays.Saturday) exitFor = true;
                                        break;
                                    }
                                case DayOfWeek.Sunday:
                                    {
                                        if (m_executeDays.Sunday) exitFor = true;
                                        break;
                                    }
                            }

                            if (exitFor) break;
                        }
                        OnAlarm();
                    }
                    break;

                case AlarmType.Weekly:

                    if (m_enabled && (System.DateTime.Now.Day == m_alarmTime.Day && System.DateTime.Now.Month == m_alarmTime.Month && System.DateTime.Now.Year == m_alarmTime.Year && System.DateTime.Now.Hour == m_alarmTime.Hour && System.DateTime.Now.Minute == m_alarmTime.Minute))
                    {
                        m_alarmTime = m_alarmTime.AddDays(7);
                        OnAlarm();
                    }
                    break;

                case AlarmType.Mounthly:

                    if (m_enabled && (System.DateTime.Now.Day == m_alarmTime.Day && System.DateTime.Now.Month == m_alarmTime.Month && System.DateTime.Now.Year == m_alarmTime.Year && System.DateTime.Now.Hour == m_alarmTime.Hour && System.DateTime.Now.Minute == m_alarmTime.Minute))
                    {
                        m_alarmTime = m_alarmTime.AddMonths(1);
                        OnAlarm();
                    }
                    break;
            }

            timer.Start();
        }

        /// <summary>
        /// Called when [alarm].
        /// </summary>
        protected virtual void OnAlarm()
        {
            if (alarmEvent != null)
                alarmEvent(this, EventArgs.Empty);
        }


        /// <summary>
        /// Occurs when [alarm].
        /// </summary>
        public event EventHandler Alarm
        {
            add { alarmEvent += value; }
            remove { alarmEvent -= value; }
        }
    }

    /// <summary>
    /// Colección de alarmas
    /// </summary>
    /// <seealso cref="System.Collections.CollectionBase" />
    public class AlarmClockCollection : CollectionBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmClockCollection"/> class.
        /// </summary>
        public AlarmClockCollection()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="AlarmClock"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="AlarmClock"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public AlarmClock this[int index]
        {
            get { return (AlarmClock)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Adds the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Add(AlarmClock entry)
        {
            List.Add(entry);
        }

        /// <summary>
        /// Adds the specified entries.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void Add(AlarmClockCollection entries)
        {
            List.Clear();
            foreach (AlarmClock alarm in entries)
                List.Add(alarm);
        }

        /// <summary>
        /// Removes the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public void Remove(AlarmClock entry)
        {
            List.Remove(entry);
        }

        /// <summary>
        /// Removes the specified alarm name.
        /// </summary>
        /// <param name="alarmName">Name of the alarm.</param>
        public void Remove(string alarmName)
        {
            AlarmClock entry = Find(alarmName);
            if (entry != null)
                List.Remove(entry);
        }

        /// <summary>
        /// Finds the specified entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public AlarmClock Find(AlarmClock entry)
        {
            foreach (AlarmClock alarm in List)
                if (entry == alarm)
                    return alarm;
            return null;
        }

        /// <summary>
        /// Finds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public AlarmClock Find(string name)
        {
            foreach (AlarmClock f in List)
                if (f.Name.ToLower() == name.ToLower())
                    return f;
            return null;
        }

        /// <summary>
        /// Exists the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            foreach (AlarmClock f in List)
                if (f.Name == name)
                    return true;
            return false;
        }

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Load(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(AlarmClockCollection));
            using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open))
            {
                this.Clear();
                this.Add((AlarmClockCollection)ser.Deserialize(fs));
            }
        }

        /// <summary>
        /// Saves the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Save(string fileName)
        {
            XmlSerializer ser = new XmlSerializer(typeof(AlarmClockCollection));

            using (XmlWriter writer = XmlWriter.Create(fileName))
            {
                ser.Serialize(writer, (AlarmClockCollection)this);
            }
        }
    }
}
