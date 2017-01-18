namespace SendEmailCSI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewSendMail = new System.Windows.Forms.DataGridView();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSendMail)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewSendMail
            // 
            this.dataGridViewSendMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSendMail.Location = new System.Drawing.Point(13, 13);
            this.dataGridViewSendMail.Name = "dataGridViewSendMail";
            this.dataGridViewSendMail.Size = new System.Drawing.Size(991, 429);
            this.dataGridViewSendMail.TabIndex = 0;
            // 
            // btnSendMail
            // 
            this.btnSendMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSendMail.Location = new System.Drawing.Point(916, 449);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(87, 38);
            this.btnSendMail.TabIndex = 1;
            this.btnSendMail.Text = "Send Mail";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSetting.Location = new System.Drawing.Point(12, 449);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(87, 38);
            this.btnSetting.TabIndex = 1;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 499);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnSendMail);
            this.Controls.Add(this.dataGridViewSendMail);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSendMail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSendMail;
        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.Button btnSetting;
    }
}

