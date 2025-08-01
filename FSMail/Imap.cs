﻿using FSTrace;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using FSLibrary;
using System.Collections.Generic;

namespace FSMail
{
	/// <summary>
	/// Imap class implementes IMAP client API
	/// RFC: https://www.rfc-es.org/rfc/rfc2060-es.txt
	/// En GMAIL, tiene que estar habilitado IMAP en la configuración de la cuenta.
	/// </summary>
	public class Imap : ImapBase
	{
		/// <summary>
		/// If user has logged in to his mailbox.
		/// </summary>
		public bool IsLoggedIn { get; set; } = false;
		/// <summary>
		/// Mailbox (Folder) name. Default INBOX.
		/// </summary>
		public string MailboxName { get; set; } = "INBOX";
		/// <summary>
		/// If folder is selected.
		/// </summary>
		public bool IsFolderSelected { get; set; } = false;
		/// <summary>
		/// if folder is examined.
		/// </summary>
		public bool IsFolderExamined { get; set; } = false;
		/// <summary>
		/// Total number of messages in mailbox.
		/// </summary>
		public int TotalMessages { get; set; } = 0;
		/// <summary>
		/// Number of recent messages in mailbox.
		/// </summary>
		public int RecentMessages { get; set; } = 0;
		/// <summary>
		/// First unseen message UID
		/// </summary>
		public int FirstUnSeenMsgUID { get; set; } = -1;


		/// <summary>
		///  Login to specified Imap host and default port (143)
		/// </summary>
		/// <param name="sHost">Imap Server name</param>
		/// <param name="sUserId">User's login id</param>
		/// <param name="sPassword">User's password</param>
		public void Login(string sHost, string sUserId, string sPassword)
		{
			try
			{
				Login(sHost, IMAP_DEFAULT_PORT, sUserId, sPassword);
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Login to specified Imap host and port
		/// </summary>
		/// <param name="sHost">Imap server name</param>
		/// <param name="nPort">Imap server port</param>
		/// <param name="sUserId">User's login id</param>
		/// <param name="sPassword">User's password</param>
		/// <param name="sslEnabled"> </param>
		/// <exception cref="IMAP_ERR_LOGIN"
		/// <exception cref="IMAP_ERR_INVALIDPARAM"
		public void Login(string sHost, int nPort, string sUserId, string sPassword, bool sslEnabled = false)
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			ImapException e_login = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_LOGIN, m_sUserId);
			ImapException e_invalidparam = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM);

			if (sHost.Length == 0)
			{
				Log.TraceError("Invalid m_sHost name");
				throw e_invalidparam;
			}

			if (sUserId.Length == 0)
			{
				Log.TraceError("Invalid m_sUserId");
				throw e_invalidparam;
			}

			if (sPassword.Length == 0)
			{
				Log.TraceError("Invalid Password");
				throw e_invalidparam;
			}
			if (m_bIsConnected)
			{
				if (IsLoggedIn)
				{
					if (m_sHost == sHost && m_nPort == nPort)
					{
						if (m_sUserId == sUserId &&
							m_sPassword == "\"" + sPassword + "\"")
						{
							Log.TraceInfo("Connected and Logged in already");
							return;
						}
						else
							LogOut();
					}
					else Disconnect();
				}
			}

			m_bIsConnected = false;
			IsLoggedIn = false;

			try
			{
				eImapResponse = Connect(sHost, nPort, sslEnabled);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					m_bIsConnected = true;
				}
				else return;
			}
			catch (Exception)
			{
				throw;
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_LOGIN_COMMAND;
			sCommand += " " + sUserId + " " + "\"" + sPassword + "\"";
			sCommand += IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					IsLoggedIn = true;
					m_sUserId = sUserId;
					m_sPassword = "\"" + sPassword + "\"";
				}
				else throw e_login;

			}
			catch (Exception)
			{
				throw;
			}
		}


		/// <summary>
		/// Logout the user: It logout the user and disconnect the connetion from IMAP server.
		/// </summary>
		public void LogOut()
		{
			if (IsLoggedIn)
			{
				ImapResponseEnum eImapResponse;
				ArrayList asResultArray = new ArrayList();
				string sCommand = IMAP_LOGOUT_COMMAND;
				sCommand += IMAP_COMMAND_EOL;
				try
				{
					eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				}
				catch (Exception)
				{
					Disconnect();
					IsLoggedIn = false;
					throw;
				}
				Disconnect();
				IsLoggedIn = false;
			}
		}

		/// <summary>
		/// Examines the default folder.
		/// No permite la modificación.
		/// </summary>
		public bool ExamineFolder()
		{
			return ExamineFolder("INBOX");
		}

		/// <summary>
		/// Examine the sFolder/mailbox after login
		/// No permite la modificación.
		/// </summary>
		/// <param name="sFolder">Mailbox folder</param>
		public bool ExamineFolder(string sFolder)
		{
			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			ImapException e_examine = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_EXAMINE, sFolder);
			ImapException e_invalidparam = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM);
			if (sFolder.Length == 0)
			{
				throw e_invalidparam;
			}
			if (IsFolderExamined)
			{
				if (MailboxName == sFolder)
				{
					Log.TraceInfo("Folder is already selected");
					return true;
				}
				else IsFolderExamined = false;
			}
			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_EXAMINE_COMMAND;
			sCommand += " " + sFolder + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					MailboxName = sFolder;
					IsFolderExamined = true;
				}
				else throw e_examine;
			}
			catch (Exception)
			{
				throw;
			}
			//-------------------------
			// PARSE RESPONSE

			bool bResult = false;
			foreach (string sLine in asResultArray)
			{
				// If this is an unsolicited response starting with '*'
				if (sLine.IndexOf(IMAP_UNTAGGED_RESPONSE_PREFIX) != -1)
				{
					// parse the line by space
					string[] asTokens;
					asTokens = sLine.Split(' ');
					if (asTokens[2] == "EXISTS")
					{
						// The line will look like "* 2 EXISTS"
						TotalMessages = Convert.ToInt32(asTokens[1]);
					}
					else if (asTokens[2] == "RECENT")
					{
						// The line will look like "* 2 RECENT"
						RecentMessages = Convert.ToInt32(asTokens[1]);
					}
					else if (asTokens[2] == "[UNSEEN")
					{
						// The line will look like "* OK [UNSEEN 2]"
						string sUIDPart = asTokens[3].Substring(0, asTokens[3].Length - 1);
						FirstUnSeenMsgUID = Convert.ToInt32(sUIDPart);
					}
				}
				// If this line looks like "<command-tag> OK ..."
				else if (sLine.IndexOf(IMAP_SERVER_RESPONSE_OK) != -1)
				{
					bResult = true;
					break;
				}
			}

			if (!bResult)
				throw e_examine;

			string sLogStr = "TotalMessages[" + TotalMessages.ToString() + "] ,";
			sLogStr += "RecentMessages[" + RecentMessages.ToString() + "] ,";
			if (FirstUnSeenMsgUID > 0)
				sLogStr += "FirstUnSeenMsgUID[" + FirstUnSeenMsgUID.ToString() + "] ,";
			Log.TraceInfo(sLogStr);

			return true;
		}


		/// <summary>
		/// Select the default sFolder/mailbox after login
		/// Permite la modificación a diferencia de Examine
		/// </summary>
		public bool SelectFolder()
		{
			return SelectFolder("INBOX");
		}

		/// <summary>
		/// Select the sFolder/mailbox after login
		/// Permite la modificación a diferencia de Examine
		/// </summary>
		/// <param name="sFolder">mailbox folder</param>
		/// <exception cref="IMAP_ERR_SELECT"
		/// <exception cref="IMAP_ERR_INSUFFICIENT_DATA"
		/// <exception cref="IMAP_ERR_INVALIDPARAM"
		public bool SelectFolder(string sFolder)
		{
			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED, e.Message);
				}

			}
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			ImapException e_select = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SELECT, sFolder);
			ImapException e_invalidparam = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM);
			if (sFolder.Length == 0)
			{
				throw e_invalidparam;
			}
			if (IsFolderSelected)
			{
				if (MailboxName == sFolder)
				{
					Log.TraceInfo("Folder is already selected");
					return true;
				}
				else IsFolderSelected = false;
			}
			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_SELECT_COMMAND;
			sCommand += " " + sFolder + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					MailboxName = sFolder;
					IsFolderSelected = true;
				}
				else throw e_select;
			}
			catch (Exception)
			{
				throw;
			}

			//-------------------------
			// PARSE RESPONSE

			bool bResult = false;
			foreach (string sLine in asResultArray)
			{
				// If this is an unsolicited response starting with '*'
				if (sLine.IndexOf(IMAP_UNTAGGED_RESPONSE_PREFIX) != -1)
				{
					// parse the line by space
					string[] asTokens;
					asTokens = sLine.Split(' ');
					if (asTokens[2] == "EXISTS")
					{
						// The line will look like "* 2 EXISTS"
						TotalMessages = Convert.ToInt32(asTokens[1]);
					}
					else if (asTokens[2] == "RECENT")
					{
						// The line will look like "* 2 RECENT"
						RecentMessages = Convert.ToInt32(asTokens[1]);
					}
					else if (asTokens[2] == "[UNSEEN")
					{
						// The line will look like "* OK [UNSEEN 2]"
						string sUIDPart = asTokens[3].Substring(0, asTokens[3].Length - 1);
						FirstUnSeenMsgUID = Convert.ToInt32(sUIDPart);
					}
				}
				// If this line looks like "<command-tag> OK ..."
				else if (sLine.IndexOf(IMAP_SERVER_RESPONSE_OK) != -1)
				{
					bResult = true;
					break;
				}
			}

			if (!bResult)
				throw e_select;

			string sLogStr = "TotalMessages[" + TotalMessages.ToString() + "] ,";
			sLogStr += "RecentMessages[" + RecentMessages.ToString() + "] ,";
			if (FirstUnSeenMsgUID > 0)
				sLogStr += "FirstUnSeenMsgUID[" + FirstUnSeenMsgUID.ToString() + "] ,";
			Log.TraceInfo(sLogStr);

			return true;
		}


		/// <summary>
		/// Restore the connection using available old data
		/// Select the sFolder if previously selected
		/// </summary>
		/// <param name="bSelectFolder">If true then it selects the folder</param>
		void Restore(bool bSelectFolder)
		{
			ImapException e_insufficiantdata = new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA);
			if (m_sHost.Length == 0 ||
				m_sUserId.Length == 0 ||
				m_sPassword.Length == 0)
			{
				throw e_insufficiantdata;
			}
			try
			{
				IsLoggedIn = false;
				Login(m_sHost, m_nPort, m_sUserId, m_sPassword);
				if (bSelectFolder && MailboxName.Length > 0)
				{
					if (IsFolderSelected)
					{
						IsFolderSelected = false;
						SelectFolder(MailboxName);
					}
					else if (IsFolderExamined)
					{
						IsFolderExamined = false;
						ExamineFolder(MailboxName);
					}
					else SelectFolder(MailboxName);
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Check if enough quota is available
		/// </summary>
		/// <param name="sFolderName">Mailbox folder</param>
		/// <returns>true if enough mail quota</returns>
		public bool HasEnoughQuota(string sFolderName)
		{
			try
			{
				bool bUnlimitedQuota = false; ;
				int nUsedKBytes = 0;
				double nTotalKBytes = 0;

				GetQuota(sFolderName, ref bUnlimitedQuota,
					ref nUsedKBytes, ref nTotalKBytes);

				if (bUnlimitedQuota || (nUsedKBytes < nTotalKBytes))
					return true;
				else
					return false;
			}
			catch (ImapException)
			{
				throw;
			}
		}

		/// <summary>
		/// Get the quota for specific folder
		/// </summary>
		/// <param name="sFolderName">Mailbox folder</param>
		/// <param name="bUnlimitedQuota">Is unlimited quota</param>
		/// <param name="nUsedKBytes">Used quota in Kbytes</param>
		/// <param name="nTotalKBytes">Total quota in KBytes</param>
		public void GetQuota(string sFolderName, ref bool bUnlimitedQuota,
			ref int nUsedKBytes, ref double nTotalKBytes)
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			bool bResult = false;
			bUnlimitedQuota = false;
			nUsedKBytes = 0;
			nTotalKBytes = 0;
			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (sFolderName.Length == 0)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM);
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_GETQUOTA_COMMAND;
			sCommand += " " + sFolderName + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					MailboxName = sFolderName;
					IsFolderExamined = true;
					string quotaPrefix = IMAP_UNTAGGED_RESPONSE_PREFIX + " ";
					quotaPrefix += IMAP_QUOTA_RESPONSE + " ";
					foreach (string sLine in asResultArray)
					{
						if (sLine.StartsWith(quotaPrefix) == true)
						{
							// Find the open and close paranthesis, and extract
							// the part inside out.
							int nStart = sLine.IndexOf('(');
							int nEnd = sLine.IndexOf(')', nStart);
							if (nStart != -1 &&
								nEnd != -1 &&
								nEnd > nStart)
							{
								string sQuota = sLine.Substring(nStart + 1, nEnd - nStart - 1);
								if (sQuota.Length > 0)
								{
									// Parse the space-delimited quota information which
									// will look like "STORAGE <used> <total>"
									string[] asArrList; // = new ArrayList();
									asArrList = sQuota.Split(' ');

									// get the used and total kbytes from these tokens
									if (asArrList.Length == 3 &&
										asArrList[0] == "STORAGE")
									{
										nUsedKBytes = Convert.ToInt32(asArrList[1], 10); ;
										nTotalKBytes = Convert.ToDouble(asArrList[2]);
									}
									else
									{
										string error = "Invalid Quota information :" + sQuota;
										Log.TraceError(error);
										break;
									}
								}
								else
								{
									bUnlimitedQuota = true;
								}
							}
							else
							{
								string error = "Invalid Quota IMAP Response : " + sLine;
								Log.TraceError(error);
								break;
							}
						}
						// If the line looks like "<command-tag> OK ..."
						else if (sLine.IndexOf(IMAP_SERVER_RESPONSE_OK) != -1)
						{
							bResult = true;
							break;
						}
					}

					if (!bResult)
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_QUOTA);
					if (bUnlimitedQuota)
						Log.TraceInfo("GETQUOTA quota=[unlimited].");
					else
					{
						string sLogStr = "GETQUOTA used=[" + nUsedKBytes.ToString() +
							"], total=[" + nTotalKBytes.ToString() + "]";
						Log.TraceInfo(sLogStr);
					}
				}
			}
			catch (Exception)
			{
				throw;
			}

		}

		/// <summary>
		/// Store flag
		/// </summary>
		/// <param name="sUid"></param>
		/// <param name="flag"> E.g \Deleted 
		/// \Seen
		///       Message has been read
		/// \Answered
		///       Message has been answered
		/// \Flagged
		///       Message is "flagged" for urgent/special attention
		/// \Deleted
		///       Message is "deleted" for removal by later EXPUNGE
		/// \Draft
		///       Message has not completed composition(marked as a draft).
		/// \Recent
		///       Message is "recently" arrived in this mailbox.This session
		///       is the first session to have been notified about this
		///       message; if the session is read-write, subsequent sessions
		///       will not see \Recent set for this message.This flag can not
		///       be altered by the client.
		///       If it is not possible to determine whether or not this
		///       session is the first session to be notified about a message,
		///       then that message SHOULD be considered recent.
		///       If multiple connections have the same mailbox selected
		///       simultaneously, it is undefined which of these connections
		///       will see newly-arrived messages with \Recent set and which
		///       will see it without \Recent set.
		/// </param>
		/// <param name="removeFlag">Remove the flag</param>
		public bool SetFlag(string sUid, string flag, bool removeFlag = false)
		{
			if (String.IsNullOrEmpty(sUid))
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM, "Invalid uid");
			}
			if (String.IsNullOrEmpty(flag))
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM, "Invalid flag");
			}
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;


			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_UIDSTORE_COMMAND;
			sCommand += " " + sUid + " " + (removeFlag ? IMAP_REMOVEFLAGS_COMMAND : IMAP_SETFLAGS_COMMAND) + " " + flag + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, asResultArray[0].ToString());
				}
			}
			catch (Exception)
			{
				throw;
			}

			return true;
		}

		/// <summary>
		/// Expunge
		/// </summary>
		public bool Expunge()
		{

			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;


			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_EXPUNGE_COMMAND;
			sCommand += IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, asResultArray[0].ToString());
				}
			}
			catch (Exception)
			{
				throw;
			}

			return true;
		}

		/// <summary>
		/// Move message to specified folder
		/// </summary>
		/// <param name="sUid">UID of the message</param>
		/// <param name="sFolderName"> Folder where you want to move the message</param>
		public bool MoveMessage(string sUid, string sFolderName)
		{
			if (CopyMessage(sUid, sFolderName))
			{
				if (SetFlag(sUid, "\\Deleted"))
					return Expunge();
			}

			return false;
		}



		/// <summary>
		/// Copy Message
		/// </summary>
		/// <param name="sUid">Either UID or range of uid e.g 1:2</param>
		/// <param name="sFolderName">Folder where it needs to be copied</param>
		public bool CopyMessage(string sUid, string sFolderName)
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;

			if (String.IsNullOrEmpty(sFolderName))
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM, "Invalid folder name.");
			}
			if (String.IsNullOrEmpty(sUid))
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM, "Invalid uid");
			}
			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}

			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_UIDCOPY_COMMAND;
			sCommand += " " + sUid + " " + sFolderName + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, asResultArray[0].ToString());
				}
			}
			catch (Exception)
			{
				throw;
			}

			return true;
		}


		/// <summary>
		/// Create Folder
		/// </summary>
		/// <param name="sFolderName">Folder where it needs to be copied</param>
		public bool CreateFolder(string sFolderName)
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;

			if (String.IsNullOrEmpty(sFolderName))
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM, "Invalid folder name.");
			}
			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}

			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_UIDCREATE_FOLDER_COMMAND;
			sCommand += " " + sFolderName + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, asResultArray[0].ToString());
				}
			}
			catch (Exception)
			{
				throw;
			}

			return true;
		}

		/// <summary>
		/// Get the message size
		/// </summary>
		/// <param name="sUid"></param>
		/// <returns>message size</returns>
		public long GetMessageSize(string sUid)
		{
			if (String.IsNullOrEmpty(sUid))
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM, "Invalid uid");
			}
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;


			if (!IsLoggedIn)
			{
				try
				{
					Restore(false);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}

			ArrayList asResultArray = new ArrayList();
			string sCommand = IMAP_UIDFETCH_COMMAND;
			sCommand += " " + sUid + " " + IMAP_RFC822_SIZE_COMMAND + IMAP_COMMAND_EOL;
			try
			{
				eImapResponse = SendAndReceive(sCommand, ref asResultArray);
				if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, asResultArray[0].ToString());
				}
				string sLastLine = IMAP_SERVER_RESPONSE_OK;
				string sBodyStruct = "";
				bool bResult = false;
				int nStart = -1;
				foreach (string sLine in asResultArray)
				{
					nStart = sLine.IndexOf(IMAP_FETCH_COMMAND);
					if (sLine.StartsWith(IMAP_UNTAGGED_RESPONSE_PREFIX) &&
						(nStart != -1))
					{
						sBodyStruct = sLine;
					}
					else if (sLine.StartsWith(sLastLine))
					{
						bResult = true;
						break;
					}
				}
				if (!bResult)
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHSIZE);
				}
				nStart = sBodyStruct.IndexOf(IMAP_RFC822_SIZE_COMMAND);
				int nEnd = sBodyStruct.IndexOf(")");

				long size = 0;
				if (nStart != -1 && nEnd != -1)
				{
					string sSize = sBodyStruct.Substring(nStart + IMAP_RFC822_SIZE_COMMAND.Length,
														nEnd - (nStart + IMAP_RFC822_SIZE_COMMAND.Length));
					size = Convert.ToUInt32(sSize.Trim());
				}

				return size;

			}
			catch (Exception)
			{
				throw;
			}
		}

		/// <summary>
		/// Search the messages of the folder
		/// </summary>
		public ArrayList SearchCommand(string command)
		{
			return SearchMessage(new string[] { command }, true);
		}


		/// <summary>
		/// Search the messages by specified criterias
		/// </summary>
		/// <param name="asSearchData">Search criterias</param>
		public ArrayList SearchMessage(string[] asSearchData, bool searchAsCommand = false)
		{
			ArrayList asSearchResult = new ArrayList();
			if (!IsLoggedIn)
			{
				try
				{
					Restore(true);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}


			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;

			string sCommandString = "";
			string sCommandSuffix = "";

			if (searchAsCommand)
			{
				sCommandSuffix = asSearchData[0];
				sCommandString = IMAP_SEARCH_COMMAND + " " + sCommandSuffix;
			}
			else
			{
				//--------------------------
				// PREPARE SEARCH KEY/VALUE

				foreach (string search in asSearchData)
				{
					if (search.Length == 0)
					{
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM); ;
					}

					if (sCommandSuffix.Length > 0)
						sCommandSuffix += " ";
					sCommandSuffix += @"""" + search.ToLower() + @"""";
				}

				sCommandString = IMAP_SEARCH_COMMAND + " OR BODY " + sCommandSuffix + " SUBJECT " + sCommandSuffix + " NOT DELETED";
			}

			sCommandString += IMAP_COMMAND_EOL;

			ArrayList asResultArray = new ArrayList();
			try
			{
				//-----------------------
				// SEND SEARCH REQUEST
				eImapResponse = SendAndReceive(sCommandString, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					//-------------------------
					// PARSE RESPONSE
					int nCount = asResultArray.Count;
					bool bResult = false;
					string sPrefix = IMAP_UNTAGGED_RESPONSE_PREFIX + " ";
					sPrefix += IMAP_SEARCH_RESPONSE;
					foreach (string sLine in asResultArray)
					{
						int nPos = sLine.IndexOf(sPrefix);
						if (nPos != -1)
						{
							nPos += sPrefix.Length;
							string sSuffix = sLine.Substring(nPos);
							sSuffix = sSuffix.Trim();
							string[] asSearchRes = sSuffix.Split(' ');
							foreach (string sResultLine in asSearchRes)
							{
								if (!String.IsNullOrEmpty(sResultLine))
									asSearchResult.Add(sResultLine);
							}
							TotalMessages = asSearchResult.Count;
							bResult = true;
							break;
						}
					}
					if (!bResult)
					{
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, sCommandSuffix);
					}
				}
				else
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_SEARCH, asResultArray[0].ToString());
			}
			catch (ImapException)
			{
				LogOut();
				throw;
			}

			return asSearchResult;
		}


		/// <summary>
		/// Fetch the body for part "1"
		/// </summary>
		/// <param name="sMessageUID"> Message uid</param>
		/// <returns></returns>
		public string FetchPartBody(string sMessageUID)
		{
			return FetchPartBody(sMessageUID, null);
		}

		/// <summary>
		/// Fetch the body for specified part
		/// </summary>
		/// <param name="sMessageUID"> Message uid</param>
		/// <param name="sMessagePart">Message part</param>
		/// <returns></returns>
		public string FetchPartBody(string sMessageUID, string sMessagePart)
		{
			string sData = "";
			if (!IsLoggedIn)
			{
				try
				{
					Restore(true);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}
			if (sMessageUID.Length == 0)
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM);

			if (String.IsNullOrEmpty(sMessagePart))
				sMessagePart = IMAP_MSG_DEFAULT_PART;
			try
			{
				sData = GetBody(sMessageUID, sMessagePart);
			}
			catch (ImapException)
			{
				throw;
			}

			return sData;
		}


		/// <summary>
		/// Fetch Header of the message uid and part
		/// </summary>
		/// <param name="sMessageUID"> Message UID</param>
		public ArrayList FetchPartHeader(string sMessageUID)
		{
			return FetchPartHeader(sMessageUID, null);
		}

		/// <summary>
		/// Fetch Header of the message uid and part
		/// </summary>
		/// <param name="sMessageUID"> Message UID</param>
		/// <param name="sMessagePart"> Message part</param>
		public ArrayList FetchPartHeader(string sMessageUID, string sMessagePart)
		{
			if (!IsLoggedIn)
			{
				try
				{
					Restore(true);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}
			if (sMessageUID.Length == 0)
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDPARAM);

			if (String.IsNullOrEmpty(sMessagePart))
				sMessagePart = IMAP_MSG_DEFAULT_PART;
			try
			{
				return GetHeader(sMessageUID, sMessagePart);
			}
			catch (ImapException)
			{
				throw;
			}
		}


		/// <summary>
		/// Fetch the full message in ImapEmail
		/// </summary>
		/// <param name="sMessageUID">Message UID </param>
		/// <param name="oXmlDoc">Message is stored as XmlDocument object</param>
		public ImapEmail FetchMessage(string sMessageUID, bool bFetchBody)
		{
			if (!IsLoggedIn)
			{
				try
				{
					Restore(true);
				}
				catch (ImapException e)
				{
					if (e.Type != ImapException.ImapErrorEnum.IMAP_ERR_INSUFFICIENT_DATA)
						throw;

					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTCONNECTED);
				}
			}
			if (!IsFolderSelected && !IsFolderExamined)
			{
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_NOTSELECTED);
			}
			try
			{
				ImapEmail imapEmail = new ImapEmail();
				imapEmail.Uid = sMessageUID;
				ImapEmailPart imapEmailPart = new ImapEmailPart();
				string sPartNumber = "0";

				ArrayList asMessageHeader = GetHeader(sMessageUID, sPartNumber);

				int nCount = asMessageHeader.Count;
				for (int i = 0; i < nCount; i = i + 2)
				{
					string header = asMessageHeader[i].ToString().ToLower();
					string value = asMessageHeader[i + 1].ToString();

					imapEmailPart.Headers.Add(header, value);

					switch (header)
					{
						case "from":
							imapEmail.From = value;
							break;
						case "to":
							imapEmail.To = value;
							break;
						case "subject":
							imapEmail.Subject = value;
							break;
						case "date":
							imapEmail.Date = DateTimeUtil.UtcToDateTime(value);
							break;
						case IMAP_MESSAGE_CONTENT_TYPE:
							imapEmail.IsMultipart = IsMultipart(value);
							break;
					}
				}

				if (imapEmail.IsMultipart)
					imapEmailPart.ID = "0";
				else
					imapEmailPart.ID = "1";

				imapEmail.Parts.Add(imapEmailPart);

				GetBodyStructure(sMessageUID, bFetchBody, imapEmail);

				DecodeParts(imapEmail);

				return imapEmail;
			}
			catch (ImapException)
			{
				throw;
			}
		}

		/// <summary>
		/// Get the Body structure of the message.
		/// If message is single part then first part is 1
		/// If message is multipart then first part is 0
		/// </summary>
		/// <param name="sMessageUID"> Message UID</param>
		void GetBodyStructure(string sMessageUID, bool bFetchBody, ImapEmail imapEmail)
		{
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			string sCommandSuffix = sMessageUID + " " + "BODYSTRUCTURE";
			string sCommandString = IMAP_UIDFETCH_COMMAND + " " + sCommandSuffix + IMAP_COMMAND_EOL;

			try
			{
				//-----------------------
				// SEND SEARCH REQUEST
				ArrayList asResultArray = new ArrayList();
				eImapResponse = SendAndReceive(sCommandString, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					string sLastLine = IMAP_SERVER_RESPONSE_OK;
					string sBodyStruct = "";
					bool bResult = false;
					int nStart = -1;
					foreach (string sLine in asResultArray)
					{
						nStart = sLine.IndexOf(IMAP_BODYSTRUCTURE_COMMAND);
						if (sLine.StartsWith(IMAP_UNTAGGED_RESPONSE_PREFIX) &&
							(nStart != -1))
						{
							sBodyStruct = sLine;
						}
						else if (sLine.StartsWith(sLastLine))
						{
							bResult = true;
							break;
						}
					}
					if (!bResult)
					{
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHBODYSTRUCT);
					}
					else if (sBodyStruct.Length == 0)
					{
						Log.TraceInfo("Bodystructure is empty");
						return;
					}
					nStart = sBodyStruct.IndexOf(IMAP_BODYSTRUCTURE_COMMAND);
					sBodyStruct = sBodyStruct.Substring(nStart + IMAP_BODYSTRUCTURE_COMMAND.Length);
					int nEnd = sBodyStruct.LastIndexOf(")");
					if (nEnd == -1)
					{
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHBODYSTRUCT);
					}
					sBodyStruct = sBodyStruct.Substring(0, nEnd);

					if (!ParseBodyStructure(sMessageUID, ref sBodyStruct, ref imapEmail, "", bFetchBody))
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHBODYSTRUCT);
				}
				else
				{
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG, sCommandSuffix);
				}

			}
			catch (ImapException)
			{
				LogOut();
				throw;
			}
		}


		/// <summary>
		/// Parse the bodystructure and store as XML Element
		/// Mejora: Utilizar REGEX para procesar las partes entre paréntesis.
		/// </summary>
		/// <param name="sBodyStruct">Bosy Structure</param>
		/// <param name="sPartPrefix">Part Prefix</param>
		/// <returns>true/false</returns>
		bool ParseBodyStructure(string sMessageUID, ref string sBodyStruct, ref ImapEmail imapEmail,
			string sPartPrefix, bool bFetchBody)
		{
			bool bMultiPart = false;
			string sTemp = "";
			ArrayList asAttrs = new ArrayList();
			sBodyStruct = sBodyStruct.Trim();

			//Check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;
			//Look for '('
			if (sBodyStruct[0] == '(')
			{
				//Check if multipart or singlepart.
				//Multipart will have another '(' here
				// and single part will not.
				char ch;
				ch = sBodyStruct[1];
				if (ch != '(')
				{
					//Singal part
					if (ch == ')')
					{
						sBodyStruct = sBodyStruct.Substring(2);
						return true;
					}
					//remove opening paranthesis
					sBodyStruct = sBodyStruct.Substring(1);
					string sType = "";
					string sSubType = "";
					if (!GetContentType(ref sBodyStruct, ref sType, ref sSubType, ref sTemp))
					{
						return false;
					}
					asAttrs.Add(IMAP_MESSAGE_CONTENT_TYPE);
					asAttrs.Add(sTemp);

					// Message-id (optional)
					if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
					{
						Log.TraceError("Failed getting Message Id.");
						return false;
					}
					if (sTemp.Length > 0)
					{
						asAttrs.Add(IMAP_MESSAGE_ID);
						asAttrs.Add(sTemp);
					}
					// Content-Description (optional)
					if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
					{
						Log.TraceError("Failed getting the Content Desc.");
						return false;
					}
					if (sTemp.Length > 0)
					{
						asAttrs.Add(IMAP_MESSAGE_CONTENT_DESC);
						asAttrs.Add(sTemp);
					}

					// Content-Transfer-Encoding
					if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
					{
						Log.TraceError("Failed getting the Content Encoding.");
						return false;
					}
					asAttrs.Add(IMAP_MESSAGE_CONTENT_ENCODING);
					asAttrs.Add(sTemp);

					// Content Size in bytes
					if (!ParseString(ref sBodyStruct, ref sTemp))
					{
						Log.TraceError("Failed getting the Content Size.");
						return false;
					}
					asAttrs.Add(IMAP_MESSAGE_CONTENT_SIZE);
					asAttrs.Add(sTemp);
					sTemp = sType + "/" + sSubType;
					if (sTemp.ToLower() == IMAP_MESSAGE_RFC822.ToLower()) //email attachment
					{
						if (!ParseEnvelope(ref sBodyStruct, asAttrs))
						{
							Log.TraceError("Failed getting the Message Envelope.");
							return false;
						}

						if (!ParseBodyStructure(sMessageUID, ref sBodyStruct, ref imapEmail, sPartPrefix, bFetchBody))
						{
							Log.TraceError("Failed getting Attached Message.");
							return false;
						}
						// No of Lines in the message
						if (!ParseString(ref sBodyStruct, ref sTemp))
						{
							Log.TraceError("Failed getting the Content Lines.");
							return false;
						}
						if (sTemp.Length > 0)
						{
							asAttrs.Add(IMAP_MESSAGE_CONTENT_LINES);
							asAttrs.Add(sTemp);
						}
					}
					else if (sType.ToLower() == "text") //simple text
					{
						// No of Lines in the message
						if (!ParseString(ref sBodyStruct, ref sTemp))
						{
							Log.TraceError("Failed getting the Content Lines.");
							return false;
						}
						if (sTemp.Length > 0)
						{
							asAttrs.Add(IMAP_MESSAGE_CONTENT_LINES);
							asAttrs.Add(sTemp);
						}
					}
					// MD5 CRC Error Check
					// Don't know what to do with it
					if (sBodyStruct[0] == ' ')
					{
						if (!ParseString(ref sBodyStruct, ref sTemp))
							return false;
					}
				}
				else // MULTIPART
				{
					bMultiPart = true;
					// remove the open paranthesis
					sBodyStruct = sBodyStruct.Substring(1);
					uint nPartNumber = 0;
					string sPartNumber = "";
					do
					{
						// prepare next part number
						nPartNumber++;

						if (sPartPrefix.Length > 0)
							sPartNumber = sPartPrefix + "." + nPartNumber.ToString();
						else
							sPartNumber = nPartNumber.ToString();

						ImapEmailPart imapEmailPart = new ImapEmailPart();
						imapEmailPart.ID = sPartNumber;

						// add a new child to the part with
						// an empty attribute array. This array will be filled
						// in the "if" condition block.
						int nCount = asAttrs.Count;
						for (int i = 0; i < nCount; i = i + 2)
						{
							imapEmailPart.Headers.Add(asAttrs[i].ToString(), asAttrs[i + 1].ToString());
						}
						if (sPartNumber.Length > 0 &&
							sPartNumber != "0" &&
							bFetchBody == true)
						{
							try
							{
								//string sData = "";
								imapEmailPart.Data = GetBody(sMessageUID, sPartNumber);
								//if (sData.Length > 0 &&
								//	((sData.ToLower()).IndexOf(IMAP_MESSAGE_CONTENT_TYPE.ToLower()) == -1))
								//{
								//	imapEmailPart.Data = sData;
								//}
							}
							catch (ImapException e)
							{
								Log.TraceError("Exception:Invalid Body Structure. Error:" + e.Message);
								return false;
							}
						}

						imapEmail.Parts.Add(imapEmailPart);

						// add a new child to the part with
						// an empty attribute array. This array will be filled
						// in the "if" condition block.
						//oXmlBodyPart.AppendChild(oXmlChildPart);
						// parse this body part
						if (!ParseBodyStructure(sMessageUID, ref sBodyStruct, ref imapEmail, sPartNumber, bFetchBody))
						{
							return false;
						}
					}
					while (sBodyStruct[0] == '('); // For each body part
												   // Content-type
					string sType = IMAP_MESSAGE_MULTIPART;
					string sSubType = "";
					if (!GetContentType(ref sBodyStruct, ref sType, ref sSubType,
						ref sTemp))
					{
						return false;
					}
					asAttrs.Add(IMAP_MESSAGE_CONTENT_TYPE);
					asAttrs.Add(sTemp);
				}
				//----------------------------------
				// COMMON FOR SINGLE AND MULTI PART

				// Disposition
				if (sBodyStruct[0] == ' ')
				{
					if (!GetContentDisposition(ref sBodyStruct, ref sTemp))
					{
						Log.TraceError("Failed getting the Content Disp.");
						return false;
					}
					if (sTemp.Length > 0)
					{
						asAttrs.Add(IMAP_MESSAGE_CONTENT_DISP);
						asAttrs.Add(sTemp);
					}
				}
				// Language
				if (sBodyStruct[0] == ' ')
				{
					if (!ParseLanguage(ref sBodyStruct, ref sTemp))
						return false;
				}
				// Extension data
				while (sBodyStruct[0] == ' ')
					if (!ParseExtension(ref sBodyStruct, ref sTemp))
						return false;

				// this is the end of the body part
				if (!FindAndRemove(ref sBodyStruct, ')'))
					return false;

				// Finally, set the attribute array to the part
				// EXCEPTION : if multipart and this is the root
				// part, the header is already prepared in the
				// GetBodyStructure function and hence DO NOT set it.

				if (!bMultiPart || sPartPrefix.Length > 0)
				{
					int nCount = asAttrs.Count;
					for (int i = 0; i < nCount; i = i + 2)
					{
						imapEmail.Parts[imapEmail.Parts.Count - 1].Headers.Add(asAttrs[i].ToString(), asAttrs[i + 1].ToString());
					}
				}
				return true;
			}

			Log.TraceError("Invalid Body Structure");
			return false;
		}


		/// <summary>
		/// Decodifica el valor de Data.
		/// </summary>
		/// <typeparam name="ImapEmail">The type of the map email.</typeparam>
		/// <returns></returns>
		public void DecodeParts(ImapEmail imapEmail)
		{
			foreach (ImapEmailPart part in imapEmail.Parts)
				if (part.Headers["content-transfer-encoding"] != null)
				{
					if (part.Headers[IMAP_MESSAGE_CONTENT_TRANSFER_ENCODING].ToLower().IndexOf(IMAP_MESSAGE_BASE64_ENCODING) >= 0)
					{
						part.Data = FSCrypto.Base64.Decode(part.Data);
					}
					if (part.Headers["content-transfer-encoding"].ToLower().IndexOf(IMAP_MESSAGE_QUOTED_PRINTABLE_ENCODING) >= 0)
					{
						part.Data = TextUtil.ConvertHexToAscii(part.Data);
					}
				}
		}

		/// <summary>
		/// Get the message body for specified part
		/// </summary>
		/// <param name="sMessageUid">Message uid</param>
		/// <param name="sPartNumber">Body part</param>
		string GetBody(string sMessageUid, string sPartNumber)
		{
			string sData = "";
			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			string sCommandSuffix = "";
			sCommandSuffix = sMessageUid + " " + "BODY[" + sPartNumber + "]";
			string sCommandString = IMAP_UIDFETCH_COMMAND + " " + sCommandSuffix + IMAP_COMMAND_EOL;

			try
			{
				//-----------------------
				// SEND SEARCH REQUEST
				ArrayList asResultArray = new ArrayList();
				eImapResponse = SendAndReceiveByNumLines(sCommandString, ref asResultArray, 1);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					//-------------------------
					// PARSE RESPONSE
					string sLastLine = IMAP_COMMAND_IDENTIFIER + " " + IMAP_OK_RESPONSE;
					string sLine = asResultArray[0].ToString();
					if (!sLine.StartsWith(IMAP_UNTAGGED_RESPONSE_PREFIX))
					{
						string sLog = "InValid Message Response " + sLine + ".";
						Log.TraceError(sLog);
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG);
					}
					long lMessageSize = GetResponseSize(sLine);
					if (lMessageSize == 0L)
					{
						string sLog = "InValid Message Response " + sLine + ".";
						Log.TraceError(sLog);
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG);
					}

					sData = ReceiveBuffer(Convert.ToInt32(lMessageSize));
					if (sData.Length == 0)
					{
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG, sCommandSuffix);
					}

					eImapResponse = Receive(ref asResultArray);
					if (eImapResponse != ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
						throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG, sCommandSuffix);

					/***
					 int nCount = asResultArray.Count;
					 for (int i = 1; i < nCount; i++)
					 {
						 string sTmpData = asResultArray[i].ToString();
						 if (sTmpData.Length > 0)
						 {
							 if (sTmpData[0] == ')' ||
								 (sTmpData.IndexOf(IMAP_SERVER_RESPONSE_OK) != -1))
							 {
								 break;
							 }
							 else
								 sData += sTmpData;
						 }

					 }****/
				}
				else
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG, sCommandSuffix);
			}
			catch (ImapException)
			{
				LogOut();
				throw;
			}

			return sData;
		}

		/// <summary>
		/// Get the header for specific partNumber and Message UID
		/// </summary>
		/// <param name="sMessageUid">Unique Identifier of message</param>
		/// <param name="sPartNumber"> Message part number</param>
		/// <param name="asMessageHeader">Return array </param>
		ArrayList GetHeader(string sMessageUid, string sPartNumber)
		{
			ArrayList asMessageHeader = new ArrayList();

			ImapResponseEnum eImapResponse = ImapResponseEnum.IMAP_SUCCESS_RESPONSE;
			string sCommandSuffix = "";
			if (sPartNumber == "0")
			{
				sCommandSuffix = sMessageUid + " " + "BODY[HEADER]";
			}
			else
			{
				sCommandSuffix = sMessageUid + " " + "BODY[" + sPartNumber + ".MIME]";
			}
			string sCommandString = IMAP_UIDFETCH_COMMAND + " " + sCommandSuffix + IMAP_COMMAND_EOL;

			try
			{
				//-----------------------
				// SEND SEARCH REQUEST
				ArrayList asResultArray = new ArrayList();
				eImapResponse = SendAndReceive(sCommandString, ref asResultArray);
				if (eImapResponse == ImapResponseEnum.IMAP_SUCCESS_RESPONSE)
				{
					//-------------------------
					// PARSE RESPONSE
					string sKey = "";
					string sValue = "";
					string sLastLine = IMAP_SERVER_RESPONSE_OK;
					foreach (string sLine in asResultArray)
					{
						if (sLine.Length <= 0 ||
							sLine.StartsWith(IMAP_UNTAGGED_RESPONSE_PREFIX) ||
							sLine == ")")
						{
							continue;
						}
						else if (sLine.StartsWith(sLastLine))
						{
							break;

						}
						int nPos = sLine.IndexOf(" ");
						if (nPos != -1)
						{
							string sTmpLine = sLine.Substring(0, nPos);
							nPos = sTmpLine.IndexOf(":");
						}
						if (nPos != -1)
						{
							if (sKey.Length > 0)
							{
								asMessageHeader.Add(sKey);
								asMessageHeader.Add(sValue);
							}
							sKey = sLine.Substring(0, nPos).Trim();
							sValue = sLine.Substring(nPos + 1).Trim();
						}
						else
						{
							sValue += sLine.Trim();
						}
					}
					if (sKey.Length > 0)
					{
						asMessageHeader.Add(sKey);
						asMessageHeader.Add(sValue);
					}

				}
				else
					throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_FETCHMSG, sCommandSuffix);
			}
			catch (ImapException)
			{
				LogOut();
				throw;
			}

			return asMessageHeader;
		}

		/// <summary>
		/// Check if this message is multipart
		/// To Identify multipart message, the content-type is either
		/// multipart or rfc822
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool IsMultipart(string value)
		{
			value = value.ToLower();
			if (value.StartsWith(IMAP_MESSAGE_MULTIPART.ToLower()) ||
				value.StartsWith(IMAP_MESSAGE_RFC822.ToLower()))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Returns true if starts with NIL
		/// </summary>
		/// <param name="sBodyStruct">Body Structure</param>
		/// <returns>true/false</returns>
		bool IsNilString(ref string sBodyStruct)
		{
			string sSub = sBodyStruct.Substring(0, 3);
			if (sSub == IMAP_MESSAGE_NIL)
			{
				sBodyStruct = sBodyStruct.Substring(3);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Get the content type
		/// </summary>
		/// <param name="sBodyStruct">Body Structure</param>
		/// <param name="sType">Content Type</param>
		/// <param name="sSubType">Sub Type</param>
		/// <param name="sContentType">Content Type Value</param>
		/// <returns>True/false</returns>
		bool GetContentType(ref string sBodyStruct,
			ref string sType,
			ref string sSubType,
			ref string sContentType)
		{
			sContentType = IMAP_PLAIN_TEXT;

			// get the type and the subtype strings from body struct
			// If not found, set it to the default value plain/text.

			if (sType.Length < 1)
			{
				if (!ParseQuotedString(ref sBodyStruct, ref sType))
				{
					Log.TraceError("Failed getting Content-Type.");
					return false;
				}
			}
			sSubType = "";
			if (!ParseQuotedString(ref sBodyStruct, ref sSubType))
			{
				Log.TraceError("Failed getting Content-Sub-Type.");
				return false;
			}

			if (sType.Length > 0 && sSubType.Length > 0)
			{
				sContentType = sType + "/" + sSubType;
			}

			// Add extra parameters (if any) to the Content-type
			ArrayList asParam = new ArrayList();
			if (!ParseParameters(ref sBodyStruct, asParam))
			{
				Log.TraceError("Failed getting Content-Type Parameters.");
				return false;
			}
			for (int i = 0; i < asParam.Count; i += 2)
			{
				string sTemp = "; " + asParam[i].ToString() + "=\"" + asParam[i + 1].ToString() + "\"";
				sContentType += sTemp;
			}
			return true;
		}
		/// <summary>
		/// Get Content Disposition
		/// </summary>
		/// <param name="sBodyStruct"> Body Structure</param>
		/// <param name="sDisp">Content Disposition</param>
		/// <returns>true if success</returns>
		bool GetContentDisposition(ref string sBodyStruct,
			ref string sDisp)
		{
			sDisp = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// find and remove opening paranthesis
			if (!FindAndRemove(ref sBodyStruct, '('))
				return false;

			// get the content disposition type (inline/attachment)
			if (!ParseQuotedString(ref sBodyStruct, ref sDisp))
			{
				Log.TraceError("Failed getting Content Disposition.");
				return false;
			}
			// get the associated parameters if any
			ArrayList asParam = new ArrayList();
			if (!ParseParameters(ref sBodyStruct, asParam))
			{
				Log.TraceError("Failed getting Content Disposition params.");
				return false;
			}
			// prepare the content-disposition
			string sTemp = "";
			for (int i = 0; i < asParam.Count; i += 2)
			{
				sTemp = "; " + asParam[i].ToString() + "=\"" + asParam[i + 1].ToString() + "\"";
				sDisp += sTemp;
			}
			// remove the closing paranthesis
			return FindAndRemove(ref sBodyStruct, ')');
		}
		/// <summary>
		/// Parse the quoted string in body structure
		/// </summary>
		/// <param name="sBodyStruct">Body Structure</param>
		/// <param name="sString">"Quoted string</param>
		/// <returns></returns>
		bool ParseQuotedString(ref string sBodyStruct, ref string sString)
		{
			sString = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			if (sBodyStruct[0] == '"')
			{
				// extract the part within quotes
				char ch;
				int nEnd;
				for (nEnd = 1; (ch = sBodyStruct[nEnd]) != '"'; nEnd++)
				{
					if (ch == '\\')
						nEnd++;
				}
				sString = sBodyStruct.Substring(1, nEnd - 1);
				sBodyStruct = sBodyStruct.Substring(nEnd + 1);
				return true;
			}
			string sLog = "InValid Body Structure " + sBodyStruct + ".";
			Log.TraceError(sLog);
			return false;
		}
		/// <summary>
		/// Parse the string (seperated by spaces or parenthesis)
		/// </summary>
		/// <param name="sBodyStruct">Body Struct</param>
		/// <param name="sString">string</param>
		/// <returns></returns>
		bool ParseString(ref string sBodyStruct, ref string sString)
		{
			sString = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();
			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// extract the literal as whole looking for a
			// space or closing paranthesis character
			char ch;
			int nEnd, nLen;
			nLen = sBodyStruct.Length;

			for (nEnd = 0; nEnd < nLen; nEnd++)
			{
				ch = sBodyStruct[nEnd];

				if (ch == ' ' || ch == ')')
					break;
			}

			if (nEnd > 0)
			{
				sString = sBodyStruct.Substring(0, nEnd);
				sBodyStruct = sBodyStruct.Substring(nEnd);
			}
			return true;
		}
		/// <summary>
		/// Parse the language or list of languages in body structure
		/// </summary>
		/// <param name="sBodyStruct">Bosy structure</param>
		/// <param name="sString">Languages</param>
		/// <returns>true if success</returns>

		bool ParseLanguage(ref string sBodyStruct, ref string sString)
		{
			sString = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			if (sBodyStruct[0] == '(')
			{ // Language list

				// remove the opening paranthesis
				if (!FindAndRemove(ref sBodyStruct, '('))
					return false;

				// TO DO
				// Logic for parsing language list is not yet
				// written. To be added in the future.

				// remove the closing paranthesis
				if (!FindAndRemove(ref sBodyStruct, ')'))
					return false;
			}
			else
			{ // One or no language

				if (!ParseQuotedString(ref sBodyStruct, ref sString))
				{
					Log.TraceError("Failed getting Content Language.");
					return false;
				}
			}

			return true;
		}
		/// <summary>
		/// Parse the parameter in body structure
		/// </summary>
		/// <param name="sBodyStruct">Body structure</param>
		/// <param name="asParams">parameter</param>
		/// <returns>true if success</returns>
		bool ParseParameters(ref string sBodyStruct,
			ArrayList asParams)
		{
			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// remove the opening paranthesis
			if (!FindAndRemove(ref sBodyStruct, '('))
				return false;

			string sName = "";
			string sValue = "";
			while (sBodyStruct[0] != ')')
			{

				// Name
				if (!ParseQuotedString(ref sBodyStruct, ref sName))
				{
					Log.TraceError("Invalid Body Parameter Name.");
					return false;
				}

				// Value
				if (!ParseQuotedString(ref sBodyStruct, ref sValue))
				{
					Log.TraceError("Invalid Body Parameter Value.");
					return false;
				}
				asParams.Add(sName);
				asParams.Add(sValue);
			}

			// remove the closing paranthesis
			return FindAndRemove(ref sBodyStruct, ')');
		}
		/// <summary>
		/// Parse the extension in body structure
		/// </summary>
		/// <param name="sBodyStruct">body structure</param>
		/// <param name="sString">extension</param>
		/// <returns>true if success</returns>
		bool ParseExtension(ref string sBodyStruct, ref string sString)
		{
			sString = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// TO DO
			// Dont know what to do with the data.

			return true;
		}
		/// <summary>
		/// Parse the address string
		/// </summary>
		/// <param name="sBodyStruct">body structure</param>
		/// <param name="sString">address</param>
		/// <returns>true if success</returns>
		bool ParseAddressList(ref string sBodyStruct, ref string sString)
		{
			sString = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// remove the opening paranthesis
			if (!FindAndRemove(ref sBodyStruct, '('))
				return false;

			// process each address
			string sAddress = "";
			while (sBodyStruct[0] == '(')
			{

				// Get each address in the list
				if (!ParseAddress(ref sBodyStruct, ref sAddress))
				{
					return true;
				}

				// prepare the address list (as comma separated
				// list of addresses).
				if (sAddress.Length > 0)
				{
					if (sString.Length > 0)
						sString += ", ";
					sString += sAddress;
				}

				sBodyStruct = sBodyStruct.Trim();
			}

			// remove the closing paranthesis
			return FindAndRemove(ref sBodyStruct, ')');
		}
		/// <summary>
		/// Parse one address and format the string
		/// </summary>
		/// <param name="sBodyStruct">body structure</param>
		/// <param name="sString">address</param>
		/// <returns>true if success</returns>
		bool ParseAddress(ref string sBodyStruct, ref string sString)
		{
			sString = "";

			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// remove the opening paranthesis
			if (!FindAndRemove(ref sBodyStruct, '('))
				return false;

			string sPersonal = "";
			string sEmailId = "";
			string sEmailDomain = "";
			string sTemp = "";

			// Personal Name
			if (!ParseQuotedString(ref sBodyStruct, ref sPersonal))
			{
				Log.TraceError("Failed getting the Personal Name.");
				return false;
			}
			// At Domain List (Right now, don't know what to do with this)
			if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Failed getting the Domain List.");
				return false;
			}
			// Email Id
			if (!ParseQuotedString(ref sBodyStruct, ref sEmailId))
			{
				Log.TraceError("Failed getting the Email Id.");
				return false;
			}
			// Email Domain
			if (!ParseQuotedString(ref sBodyStruct, ref sEmailDomain))
			{
				Log.TraceError("Failed getting the Email Domain.");
				return false;
			}

			if (sEmailId.Length > 0)
			{
				if (sPersonal.Length > 0)
				{
					if (sEmailDomain.Length > 0)
					{
						sString = sPersonal + " <" +
								  sEmailId + "@" +
								  sEmailDomain + ">";
					}
					else
					{
						sString = sPersonal + " <" +
								  sEmailId + ">";
					}
				}
				else
				{
					if (sEmailDomain.Length > 0)
					{
						sString = sEmailId + "@" + sEmailDomain;
					}
					else
					{
						sString = sEmailId;
					}
				}
			}

			// remove the closing paranthesis
			return FindAndRemove(ref sBodyStruct, ')');
		}

		bool ParseEnvelope(ref string sBodyStruct,
			ArrayList asAttrs)
		{
			// remove any spaces at the beginning and the end
			sBodyStruct = sBodyStruct.Trim();

			// check if this is NIL
			if (IsNilString(ref sBodyStruct))
				return true;

			// look for '(' character
			if (!FindAndRemove(ref sBodyStruct, '('))
				return false;

			string sTemp = "";
			// Date
			if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope Date.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_DATE_TAG);
				asAttrs.Add(sTemp);
			}

			// Subject
			if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope Subject.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_SUBJECT_TAG);
				asAttrs.Add(sTemp);
			}

			// From
			if (!ParseAddressList(ref sBodyStruct, ref sTemp))
			{
				Log.TraceInfo("Invalid Message Envelope From.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_FROM_TAG);
				asAttrs.Add(sTemp);
			}

			// Sender
			if (!ParseAddressList(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope Sender.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_SENDER_TAG);
				asAttrs.Add(sTemp);
			}

			// ReplyTo
			if (!ParseAddressList(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope Reply-To.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_REPLY_TO_TAG);
				asAttrs.Add(sTemp);
			}

			// To
			if (!ParseAddressList(ref sBodyStruct, ref sTemp))
			{
				Log.TraceInfo("Invalid Message Envelope To.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_TO_TAG);
				asAttrs.Add(sTemp);
			}

			// Cc
			if (!ParseAddressList(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope CC.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_CC_TAG);
				asAttrs.Add(sTemp);
			}

			// Bcc
			if (!ParseAddressList(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope BCC.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_BCC_TAG);
				asAttrs.Add(sTemp);
			}

			// In-Reply-To
			if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope In-Reply-To.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_HEADER_IN_REPLY_TO_TAG);
				asAttrs.Add(sTemp);
			}

			// Message Id
			if (!ParseQuotedString(ref sBodyStruct, ref sTemp))
			{
				Log.TraceError("Invalid Message Envelope Message Id.");
				return false;
			}
			if (sTemp.Length > 0)
			{
				asAttrs.Add(IMAP_MESSAGE_ID);
				asAttrs.Add(sTemp);
			}

			// remove the closing paranthesis
			return FindAndRemove(ref sBodyStruct, ')');
		}
		/// <summary>
		/// find the given character and remove
		/// </summary>
		/// <param name="sBodyStruct">body structure</param>
		/// <param name="ch">first character to find and remove</param>
		/// <returns>true if success</returns>
		bool FindAndRemove(ref string sBodyStruct, char ch)
		{
			sBodyStruct = sBodyStruct.Trim();
			if (sBodyStruct[0] != ch)
			{
				string sLog = "Invalid Body Structure " + sBodyStruct + ".";
				Log.TraceError(sLog);
				return false;
			}

			// remove character
			sBodyStruct = sBodyStruct.Substring(1);

			return true;
		}
		/// <summary>
		/// Get the Size of the fetch command response
		/// response will look like "{<size>}"
		/// </summary>
		/// <param name="sResponse"></param>
		/// <returns></returns>
		long GetResponseSize(string sResponse)
		{
			// check if there is a size element
			// if not, then the message part number is wrong

			if (sResponse.IndexOf(IMAP_MESSAGE_NIL) != -1)
			{
				Log.TraceError("Size 0. No Message after this.");
				return 0L;
			}


			int nStart = sResponse.IndexOf('{');
			if (nStart == -1)
			{
				string sLog = "Invalid size in Response " + sResponse + ".";
				Log.TraceError(sLog);
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDHEADER);
			}
			int nEnd = sResponse.IndexOf('}');
			if (nEnd == -1 || nEnd < nStart)
			{
				string sLog = "Invalid size in Response " + sResponse + ".";
				Log.TraceError(sLog);
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDHEADER);
			}

			string sSize = sResponse.Substring(nStart + 1, (nEnd - nStart - 1));
			long nSize = Convert.ToInt64(sSize, 10);

			if (nSize <= 0L)
			{
				string sLog = "Invalid size in Response " + sResponse + ".";
				Log.TraceError(sLog);
				throw new ImapException(ImapException.ImapErrorEnum.IMAP_ERR_INVALIDHEADER);
			}

			return nSize;
		}


		public void SaveXML(string sUid, string sFileName, bool bFetchBody)
		{
			//XmlTextWriter oXmlWriter = new XmlTextWriter(sFileName, System.Text.Encoding.UTF8);
			//oXmlWriter.Formatting = Formatting.Indented;
			//oXmlWriter.WriteStartDocument(true);
			//oXmlWriter.WriteStartElement("Message");
			//oXmlWriter.WriteAttributeString("UID", sUid);
			//FetchMessage(sUid, oXmlWriter, bFetchBody);
			//oXmlWriter.WriteEndElement();
			//oXmlWriter.WriteEndDocument();
			//oXmlWriter.Flush();
			//oXmlWriter.Close();
		}
	}

	public class ImapEmail
	{
		public DateTime Date { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public string Subject { get; set; }
		public string Uid { get; set; }
		public bool IsMultipart { get; set; }

		public List<ImapEmailPart> Parts { get; set; }

		public ImapEmail()
		{
			Parts = new List<ImapEmailPart>();
		}
	}

	public class ImapEmailPart
	{
		public string ID;
		public string Data { get; set; }
		public NameValueCollection Headers { get; set; }

		public ImapEmailPart()
		{
			Headers = new NameValueCollection();
		}
	}
}