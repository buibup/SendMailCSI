using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SendEmailCSI.Models
{
    public class Languages
    {
        [Key]
        public int LanguagesId { get; set; }
        [MaxLength(10)]
        public string Code { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

        public virtual SettingMail SettingMail { get; set; }
    }
}
