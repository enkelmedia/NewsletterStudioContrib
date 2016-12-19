using NewsletterStudio.Services.Mail;

namespace NewsletterStudioContrib.BounceManagement.Models
{
    /// <summary>
    /// Represents an email. Add additional logic here if needed
    /// </summary>
    /// <seealso cref="NewsletterStudio.Services.Mail.IBounceMessage" />
    class ImapBouncedEmail : IBounceMessage
    {
        public int Id { get; set; }

        public string OriginalHeader { get; set; }
        public string Subject { get; set; }

        public string ReceiverEmail { get; set; }

        public bool Downloaded { get; set; }

        public string RawBody { get; set; }

        public BounceType BounceType { get; set; }

        public bool Deleted { get; set; }

        public int Status { get; set; }

        public static IBounceMessage CreateMessage(int id)
        {
            return null;
        }


        public ImapBouncedEmail(string subject, string body, string email, string originalHeader)
        {
            this.Subject = subject;
            this.RawBody = body;
            this.ReceiverEmail = email;
            this.OriginalHeader = originalHeader;
        }

        public void ProcessRawBody(string rawBody)
        {
            this.BounceType = this.FigureOutBounceType((IBounceMessage)this);
            this.Downloaded = true;
        }

        public BounceType FigureOutBounceType(IBounceMessage message)
        {
            //PUT your logic here, for example
            if (message.RawBody.Contains("500"))
            {
                return BounceType.HardBounce;
            }
            else
            {
                return BounceType.SoftBounce;
            }

        }
    }
}