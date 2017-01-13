using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SendEmailCSI.Common
{
    public static class Helper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            List<T> list = new List<T>();

            try
            {
                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Patient> DataTableToListPatient(DataTable dt)
        {
            var patients = new List<Patient>(dt.Rows.Count);

            foreach (DataRow row in dt.Rows)
            {
                var values = row.ItemArray;

                try
                {
                    var patient = new Patient()
                    {
                        Adm = row["Adm"].ToString(),
                        AdmNo = row["AdmNo"].ToString(),
                        AdmType = Convert.ToChar(row["AdmType"]),
                        PAADMDischgDate = Convert.ToDateTime(row["PAADMDischgDate"]),
                        visStatus = Convert.ToChar(row["visStatus"]),
                        AdmCpCode = row["admCpCode"].ToString(),
                        PapmiNo = row["papmiNo"].ToString(),
                        NationCode = row["NationCode"].ToString(),
                        NationDESC = row["NationDESC"].ToString(),
                        EMail = row["EMail"].ToString(),
                        OldNew = Convert.ToChar(row["oldNew"])
                    };
                    patients.Add(patient);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return patients;
        }

        public static List<SendMail> GetDataSendMailList(List<Patient> patients, List<SendMail> sendMails)
        {
            var sendMailsList = new List<SendMail>();

            // get diff papmino between 2 list
            var dataDiff = patients.Where(p => !sendMails.Any(s => p.PapmiNo == s.PapmiNo)).ToList();

            foreach (var data in dataDiff)
            {
                var sendMail = new SendMail
                {
                    Adm = data.Adm,
                    AdmNo = data.AdmNo,
                    PapmiNo = data.PapmiNo,
                    NationCode = data.NationCode,
                    NationDESC = data.NationDESC,
                    EMail = data.EMail,
                    PAADMDischgDate = data.PAADMDischgDate,
                };
                sendMailsList.Add(sendMail);
            }

            return sendMailsList;
        }

        public static List<SendMail> SendMailForSurvey(List<SendMail> sendmailList)
        {
            var result = new List<SendMail>();



            return result;
        }

        

        public static string GetLinkSurvey(string nationCode, char OldNew)
        {
            string link = "";

            /** <summary>Link Survey</summary> 
            ภาษาอังกฤษ
            ผู้ป่วยใหม่ -> https://www.surveymonkey.com/r/QX2VRSK 
            ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/QXKV68J 

            ภาษาพม่า 
            ผู้ป่วยใหม่ -> https://www.surveymonkey.com/r/V3XX8ZZ  
            ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/V3PXPKD  
 
            ภาษาอาหรับ
            ผู้ป่วยใหม่ -> https://www.surveymonkey.com/r/7FWBZ3T  
            ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/7BDVXDF  

            ภาษาญี่ปุ่น
            ผู้ป่วยใหม่ -> https://www.surveymonkey.com/r/DT5ZVRG 
            ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/DTZKWB8 

            ภาษาไทย 
            ผู้ป่วยใหม่> https://www.surveymonkey.com/r/G58GMKW
            ผู้ป่วยเก่า-> https://www.surveymonkey.com/r/WCH72Q9

             */
            #region Link for old patient 
            if (char.ToUpper(OldNew) == 'O')
            {
                switch (nationCode.ToUpper())
                {
                    case "TH":
                        link = "https://www.surveymonkey.com/r/WCH72Q9";
                        break;
                    case "SY":
                    case "IR":
                    case "IS":
                        link = "https://www.surveymonkey.com/r/7BDVXDF";
                        break;
                    case "JP":
                        link = "https://www.surveymonkey.com/r/DTZKWB8s";
                        break;
                    case "MR":
                        link = "https://www.surveymonkey.com/r/V3PXPKD";
                        break;
                    //case "CN":
                    //    link = "";
                    //    break;
                    default:
                        link = "https://www.surveymonkey.com/r/QXKV68J";
                        break;
                }
            }
            #endregion
            #region Link for new patient
            else
            {
                switch (nationCode.ToUpper())
                {
                    case "TH":
                        link = " https://www.surveymonkey.com/r/G58GMKW";
                        break;
                    case "SY":
                    case "IR":
                    case "IS":
                        link = "https://www.surveymonkey.com/r/7FWBZ3T";
                        break;
                    case "JP":
                        link = "https://www.surveymonkey.com/r/DT5ZVRG";
                        break;
                    case "MR":
                        link = "https://www.surveymonkey.com/r/V3XX8ZZ";
                        break;
                    //case "CN":
                    //    link = "";
                    //    break;
                    default:
                        link = "https://www.surveymonkey.com/r/QX2VRSK";
                        break;
                }
            }
            #endregion
            return link;
        }
    }
}
