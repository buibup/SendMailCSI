using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SendEmailCSI.Models
{
    public class SettingMail
    {
        [Key, ForeignKey("Languages")]
        public int LanguagesId { get; set; }
        [MaxLength(250)]
        public string Subject { get; set; }
        [MaxLength(250)]
        public string MailFrom { get; set; }
        public string MailBody { get; set; }
        [MaxLength(250)]
        public string SmtpClient { get; set; }

        public virtual Languages Languages { get; set; }
    }
}
