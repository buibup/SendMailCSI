using SendEmailCSI.Common;
using SendEmailCSI.Context;
using SendEmailCSI.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SendEmailCSI
{
    public partial class AddLanguageFrm : Form
    {

        SendMailContext sendMailContext = new SendMailContext();
        public AddLanguageFrm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddLanguageFrm_Load(object sender, EventArgs e)
        {
            GetLanguageListToListBox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            sendMailContext.Languages.Add(new Languages() { Code = txtCode.Text.Trim(), Description = txtDesc.Text.Trim() });
            sendMailContext.SaveChanges();
            GetLanguageListToListBox();
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            txtCode.Text = "";
            txtDesc.Text = "";
        }

        private void GetLanguageListToListBox()
        {
            listboxLanguage.DisplayMember = "Description";
            listboxLanguage.ValueMember = "Code";

            listboxLanguage.DataSource = DataAccess.GetLanguageList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using(var ctx = new SendMailContext())
            {
                Languages lang = (from l in ctx.Languages
                                  where l.Code == listboxLanguage.SelectedValue.ToString()
                                  select l).FirstOrDefault<Languages>();

                SettingMail sMail = lang.SettingMail;
                if(sMail !=null)
                {
                    ctx.SettingMails.Remove(sMail);
                    ctx.Languages.Remove(lang);
                }
                else
                {
                    ctx.Languages.Remove(lang);
                }
                
                ctx.SaveChanges();

                GetLanguageListToListBox();
            }
        }
    }
}
