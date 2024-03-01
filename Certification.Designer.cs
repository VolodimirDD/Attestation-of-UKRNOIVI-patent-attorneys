
namespace Атестація
{
    partial class Certification
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
            this.button1 = new System.Windows.Forms.Button();
            this.questionLabel = new System.Windows.Forms.Label();
            this.answersListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.fullName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(582, 390);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 68);
            this.button1.TabIndex = 0;
            this.button1.Text = "Відповісти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // questionLabel
            // 
            this.questionLabel.AutoSize = true;
            this.questionLabel.Location = new System.Drawing.Point(110, 137);
            this.questionLabel.Name = "questionLabel";
            this.questionLabel.Size = new System.Drawing.Size(35, 13);
            this.questionLabel.TabIndex = 1;
            this.questionLabel.Text = "label1";
            // 
            // answersListBox
            // 
            this.answersListBox.FormattingEnabled = true;
            this.answersListBox.Location = new System.Drawing.Point(408, 222);
            this.answersListBox.Name = "answersListBox";
            this.answersListBox.Size = new System.Drawing.Size(444, 147);
            this.answersListBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Питання :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(404, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "Відповіді";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(444, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 22);
            this.label3.TabIndex = 5;
            this.label3.Text = "Вікно тестування";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(404, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Час тестування :";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold);
            this.labelTime.Location = new System.Drawing.Point(554, 34);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(22, 22);
            this.labelTime.TabIndex = 7;
            this.labelTime.Text = "...";
            // 
            // fullName
            // 
            this.fullName.Location = new System.Drawing.Point(737, 12);
            this.fullName.Name = "fullName";
            this.fullName.Size = new System.Drawing.Size(172, 20);
            this.fullName.TabIndex = 8;
            // 
            // Certification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 593);
            this.Controls.Add(this.fullName);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.answersListBox);
            this.Controls.Add(this.questionLabel);
            this.Controls.Add(this.button1);
            this.Name = "Certification";
            this.Text = "Тестування";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Certification_FormClosing);
            this.Load += new System.EventHandler(this.Certification_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label questionLabel;
        private System.Windows.Forms.ListBox answersListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.TextBox fullName;
    }
}