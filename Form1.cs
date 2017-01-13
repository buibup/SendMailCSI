using SendEmailCSI.Common;
using SendEmailCSI.DA.InterSystems;
using SendEmailCSI.Models;
using System;
using System.Windows.Forms;

namespace SendEmailCSI
{
    public partial class Form1 : Form
    {
        
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // set date for get data
            string dateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            try
            {
                /** 
                 <summary>get data from cache by procedure</summary>
                 */
                var dt = InterSystemsDA.GetDTSendEmailCSIData(dateString, dateString, Constants.commandCache112);
                var patientsCache = dt.DataTableToList<Patient>();

                //get data sendmail by current month
                var sendmails = DataAccess.GetDataByMonth(DateTime.Now.Month.ToString());

                //check data for send mail
                var patientSendMail = Helper.GetDataSendMailList(patientsCache, sendmails);

                //send survey to patient
                var sendMailListToDb = MailSender.SendMailListResult(patientSendMail);

                //save to database
                if(sendMailListToDb.Count > 0)
                {
                    DataAccess.SendMailListToDb(sendMailListToDb);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
    }
}
