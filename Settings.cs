using SendEmailCSI.Common;
using SendEmailCSI.Context;
using SendEmailCSI.Models;
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

        List<SettingMail> settingMailList = new List<SettingMail>();

        public Settings()
        {
            InitializeComponent();
        }


        private void SaveDataToAppconfig()
        {
            using (var form = new Settings())
            {
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                appPath = appPath.Replace("\\bin\\Release", "");
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
            LoadLanguagesToCombobox();
            settingMailList = DataAccess.GetSettingMailList();
        }

        private void LoadLanguagesToCombobox()
        {
            if (DataAccess.GetLanguageList().Count == 0)
                cbbLanguages.Text = "";

            cbbLanguages.DataSource = DataAccess.GetLanguageList();
            cbbLanguages.DisplayMember = "Description";
            cbbLanguages.ValueMember = "Code";
            cbbLanguages.Invalidate();
            
        }

        private void btnAddLanguage_Click(object sender, EventArgs e)
        {
            AddLanguageFrm frm = new AddLanguageFrm();
            frm.ShowDialog();
            LoadLanguagesToCombobox();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbbLanguages.SelectedIndex > -1)
            {
                foreach (var lang in DataAccess.GetLanguageList())
                {
                    if (lang.Code == cbbLanguages.SelectedValue.ToString())
                    {
                        SettingMail settingMail = new SettingMail
                        {
                            LanguagesId = lang.LanguagesId,
                            MailFrom = txtFrom.Text.Trim(),
                            Subject = txtSubject.Text.Trim(),
                            MailBody = txtMessage.Text,
                            SmtpClient = txtSmtp.Text.Trim()
                        };

                        using (var ctx = new SendMailContext())
                        {
                            ctx.SettingMails.Add(settingMail);
                            ctx.SaveChanges();
                            MessageBox.Show("Save completed");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add languages first");
            }

        }

        private void cbbLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            SettingMail settingMail = new SettingMail();
            Languages lang = cbbLanguages.SelectedValue as Languages;

            if(lang != null)
            {
                settingMail = DataAccess.GetSettingMail(lang.LanguagesId);
            }else
            {
                settingMail = GetSettingMail(cbbLanguages.SelectedValue.ToString());
            }
            
            if(settingMail != null)
            {
                txtFrom.Text = settingMail.MailFrom;
                txtMessage.Text = settingMail.MailBody;
                txtSubject.Text = settingMail.Subject;
                txtSmtp.Text = settingMail.SmtpClient;
            }else
            {
                ClearTextBoxControls();
            }
        }

        private void ClearTextBoxControls()
        {
            txtFrom.Text = "";
            txtMessage.Text = "";
            txtSubject.Text = "";
            txtSmtp.Text = "";
        }

        private SettingMail GetSettingMail(string codeLang)
        {

            int languagesId = DataAccess.GetLanguagesId(codeLang);

            var result = DataAccess.GetSettingMail(languagesId);

            return result;
        }
    }
}
