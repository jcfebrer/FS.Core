#region

using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

#endregion

namespace FSNetwork
{
    public class Ping
    {
        private const int DATA_SIZE = 32;
        private const int DEFAULT_TIMEOUT = 1000;
        private const int ICMP_ECHO = 8;
        private const int SOCKET_ERROR = -1;
        private const int PING_ERROR = -1;
        private const int RECV_SIZE = 128 - 1;
        private string m_HostName;
        private EndPoint m_Local;


        private bool m_Open;
        private SPing m_Packet;
        private byte[] m_RecvBuffer;
        private EndPoint m_Server;
        private Socket m_Socket;


        public Ping(string hostName)
        {
            HostName = hostName;
            m_RecvBuffer = new byte[RECV_SIZE];
        }

        public Ping()
        {
            HostName = Dns.GetHostName();
            m_RecvBuffer = new byte[RECV_SIZE];
        }


        public string HostName
        {
            get { return m_HostName; }
            set
            {
                m_HostName = value;
                if ((m_Open))
                {
                    Close();
                    Open();
                }
            }
        }

        public bool IsOpen
        {
            get { return m_Open; }
        }

        ~Ping()
        {
            Close();
            m_RecvBuffer = null;
        }

        public bool Open()
        {
            byte[] Payload = null;
            if ((!(m_Open)))
            {
                try
                {
                    Payload = new byte[DATA_SIZE];
                    m_Packet.Initialize(Convert.ToByte(ICMP_ECHO), 0, Payload);
                    m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
                    m_Server = new IPEndPoint(Dns.GetHostEntry(m_HostName).AddressList[0], 0);
                    m_Local = new IPEndPoint(Dns.GetHostEntry(Dns.GetHostName()).AddressList[0], 0);
                    m_Open = true;
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }


        public bool Close()
        {
            if ((m_Open))
            {
                m_Socket.Close();
                m_Socket = null;
                m_Server = null;
                m_Local = null;
                m_Open = false;
            }
            return true;
        }


        public int DoPing()
        {
            return DoPing(DEFAULT_TIMEOUT);
        }


        public int DoPing(int timeOutMilliSeconds)
        {
            int TimeOut = timeOutMilliSeconds + Environment.TickCount;
            try
            {
                if ((SOCKET_ERROR == m_Socket.SendTo(m_Packet.Serialize(), m_Packet.Size(), ((0)), m_Server)))
                {
                    return PING_ERROR;
                }
            }
            catch
            {
            }
            do
            {
                if ((m_Socket.Poll(1000, SelectMode.SelectRead)))
                {
                    m_Socket.ReceiveFrom(m_RecvBuffer, RECV_SIZE + 1, ((0)), ref m_Local);
                    return (timeOutMilliSeconds - (TimeOut - Environment.TickCount));
                }
                else if ((Environment.TickCount >= TimeOut))
                {
                    return PING_ERROR;
                }
            } while ((true));
        }

        #region Nested type: SPing

        public struct SPing
        {
            public int Complement_CheckSum;
            public byte[] Data;
            public int Identifier;
            public int SequenceNumber;
            public byte SubCode_type;
            public byte Type_Message;

            public void Initialize(byte type, byte subCode, byte[] payload)
            {
                byte[] Buffer_IcmpPacket = null;
                int[] CksumBuffer = null;
                Int32 IcmpHeaderBufferIndex = 0;
                int Index = 0;
                Type_Message = type;
                SubCode_type = subCode;
                Complement_CheckSum = UInt16.Parse("0");
                Identifier = UInt16.Parse("45");
                SequenceNumber = UInt16.Parse("0");
                Data = payload;
                Buffer_IcmpPacket = Serialize();
                CksumBuffer = new int[(Buffer_IcmpPacket.Length/2) - 1];
                for (Index = 0; Index <= (CksumBuffer.Length - 1); Index++)
                {
                    CksumBuffer[Index] = BitConverter.ToUInt16(Buffer_IcmpPacket, IcmpHeaderBufferIndex);
                    IcmpHeaderBufferIndex += 2;
                }
                Complement_CheckSum = MCheckSum.Calculate(CksumBuffer, CksumBuffer.Length);
            }


            public int Size()
            {
                return (8 + Data.Length);
            }


            public byte[] Serialize()
            {
                byte[] Buffer = null;
                byte[] B_Seq = BitConverter.GetBytes(SequenceNumber);
                byte[] B_Cksum = BitConverter.GetBytes(Complement_CheckSum);
                byte[] B_Id = BitConverter.GetBytes(Identifier);
                Int32 Index = 0;
                Buffer = new byte[Size() - 1];
                Buffer[0] = Type_Message;
                Buffer[1] = SubCode_type;
                Index += 2;
                Array.Copy(B_Cksum, 0, Buffer, Index, 2);
                Index += 2;
                Array.Copy(B_Id, 0, Buffer, Index, 2);
                Index += 2;
                Array.Copy(B_Seq, 0, Buffer, Index, 2);
                Index += 2;
                if ((Data.Length > 0))
                {
                    Array.Copy(Data, 0, Buffer, Index, Data.Length);
                }
                return Buffer;
            }
        }

        #endregion
    }


    // following class was VB module
    public sealed class MCheckSum
    {
        public static int Calculate(int[] buffer, Int32 size)
        {
            Int32 Counter = 0;
            UNION_INT32 Cksum32 = new UNION_INT32();
            while ((size > 0))
            {
                Cksum32.w32 += buffer[Counter];
                Counter += 1;
                size -= 1;
            }
            Cksum32.w32 = Cksum32.msw.w16 + Cksum32.lsw.w16 + Cksum32.msw.w16;
            return (Cksum32.lsw.w16 ^ 0XFFFF);
        }

        #region Nested type: UNION_INT16

        [StructLayout(LayoutKind.Explicit)]
        public struct UNION_INT16
        {
            [FieldOffset(0)] public byte lsb;
            [FieldOffset(1)] public byte msb;
            [FieldOffset(0)] public short w16;
        }

        #endregion

        #region Nested type: UNION_INT32

        [StructLayout(LayoutKind.Explicit)]
        public struct UNION_INT32
        {
            [FieldOffset(0)] public UNION_INT16 lsw;
            [FieldOffset(2)] public UNION_INT16 msw;
            [FieldOffset(0)] public int w32;
        }

        #endregion
    }
}