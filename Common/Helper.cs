using FastMember;
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataTable SendMailListToDataTable(List<SendMail> patientsSendMail)
        {
            DataTable dt = new DataTable();
            using (var reader = ObjectReader.Create(patientsSendMail))
            {
                dt.Load(reader);
            }
            return dt;
        }

        public static DataTable SendMailDataGridView(List<SendMail> patiens)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Hn", typeof(string));
            dt.Columns.Add("Episode", typeof(string));
            dt.Columns.Add("NationDes", typeof(string));
            dt.Columns.Add("EMail", typeof(string));
            dt.Columns.Add("OldNew", typeof(string));
            dt.Columns.Add("DischageDate", typeof(string));
            dt.Columns.Add("SendDate", typeof(string));
            dt.Columns.Add("SendFlag", typeof(string));
            dt.Columns.Add("Link", typeof(string));


            foreach (var p in patiens)
            {
                var sendDate = p.SendDate.Year == DateTime.Now.Year ? p.SendDate.ToString("dd/MM/yyyy") : "";
                dt.Rows.Add(p.PapmiNo, p.AdmNo, p.NationDESC, p.EMail, p.OldNew, p.PAADMDischgDate.ToString("dd/MM/yyyy"), sendDate, p.SendFlag, p.Link);
            }

            return dt;
        }

        public static DataTable GetDataTableFromObject(object[] objects)
        {
            if(objects !=null && objects.Length > 0)
            {
                Type T = objects[0].GetType();
                DataTable dt = new DataTable(T.Name);
                foreach(PropertyInfo pi in T.GetProperties())
                {
                    dt.Columns.Add(new DataColumn(pi.Name));
                }
                foreach(var o in objects)
                {
                    DataRow dr = dt.NewRow();
                    foreach(DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = o.GetType().GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return null;
        }

        // convert ListSendMail object to datatable
        public static DataTable ObjectToDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dt = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dt.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity, values);
                }
                dt.Rows.Add(values);
            }

            return dt;
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
                        OldNew = row["oldNew"].ToString()
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
                    OldNew = data.OldNew
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



        public static string GetLinkSurvey(string nationCode, string OldNew)
        {
            string link = "";

            /** <summary>Link Survey</summary> 
            ภาษาไทย         
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/WCH72Q9 
                ผู้ป่วยใหม่-> https://www.surveymonkey.com/r/G58GMKW
            ภาษาอังกฤษ     
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/QXKV68J 
                ผู้ป่วยใหม่-> https://www.surveymonkey.com/r/QX2VRSK 
            ภาษาอาหรับ     
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/7BDVXDF 
                ผู้ป่วยใหม่ -> https://www.surveymonkey.com/r/7FWBZ3T
            ภาษาพม่า         
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/V3PXPKD
                ผู้ป่วยใหม่-> https://www.surveymonkey.com/r/V3XX8ZZ 
            ภาษาญี่ปุ่น       
                ผู้ป่วยใหม่ -> https://www.surveymonkey.com/r/DT5ZVRG 
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/DTZKWB8 
            ภาษาจีน - SVH 
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/KQL9RBM 
                ผู้ป่วยใหม่-> https://www.surveymonkey.com/r/KJNT8V6
            ภาษาจีน - SNH 
                ผู้ป่วยเก่า -> https://www.surveymonkey.com/r/KQWLBZZ
                ผู้ป่วยใหม่-> https://www.surveymonkey.com/r/KQ2YSSR 

             */
            #region Link for old patient 
            if (OldNew.ToUpper().ToString() == "O")
            {
                switch (nationCode.ToUpper())
                {
                    case "TH":
                        link = "https://www.surveymonkey.com/r/WCH72Q9";
                        break;
                    case "AR":
                        link = "https://www.surveymonkey.com/r/7BDVXDF";
                        break;
                    case "JP":
                        link = "https://www.surveymonkey.com/r/DTZKWB8";
                        break;
                    case "MR":
                        link = "https://www.surveymonkey.com/r/V3PXPKD";
                        break;
                    case "CNSVH":
                        link = "https://www.surveymonkey.com/r/KQL9RBM";
                        break;
                    case "CNSNH":
                        link = "https://www.surveymonkey.com/r/KQWLBZZ";
                        break;
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
                    case "AR":
                        link = "https://www.surveymonkey.com/r/7FWBZ3T";
                        break;
                    case "JP":
                        link = "https://www.surveymonkey.com/r/DT5ZVRG";
                        break;
                    case "MR":
                        link = "https://www.surveymonkey.com/r/V3XX8ZZ";
                        break;
                    case "CNSVH":
                        link = "https://www.surveymonkey.com/r/KJNT8V6";
                        break;
                    case "CNSNH":
                        link = "https://www.surveymonkey.com/r/KQ2YSSR";
                        break;
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
