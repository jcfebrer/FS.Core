#region

using System;
using System.Collections.Generic;
using System.Net;

#endregion

namespace FSDns
{
    public class Response
    {
        /// <summary>
        ///   List of AdditionalRR records
        /// </summary>
        public List<AdditionalRR> Additionals;

        /// <summary>
        ///   List of AnswerRR records
        /// </summary>
        public List<AnswerRR> Answers;

        /// <summary>
        ///   List of AuthorityRR records
        /// </summary>
        public List<AuthorityRR> Authorities;

        /// <summary>
        ///   Error message, empty when no error
        /// </summary>
        public string Error;

        /// <summary>
        ///   The Size of the message
        /// </summary>
        public int MessageSize;

        /// <summary>
        ///   List of Question records
        /// </summary>
        public List<Question> Questions;

        /// <summary>
        ///   Server which delivered this response
        /// </summary>
        public IPEndPoint Server;

        /// <summary>
        ///   TimeStamp when cached
        /// </summary>
        public DateTime TimeStamp;

        public Header header;

        public Response()
        {
            Questions = new List<Question>();
            Answers = new List<AnswerRR>();
            Authorities = new List<AuthorityRR>();
            Additionals = new List<AdditionalRR>();

            Server = new IPEndPoint(0, 0);
            Error = "";
            MessageSize = 0;
            TimeStamp = DateTime.Now;
            header = new Header();
        }

        public Response(IPEndPoint iPEndPoint, byte[] data)
        {
            Error = "";
            Server = iPEndPoint;
            TimeStamp = DateTime.Now;
            MessageSize = data.Length;
            RecordReader rr = new RecordReader(data);

            Questions = new List<Question>();
            Answers = new List<AnswerRR>();
            Authorities = new List<AuthorityRR>();
            Additionals = new List<AdditionalRR>();

            header = new Header(rr);

            for (int intI = 0; intI < header.QDCOUNT; intI++)
            {
                Questions.Add(new Question(rr));
            }

            for (int intI = 0; intI < header.ANCOUNT; intI++)
            {
                Answers.Add(new AnswerRR(rr));
            }

            for (int intI = 0; intI < header.NSCOUNT; intI++)
            {
                Authorities.Add(new AuthorityRR(rr));
            }
            for (int intI = 0; intI < header.ARCOUNT; intI++)
            {
                Additionals.Add(new AdditionalRR(rr));
            }
        }

        /// <summary>
        ///   List of RecordMX in Response.Answers
        /// </summary>
        public RecordMX[] RecordsMX
        {
            get
            {
                List<RecordMX> list = new List<RecordMX>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordMX record = answerRR.RECORD as RecordMX;
                    if (record != null)
                        list.Add(record);
                }
                list.Sort();
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordTXT in Response.Answers
        /// </summary>
        public RecordTXT[] RecordsTXT
        {
            get
            {
                List<RecordTXT> list = new List<RecordTXT>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordTXT record = answerRR.RECORD as RecordTXT;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordA in Response.Answers
        /// </summary>
        public RecordA[] RecordsA
        {
            get
            {
                List<RecordA> list = new List<RecordA>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordA record = answerRR.RECORD as RecordA;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordPTR in Response.Answers
        /// </summary>
        public RecordPTR[] RecordsPTR
        {
            get
            {
                List<RecordPTR> list = new List<RecordPTR>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordPTR record = answerRR.RECORD as RecordPTR;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordCNAME in Response.Answers
        /// </summary>
        public RecordCNAME[] RecordsCNAME
        {
            get
            {
                List<RecordCNAME> list = new List<RecordCNAME>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordCNAME record = answerRR.RECORD as RecordCNAME;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordAAAA in Response.Answers
        /// </summary>
        public RecordAAAA[] RecordsAAAA
        {
            get
            {
                List<RecordAAAA> list = new List<RecordAAAA>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordAAAA record = answerRR.RECORD as RecordAAAA;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordNS in Response.Answers
        /// </summary>
        public RecordNS[] RecordsNS
        {
            get
            {
                List<RecordNS> list = new List<RecordNS>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordNS record = answerRR.RECORD as RecordNS;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        /// <summary>
        ///   List of RecordSOA in Response.Answers
        /// </summary>
        public RecordSOA[] RecordsSOA
        {
            get
            {
                List<RecordSOA> list = new List<RecordSOA>();
                foreach (AnswerRR answerRR in Answers)
                {
                    RecordSOA record = answerRR.RECORD as RecordSOA;
                    if (record != null)
                        list.Add(record);
                }
                return list.ToArray();
            }
        }

        public RR[] RecordsRR
        {
            get
            {
                List<RR> list = new List<RR>();
                foreach (RR rr in Answers)
                {
                    list.Add(rr);
                }
                foreach (RR rr in Answers)
                {
                    list.Add(rr);
                }
                foreach (RR rr in Authorities)
                {
                    list.Add(rr);
                }
                foreach (RR rr in Additionals)
                {
                    list.Add(rr);
                }
                return list.ToArray();
            }
        }
    }
}