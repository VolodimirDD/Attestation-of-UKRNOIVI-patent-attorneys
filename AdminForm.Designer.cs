
namespace Атестація
{
    partial class AdminForm
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
            this.Accounts = new System.Windows.Forms.Button();
            this.Questions = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Attestations = new System.Windows.Forms.Button();
            this.fullName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Accounts
            // 
            this.Accounts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Accounts.Location = new System.Drawing.Point(12, 164);
            this.Accounts.Name = "Accounts";
            this.Accounts.Size = new System.Drawing.Size(101, 56);
            this.Accounts.TabIndex = 0;
            this.Accounts.Text = "Облікові записи";
            this.Accounts.UseVisualStyleBackColor = true;
            this.Accounts.Click += new System.EventHandler(this.Accounts_Click);
            // 
            // Questions
            // 
            this.Questions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Questions.Location = new System.Drawing.Point(258, 164);
            this.Questions.Name = "Questions";
            this.Questions.Size = new System.Drawing.Size(101, 56);
            this.Questions.TabIndex = 1;
            this.Questions.Text = "Питання атестацій";
            this.Questions.UseVisualStyleBackColor = true;
            this.Questions.Click += new System.EventHandler(this.Questions_Click);
            // 
            // Time
            // 
            this.Time.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Time.Location = new System.Drawing.Point(376, 164);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(101, 56);
            this.Time.TabIndex = 2;
            this.Time.Text = "Час атестацій";
            this.Time.UseVisualStyleBackColor = true;
            this.Time.Click += new System.EventHandler(this.Time_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(118, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Оберіть предмет редагування";
            // 
            // Attestations
            // 
            this.Attestations.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Attestations.Location = new System.Drawing.Point(136, 164);
            this.Attestations.Name = "Attestations";
            this.Attestations.Size = new System.Drawing.Size(101, 56);
            this.Attestations.TabIndex = 4;
            this.Attestations.Text = "Атестації";
            this.Attestations.UseVisualStyleBackColor = true;
            this.Attestations.Click += new System.EventHandler(this.Attestations_Click);
            // 
            // fullName
            // 
            this.fullName.Location = new System.Drawing.Point(277, 12);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(200, 20);
            this.fullName.TabIndex = 13;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 304);
            this.Controls.Add(this.fullName);
            this.Controls.Add(this.Attestations);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.Questions);
            this.Controls.Add(this.Accounts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Адмін-панель";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminForm_FormClosing);
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Accounts;
        private System.Windows.Forms.Button Questions;
        private System.Windows.Forms.Button Time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Attestations;
        private System.Windows.Forms.TextBox fullName;
    }
}