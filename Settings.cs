using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendEmailCSI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            using (var form = new Settings())
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                appPath = appPath.Replace("\\bin\\Debug", "");
                string configFile = Path.Combine(appPath, "App.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                config.AppSettings.Settings["subject"].Value = txtSubject.Text;
                config.AppSettings.Settings["MailAddress"].Value = txtFrom.Text;
                config.AppSettings.Settings["SmtpClient"].Value = txtSmtp.Text;
                config.AppSettings.Settings["Message"].Value = txtMessage.Text;
                config.Save();

                this.Close();
            };
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtFrom.Text = ConfigurationManager.AppSettings["MailAddress"];
            txtSubject.Text = ConfigurationManager.AppSettings["subject"];
            txtSmtp.Text = ConfigurationManager.AppSettings["SmtpClient"];
            txtMessage.Text = ConfigurationManager.AppSettings["Message"];
        }

        
    }
}
