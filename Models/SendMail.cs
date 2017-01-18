using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SendEmailCSI.Models
{
    public class SendMail
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Adm { get; set; }
        [MaxLength(15)]
        public string AdmNo { get; set; }
        [MaxLength(15)]
        public string PapmiNo { get; set; }
        [MaxLength(5)]
        public string NationCode { get; set; }
        public string NationDESC { get; set; }
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }
        [Column(TypeName ="char")]
        [MaxLength(1)]
        public string OldNew { get; set; }
        public DateTime PAADMDischgDate { get; set; }
        public DateTime SendDate { get; set; }
        public string SendFlag { get; set; }
        public string Link { get; set; }
    }
}
