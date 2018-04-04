namespace General_Assessment_Analyzer.Forms
{
    partial class frmCOEReports
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tbTerm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLoadResults = new System.Windows.Forms.Button();
            this.btn_SaveReport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(681, 17);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(107, 23);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate SQL ";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Run the SQL below and Load the Resulting File";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(13, 60);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(775, 204);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // tbTerm
            // 
            this.tbTerm.Location = new System.Drawing.Point(403, 19);
            this.tbTerm.Name = "tbTerm";
            this.tbTerm.Size = new System.Drawing.Size(100, 20);
            this.tbTerm.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter the Banner Term code for the Report you wish to Generate (E.G.: 201830):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(250, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Save the Results as a CSV and Press Load Results";
            // 
            // btnLoadResults
            // 
            this.btnLoadResults.Location = new System.Drawing.Point(681, 266);
            this.btnLoadResults.Name = "btnLoadResults";
            this.btnLoadResults.Size = new System.Drawing.Size(107, 23);
            this.btnLoadResults.TabIndex = 6;
            this.btnLoadResults.Text = "Load Results";
            this.btnLoadResults.UseVisualStyleBackColor = true;
            this.btnLoadResults.Click += new System.EventHandler(this.btnLoadResults_Click);
            // 
            // btn_SaveReport
            // 
            this.btn_SaveReport.Location = new System.Drawing.Point(681, 296);
            this.btn_SaveReport.Name = "btn_SaveReport";
            this.btn_SaveReport.Size = new System.Drawing.Size(107, 23);
            this.btn_SaveReport.TabIndex = 7;
            this.btn_SaveReport.Text = "Produce Report";
            this.btn_SaveReport.UseVisualStyleBackColor = true;
            this.btn_SaveReport.Click += new System.EventHandler(this.btn_SaveReport_Click);
            // 
            // frmCOEReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 327);
            this.Controls.Add(this.btn_SaveReport);
            this.Controls.Add(this.btnLoadResults);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbTerm);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Name = "frmCOEReports";
            this.Text = "College of Education Assessment Report";
            this.Load += new System.EventHandler(this.frmCOEReports_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox tbTerm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoadResults;
        private System.Windows.Forms.Button btn_SaveReport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}