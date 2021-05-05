#region

using System;

#endregion

#region Rfc info

/*
 * http://www.ietf.org/rfc/rfc2535.txt
 * 4.1 SIG RDATA Format

   The RDATA portion of a SIG RR is as shown below.  The integrity of
   the RDATA information is protected by the signature field.

                           1 1 1 1 1 1 1 1 1 1 2 2 2 2 2 2 2 2 2 2 3 3
       0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
      |        type covered           |  algorithm    |     labels    |
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
      |                         original TTL                          |
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
      |                      signature expiration                     |
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
      |                      signature inception                      |
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
      |            key  tag           |                               |
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+         signer's name         +
      |                                                               /
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-/
      /                                                               /
      /                            signature                          /
      /                                                               /
      +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+


*/

#endregion

namespace FSDns
{
    public class RecordSIG : Record
    {
        public byte ALGORITHM;
        public int KEYTAG;
        public byte LABELS;
        public Int64 ORIGINALTTL;
        public string SIGNATURE;
        public Int64 SIGNATUREEXPIRATION;
        public Int64 SIGNATUREINCEPTION;
        public string SIGNERSNAME;
        public int TYPECOVERED;

        public RecordSIG(RecordReader rr)
        {
            TYPECOVERED = rr.ReadUInt16();
            ALGORITHM = rr.ReadByte();
            LABELS = rr.ReadByte();
            ORIGINALTTL = rr.ReadUInt32();
            SIGNATUREEXPIRATION = rr.ReadUInt32();
            SIGNATUREINCEPTION = rr.ReadUInt32();
            KEYTAG = rr.ReadUInt16();
            SIGNERSNAME = rr.ReadDomainName();
            SIGNATURE = rr.ReadString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} {6} {7} \"{8}\"",
                                 TYPECOVERED,
                                 ALGORITHM,
                                 LABELS,
                                 ORIGINALTTL,
                                 SIGNATUREEXPIRATION,
                                 SIGNATUREINCEPTION,
                                 KEYTAG,
                                 SIGNERSNAME,
                                 SIGNATURE);
        }
    }
}