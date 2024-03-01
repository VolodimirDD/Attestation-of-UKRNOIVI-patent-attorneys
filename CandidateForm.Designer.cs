
namespace Атестація
{
    partial class CandidateForm
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
            this.attestations = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChoise = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fullName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // attestations
            // 
            this.attestations.FormattingEnabled = true;
            this.attestations.Location = new System.Drawing.Point(179, 113);
            this.attestations.Name = "attestations";
            this.attestations.Size = new System.Drawing.Size(121, 21);
            this.attestations.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Атестація";
            // 
            // btnChoise
            // 
            this.btnChoise.Location = new System.Drawing.Point(163, 198);
            this.btnChoise.Name = "btnChoise";
            this.btnChoise.Size = new System.Drawing.Size(101, 56);
            this.btnChoise.TabIndex = 3;
            this.btnChoise.Text = "Вибрати";
            this.btnChoise.UseVisualStyleBackColor = true;
            this.btnChoise.Click += new System.EventHandler(this.btnChoise_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(142, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Вибір атестації";
            // 
            // fullName
            // 
            this.fullName.Location = new System.Drawing.Point(198, 12);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(200, 20);
            this.fullName.TabIndex = 14;
            // 
            // CandidateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 266);
            this.Controls.Add(this.fullName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChoise);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.attestations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "CandidateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Атестації";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CandidateForm_FormClosing);
            this.Load += new System.EventHandler(this.CandidateForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox attestations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChoise;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fullName;
    }
}