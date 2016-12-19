/// <summary>
/// 
/// </summary>
namespace NewsletterStudioContrib.BounceManagement.Connector
{
    using NewsletterStudio.Services.Mail;
    using System;
    using System.Collections.Generic;
    using System.IO;


    /// <summary>
    /// Redefines to comply to API
    /// </summary>
    internal interface IMailConnector
    {
        string Host { get; set; }

        int Port { get; set; }

        string User { get; set; }

        string Password { get; set; }
    }

    /// <summary>
    /// Intended to be plugged into the Newsletter studio Bounce management settings screen
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="NewsletterStudioContrib.BounceManagement.NewsletterStudioContrib.BounceManagement.Connector.IMailConnector" />
    public class ImapBounceConnector : IDisposable, IMailConnector
    {
        private List<IBounceMessage> _emails = new List<IBounceMessage>();

        private ImapX.ImapClient _imapClient;

        private StreamReader _strReader;
        private bool listPulled;

        public string Host { get; set; }

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public bool IsLoggedin { get; set; }

        public int ProcessedEmails { get; internal set; }

        public int TotalBounceEmails
        {
            get
            {
                if (!this.listPulled)
                    this.PullList();
                return this._emails.Count;
            }
        }

        public event ProcessEmailEventHandler ProcessEmail;

        public ImapBounceConnector(string host, int port)
        {
            this.Host = host;
            this.Port = port;
        }

        public bool Connect()
        {
            this._imapClient = new ImapX.ImapClient(this.Host, this.Port);
            this._imapClient.Connect();
            this._imapClient.Login(User, Password);

            var connectionResult = this._imapClient.IsConnected;
            return connectionResult;
        }


        public void PullList()
        {
            if (this._imapClient == null || this._imapClient.IsConnected == false)
                throw new Exception("Imap client not connected; Bad credentials ? ");

            try
            {
                //Look for all UNSEEN message in INBOX fodler
                ImapX.Message[] messages = _imapClient.Folders.Inbox.Search("UNSEEN", ImapX.Enums.MessageFetchMode.Full); // true - means all message parts will be received from server
                foreach (var message in messages)
                {

                    //mark mail as seen in any case
                    message.Seen = true;
                }
            }
            catch (Exception ex)
            {
                //TODO handle exception here
            }
            this.listPulled = true;
        }

        public void CommitAndQuit()
        {
            this._imapClient.Dispose(); ;
            this._emails = (List<IBounceMessage>)null;
        }

        public void Dispose()
        {
            this.CommitAndQuit();
        }
        public List<IBounceMessage> Messages()

        {
            return this._emails;
        }
    }
}
