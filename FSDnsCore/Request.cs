#region

using System.Collections.Generic;

#endregion

namespace FSDnsCore
{
    public class Request
    {
        private readonly List<Question> questions;
        public Header header;

        public Request()
        {
            header = new Header();
            header.OPCODE = OPCode.Query;
            header.QDCOUNT = 0;

            questions = new List<Question>();
        }

        public byte[] Data
        {
            get
            {
                List<byte> data = new List<byte>();
                header.QDCOUNT = questions.Count;
                data.AddRange(header.Data);
                foreach (Question q in questions)
                    data.AddRange(q.Data);
                return data.ToArray();
            }
        }

        public void AddQuestion(Question question)
        {
            questions.Add(question);
        }
    }
}