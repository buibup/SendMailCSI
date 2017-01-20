using SendEmailCSI.Common;
using SendEmailCSI.DA.InterSystems;
using SendEmailCSI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

namespace SendEmailCSI
{
    public partial class Form1 : Form
    {
        #region Set variable
        DataTable dt = new DataTable();
        List<SendMail> patientsSendMail = new List<SendMail>();
        List<SendMail> sendMailToPatients = new List<SendMail>();
        #endregion

        #region InitializeComponent
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region FormLoad
        private void Form1_Load(object sender, EventArgs e)
        {
            // Set datenow add day -1 to dateimepicker
            dateTimePickerFrom.Value = DateTime.Now.AddDays(-1);
            dateTimePickerTo.Value = DateTime.Now.AddDays(-1);

            // Close send mail button
            btnSendMail.Enabled = false;

            // Load and show data to datagridview
            ProgressLoadDataDialog();

            btnSendMail.Enabled = dt.Rows.Count > 0 ? btnSendMail.Enabled = true : btnSendMail.Enabled = false;
        }
        #endregion

        #region Process data send mail from cache to sendmail object 
        private void ProgressLoadDataDialog()
        {
            // Initialize the dialog that will contain the progress bar
            ProgressDialog progressDialog = new ProgressDialog();

            // Initialize the thread that will handle the background process
            Thread backgroundThread = new Thread(
                new ThreadStart(() =>
                {

                    Thread.Sleep(50);

                    // get data sendmail to datable
                    dt = DataProcess();

                    if (dataGridViewSendMail.InvokeRequired)
                        dataGridViewSendMail.BeginInvoke(new Action(() => dataGridViewSendMail.DataSource = dt));

                    if (progressDialog.InvokeRequired)
                        progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

                }
            ));

            // Start the background process thread
            backgroundThread.Start();

            // Open the dialog
            progressDialog.ShowDialog();
        }
        #endregion

        #region progress for sending mail
        private void ProgressSendMailDialog()
        {

            // Initialize the dialog that will contain the progress bar
            ProgressDialog progressDialog = new ProgressDialog();
            //progressDialog.Message = "Sending E-Mail...";

            // Initialize the thread that will handle the background process
            Thread backgroundThread = new Thread(
                new ThreadStart(() =>
                {

                    dt = SendMailProcess();

                    if (dataGridViewSendMail.InvokeRequired)
                        dataGridViewSendMail.BeginInvoke(new Action(() => dataGridViewSendMail.DataSource = dt));

                    // Close the dialog if it hasn't been already
                    if (progressDialog.InvokeRequired)
                        progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

                }
            ));

            // Start the background process thread
            backgroundThread.Start();

            // Open the dialog
            progressDialog.ShowDialog();
        }
        #endregion

        #region Load patient data sendmail from store procedure to data table 
        private DataTable DataProcess()
        {
            DataTable dt = new DataTable();
            string dateStringFrom = dateTimePickerFrom.Value.ToString("yyyy-MM-dd");
            string dateStringTo = dateTimePickerTo.Value.ToString("yyyy-MM-dd");
            try
            {
                /** 
                 <summary>get data from cache by procedure</summary>
                 */
                var dtPatients = InterSystemsDA.GetDTSendEmailCSIData(dateStringFrom, dateStringTo, Constants.Server108DataTest);

                //connvert datatable to paient model
                var patientsToModel = dtPatients.DataTableToList<Patient>();

                //get data sendmail by current month from sql server by entity framework
                var sendMailsSql = DataAccess.GetDataByMonth(DateTime.Now.Month.ToString());

                //check data different by hn between cache database and sql server database for send mail
                patientsSendMail = Helper.GetDataSendMailList(patientsToModel, sendMailsSql);

                if (patientsSendMail.Count > 0)
                {
                    // Convert sendMail model to datatable
                    dt = Helper.SendMailDataGridView(patientsSendMail);
                }

                return dt;
            }
            catch (Exception )
            {
                return null;
                //MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Sendmail button click event
        private void btnSendMail_Click(object sender, EventArgs e)
        {
            ProgressSendMailDialog();
            MessageBox.Show("Send completed.");
        }
        #endregion

        #region  Send mail to data table
        private DataTable SendMailProcess()
        {
            DataTable dt = new DataTable();
            try
            {
                SendMailToPatients(patientsSendMail);
                SaveSendMailListToDb(sendMailToPatients);
                if (sendMailToPatients.Count > 0)
                {
                    dt = Helper.SendMailDataGridView(sendMailToPatients);
                }

                return dt;
            }
            catch (Exception)
            {

                return dt;
            }
        }
        #endregion

        #region Settings
        private void btnSetting_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
        #endregion

        #region Send mail to all patient
        private void SendMailToPatients(List<SendMail> sendMailList)
        {
            if (sendMailList.Count > 0)
            {
                //send mail survey to patient
                sendMailToPatients = MailSender.SendMailListResult(sendMailList);
            }
        }
        #endregion

        #region Save sendmail object to sql server
        private void SaveSendMailListToDb(List<SendMail> sendMailList)
        {
            //save to database
            if (sendMailList.Count > 0)
            {
                DataAccess.SendMailListToDb(sendMailList);
            }
        }
        #endregion

        #region Load data button click
        private void btnLoadData_Click(object sender, EventArgs e)
        {

            btnLoadData.Enabled = false;

            // Load data
            ProgressLoadDataDialog();

            btnLoadData.Enabled = true;

            if (patientsSendMail.Count > 0)
            {
                btnSendMail.Enabled = true;
            }
            else
            {
                btnSendMail.Enabled = false;
            }
        }
        #endregion
    }
}
