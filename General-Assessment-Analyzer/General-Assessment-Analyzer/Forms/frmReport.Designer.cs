namespace General_Assessment_Analyzer.Forms
{
    partial class frmReport
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_StudentData = new System.Windows.Forms.Label();
            this.lb_CourseData = new System.Windows.Forms.Label();
            this.lb_AssessmentData = new System.Windows.Forms.Label();
            this.btn_LoadStudentFile = new System.Windows.Forms.Button();
            this.btn_LoadCourseFile = new System.Windows.Forms.Button();
            this.btn_LoadAssessmentFile = new System.Windows.Forms.Button();
            this.btnCombine = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCombine);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lb_StudentData);
            this.groupBox1.Controls.Add(this.lb_CourseData);
            this.groupBox1.Controls.Add(this.lb_AssessmentData);
            this.groupBox1.Controls.Add(this.btn_LoadStudentFile);
            this.groupBox1.Controls.Add(this.btn_LoadCourseFile);
            this.groupBox1.Controls.Add(this.btn_LoadAssessmentFile);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Data";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(212, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Assessment Catalog Loaded from Computer";
            // 
            // lb_StudentData
            // 
            this.lb_StudentData.AutoSize = true;
            this.lb_StudentData.Location = new System.Drawing.Point(149, 83);
            this.lb_StudentData.Name = "lb_StudentData";
            this.lb_StudentData.Size = new System.Drawing.Size(33, 13);
            this.lb_StudentData.TabIndex = 5;
            this.lb_StudentData.Text = "Done";
            // 
            // lb_CourseData
            // 
            this.lb_CourseData.AutoSize = true;
            this.lb_CourseData.Location = new System.Drawing.Point(149, 54);
            this.lb_CourseData.Name = "lb_CourseData";
            this.lb_CourseData.Size = new System.Drawing.Size(33, 13);
            this.lb_CourseData.TabIndex = 4;
            this.lb_CourseData.Text = "Done";
            // 
            // lb_AssessmentData
            // 
            this.lb_AssessmentData.AutoSize = true;
            this.lb_AssessmentData.Location = new System.Drawing.Point(149, 25);
            this.lb_AssessmentData.Name = "lb_AssessmentData";
            this.lb_AssessmentData.Size = new System.Drawing.Size(33, 13);
            this.lb_AssessmentData.TabIndex = 3;
            this.lb_AssessmentData.Text = "Done";
            // 
            // btn_LoadStudentFile
            // 
            this.btn_LoadStudentFile.Location = new System.Drawing.Point(7, 78);
            this.btn_LoadStudentFile.Name = "btn_LoadStudentFile";
            this.btn_LoadStudentFile.Size = new System.Drawing.Size(136, 23);
            this.btn_LoadStudentFile.TabIndex = 2;
            this.btn_LoadStudentFile.Text = "Load Student Data";
            this.btn_LoadStudentFile.UseVisualStyleBackColor = true;
            this.btn_LoadStudentFile.Click += new System.EventHandler(this.btn_LoadStudentFile_Click);
            // 
            // btn_LoadCourseFile
            // 
            this.btn_LoadCourseFile.Location = new System.Drawing.Point(7, 49);
            this.btn_LoadCourseFile.Name = "btn_LoadCourseFile";
            this.btn_LoadCourseFile.Size = new System.Drawing.Size(136, 23);
            this.btn_LoadCourseFile.TabIndex = 1;
            this.btn_LoadCourseFile.Text = "Load Course Data";
            this.btn_LoadCourseFile.UseVisualStyleBackColor = true;
            this.btn_LoadCourseFile.Click += new System.EventHandler(this.btn_LoadCourseFile_Click);
            // 
            // btn_LoadAssessmentFile
            // 
            this.btn_LoadAssessmentFile.Location = new System.Drawing.Point(7, 20);
            this.btn_LoadAssessmentFile.Name = "btn_LoadAssessmentFile";
            this.btn_LoadAssessmentFile.Size = new System.Drawing.Size(136, 23);
            this.btn_LoadAssessmentFile.TabIndex = 0;
            this.btn_LoadAssessmentFile.Text = "Load Assessment Data";
            this.btn_LoadAssessmentFile.UseVisualStyleBackColor = true;
            this.btn_LoadAssessmentFile.Click += new System.EventHandler(this.btn_LoadAssessmentFile_Click);
            // 
            // btnCombine
            // 
            this.btnCombine.Location = new System.Drawing.Point(7, 141);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(211, 23);
            this.btnCombine.TabIndex = 7;
            this.btnCombine.Text = "Combine Dataset";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 603);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReport";
            this.Text = "Produce Assessment Reports";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_StudentData;
        private System.Windows.Forms.Label lb_CourseData;
        private System.Windows.Forms.Label lb_AssessmentData;
        private System.Windows.Forms.Button btn_LoadStudentFile;
        private System.Windows.Forms.Button btn_LoadCourseFile;
        private System.Windows.Forms.Button btn_LoadAssessmentFile;
        private System.Windows.Forms.Button btnCombine;
    }
}