using SendEmailCSI.Common;
using SendEmailCSI.DA.InterSystems;
using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SendEmailCSI
{
    public partial class Form1 : Form
    {

        List<SendMail> patientsSendMail = new List<SendMail>();
        List<SendMail> sendMailToPatients = new List<SendMail>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProcessData();
        }

        private void ProcessData()
        {
            //date yesterday
            //string dateString = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            //date today
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                /** 
                 <summary>get data from cache by procedure</summary>
                 */
                var dtPatients = InterSystemsDA.GetDTSendEmailCSIData(dateString, dateString, Constants.Server108DataTest);

                //connvert datatable to object model
                var patientsToModel = dtPatients.DataTableToList<Patient>();

                //get data sendmail by current month from sql server
                var sendMailsSql = DataAccess.GetDataByMonth(DateTime.Now.Month.ToString());

                //check data dif by hn between cache and sqlserver for send mail
                patientsSendMail = Helper.GetDataSendMailList(patientsToModel, sendMailsSql);

                if (patientsSendMail.Count > 0)
                {
                    //patientsSendMail to datatable
                    var dt = Helper.SendMailDataGridView(patientsSendMail);
                    dataGridViewSendMail.DataSource = dt;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            SendMailToPatients(patientsSendMail);
            SaveSendMailListToDb(sendMailToPatients);
            if(sendMailToPatients.Count > 0)
            {
                var dt = Helper.SendMailDataGridView(sendMailToPatients);
                
                dataGridViewSendMail.DataSource = dt;
                btnSendMail.Enabled = false;

                MessageBox.Show("Send Complete");
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void SendMailToPatients(List<SendMail> sendMailList)
        {
            if (sendMailList.Count > 0)
            {
                //send mail survey to patient
                sendMailToPatients = MailSender.SendMailListResult(sendMailList);
            }
        }

        private void SaveSendMailListToDb(List<SendMail> sendMailList)
        {
            //save to database
            if (sendMailList.Count > 0)
            {
                DataAccess.SendMailListToDb(sendMailList);
            }
        }
    }
}
