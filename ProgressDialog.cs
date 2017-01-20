using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SendEmailCSI
{
    public partial class ProgressDialog : Form
    {
        public ProgressDialog()
        {
            InitializeComponent();
        }

        public string Message
        {
            set { lblMessage.Text = value; }
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
