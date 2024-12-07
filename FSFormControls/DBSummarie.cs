using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSFormControls
{
    public class DBSummarie : CollectionBase
    {
        public enum SummarieType
        {
            Average,
            Count,
            Sum
        }

        private string m_Name;
        private SummarieType m_Type;

        public DBSummarie(string name, SummarieType type)
        {
            m_Name = name;
            m_Type = type;
        }

        public string Name { get; set; }
        public SummarieType Type { get; set; }
    }
}
