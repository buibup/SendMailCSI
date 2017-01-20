using System;

namespace SendEmailCSI.Models
{
    public class Patient
    {
        public string Adm { get; set; }
        public string AdmNo { get; set; }
        public char AdmType { get; set; }
        public DateTime PAADMDischgDate { get; set; }
        public char visStatus { get; set; }
        public string AdmCpCode { get; set; }
        public string PapmiNo { get; set; }
        public string NationCode { get; set; }
        public string NationDESC { get; set; }
        public string EMail { get; set; }
        public string OldNew { get; set; }
    }
}
