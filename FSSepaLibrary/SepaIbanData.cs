﻿using System;
using System.Text.RegularExpressions;
using FSSepaLibrary.Utils;

namespace FSSepaLibrary
{
    /// <summary>
    /// Get Creditor or Debtor data (Name and BIC + IBAN)
    /// </summary>
    public class SepaIbanData : ICloneable
    {
        private string bic;
        private string iban;
        private string name;
        private SepaPostalAddress address;
        private SepaPostalAddress agentAddress;
        private bool withoutBic;

        // Regex to find space
        private static readonly Regex SpaceRegex = new Regex("\\s+", RegexOptions.Compiled);

        /// <summary>
        /// The Name of the owner
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = StringUtils.GetLimitedString(value, 70);
            }
        }

        public SepaPostalAddress Address
        {
            get { return address; }
            set
            {
                if (!value.IsValid)
                    throw new SepaRuleException("Iban Address data is invalid.");
                address = value;
            }
        }

        public SepaPostalAddress AgentAddress
        {
            get { return agentAddress; }
            set
            {
                if (!value.IsValid)
                    throw new SepaRuleException("Iban Address data is invalid.");
                agentAddress = value;
            }
        }

        /// <summary>
        /// Is the BIC code unknown?
        /// </summary>
        public bool UnknownBic
        {
            get { return withoutBic; }
            set { withoutBic = value; }
        }

        /// <summary>
        /// The BIC Code
        /// </summary>
        /// <exception cref="SepaRuleException">If BIC hasn't 8 or 11 characters.</exception>
        public string Bic
        {
            get { return bic; }
            set
            {
				if (value == null || (value.Length != 8 && value.Length != 11)) {
					throw new SepaRuleException(string.Format("Null or Invalid length of BIC/swift code \"{0}\", must be 8 or 11 chars.", value));
				}
                bic = value;
            }
        }

        /// <summary>
        /// The IBAN Number
        /// </summary>
        /// <exception cref="SepaRuleException">If IBAN length is not between 14 and 34 characters.</exception>
        public string Iban
        {
            get { return iban; }
            set
            {
                if (value == null || value.Length < 11 || value.Length > 34)
                    throw new SepaRuleException(string.Format("Null or Invalid length of IBAN code \"{0}\", must contain between 11 and 34 characters.", value));
                iban = SpaceRegex.Replace(value, string.Empty);
            }
        }

        /// <summary>
        /// Is data is well set to be used
        /// </summary>
        /// <returns></returns>
        public bool IsValid
        {
            get { return (!string.IsNullOrEmpty(bic) || withoutBic) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(iban); }
        }

        /// <summary>
        /// Return a copy of this object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return Name + Environment.NewLine + 
                Iban + Environment.NewLine + 
                Bic + Environment.NewLine + 
                Address;
        }
    }

    public enum PostalAddressType
    {
        ADDR,
        PBOX,
        HOME,
        BIZZ,
        MLTO,
        DLVY
    }
}