using System.Net.Mail;

namespace Chronicle.Logging.Business.Notifications
{
    /// <summary>Base class to send emails</summary>
    public abstract class Emailer
    {
        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public string ToAddress { get; set; }

        public string ToName { get; set; }

        public string SubjectLine { get; set; }

        public string BodyText { get; set; }

        /// <summary>
        ///     Send email
        /// </summary>
        public void Send()
        {
            var fromAddress = new MailAddress(FromAddress, FromName);
            var toAddress = new MailAddress(ToAddress, ToName);

            var smtp = new SmtpClient();
            using (var message = new MailMessage(fromAddress, toAddress) {Subject = SubjectLine, Body = BodyText})
            {
                smtp.Send(message);
            }
        }
    }
}