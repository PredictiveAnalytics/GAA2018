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
            this.btnCombine = new System.Windows.Forms.Button();
            this.lbCatalog = new System.Windows.Forms.Label();
            this.lb_StudentData = new System.Windows.Forms.Label();
            this.lb_CourseData = new System.Windows.Forms.Label();
            this.lb_AssessmentData = new System.Windows.Forms.Label();
            this.btn_LoadStudentFile = new System.Windows.Forms.Button();
            this.btn_LoadCourseFile = new System.Windows.Forms.Button();
            this.btn_LoadAssessmentFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_FilterSubjects = new System.Windows.Forms.Button();
            this.clb_Subjects = new System.Windows.Forms.CheckedListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_FilterAssessments = new System.Windows.Forms.Button();
            this.clb_AssessmentTypes = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tvCourses = new System.Windows.Forms.TreeView();
            this.btn_AddSelectedCourses = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btn_LockScale = new System.Windows.Forms.Button();
            this.dgvScale = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btn_FilterRates = new System.Windows.Forms.Button();
            this.clb_Rates = new System.Windows.Forms.CheckedListBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btn_FilterMajors = new System.Windows.Forms.Button();
            this.clb_Majors = new System.Windows.Forms.CheckedListBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btn_SaveWorkbook = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvScale)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCombine);
            this.groupBox1.Controls.Add(this.lbCatalog);
            this.groupBox1.Controls.Add(this.lb_StudentData);
            this.groupBox1.Controls.Add(this.lb_CourseData);
            this.groupBox1.Controls.Add(this.lb_AssessmentData);
            this.groupBox1.Controls.Add(this.btn_LoadStudentFile);
            this.groupBox1.Controls.Add(this.btn_LoadCourseFile);
            this.groupBox1.Controls.Add(this.btn_LoadAssessmentFile);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(245, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load Data";
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
            // lbCatalog
            // 
            this.lbCatalog.AutoSize = true;
            this.lbCatalog.Location = new System.Drawing.Point(6, 124);
            this.lbCatalog.Name = "lbCatalog";
            this.lbCatalog.Size = new System.Drawing.Size(212, 13);
            this.lbCatalog.TabIndex = 6;
            this.lbCatalog.Text = "Assessment Catalog Loaded from Computer";
            this.lbCatalog.Visible = false;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_FilterSubjects);
            this.groupBox2.Controls.Add(this.clb_Subjects);
            this.groupBox2.Location = new System.Drawing.Point(13, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(245, 163);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter - Step 1 - Subjects";
            // 
            // btn_FilterSubjects
            // 
            this.btn_FilterSubjects.Location = new System.Drawing.Point(128, 20);
            this.btn_FilterSubjects.Name = "btn_FilterSubjects";
            this.btn_FilterSubjects.Size = new System.Drawing.Size(111, 23);
            this.btn_FilterSubjects.TabIndex = 1;
            this.btn_FilterSubjects.Text = "Filter Subjects";
            this.btn_FilterSubjects.UseVisualStyleBackColor = true;
            this.btn_FilterSubjects.Click += new System.EventHandler(this.btn_FilterSubjects_Click);
            // 
            // clb_Subjects
            // 
            this.clb_Subjects.FormattingEnabled = true;
            this.clb_Subjects.Location = new System.Drawing.Point(7, 20);
            this.clb_Subjects.Name = "clb_Subjects";
            this.clb_Subjects.Size = new System.Drawing.Size(114, 124);
            this.clb_Subjects.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_FilterAssessments);
            this.groupBox3.Controls.Add(this.clb_AssessmentTypes);
            this.groupBox3.Location = new System.Drawing.Point(13, 353);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 163);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter - Step 2 - Select Assessment Type";
            // 
            // btn_FilterAssessments
            // 
            this.btn_FilterAssessments.Location = new System.Drawing.Point(130, 19);
            this.btn_FilterAssessments.Name = "btn_FilterAssessments";
            this.btn_FilterAssessments.Size = new System.Drawing.Size(109, 23);
            this.btn_FilterAssessments.TabIndex = 3;
            this.btn_FilterAssessments.Text = "Filter Assessments";
            this.btn_FilterAssessments.UseVisualStyleBackColor = true;
            this.btn_FilterAssessments.Click += new System.EventHandler(this.btn_FilterAssessments_Click);
            // 
            // clb_AssessmentTypes
            // 
            this.clb_AssessmentTypes.FormattingEnabled = true;
            this.clb_AssessmentTypes.Location = new System.Drawing.Point(9, 19);
            this.clb_AssessmentTypes.Name = "clb_AssessmentTypes";
            this.clb_AssessmentTypes.Size = new System.Drawing.Size(114, 124);
            this.clb_AssessmentTypes.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tvCourses);
            this.groupBox4.Controls.Add(this.btn_AddSelectedCourses);
            this.groupBox4.Controls.Add(this.btn_SelectAll);
            this.groupBox4.Location = new System.Drawing.Point(264, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(413, 170);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Courses";
            // 
            // tvCourses
            // 
            this.tvCourses.CheckBoxes = true;
            this.tvCourses.Location = new System.Drawing.Point(7, 20);
            this.tvCourses.Name = "tvCourses";
            this.tvCourses.Size = new System.Drawing.Size(400, 117);
            this.tvCourses.TabIndex = 3;
            this.tvCourses.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvCourses_AfterCheck);
            // 
            // btn_AddSelectedCourses
            // 
            this.btn_AddSelectedCourses.Location = new System.Drawing.Point(94, 141);
            this.btn_AddSelectedCourses.Name = "btn_AddSelectedCourses";
            this.btn_AddSelectedCourses.Size = new System.Drawing.Size(81, 23);
            this.btn_AddSelectedCourses.TabIndex = 2;
            this.btn_AddSelectedCourses.Text = "Add Selected";
            this.btn_AddSelectedCourses.UseVisualStyleBackColor = true;
            this.btn_AddSelectedCourses.Click += new System.EventHandler(this.btn_AddSelectedCourses_Click);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Location = new System.Drawing.Point(7, 141);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(81, 23);
            this.btn_SelectAll.TabIndex = 1;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = true;
            this.btn_SelectAll.Click += new System.EventHandler(this.btn_SelectAll_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btn_LockScale);
            this.groupBox5.Controls.Add(this.dgvScale);
            this.groupBox5.Location = new System.Drawing.Point(264, 184);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(407, 163);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Configure Scale";
            // 
            // btn_LockScale
            // 
            this.btn_LockScale.Location = new System.Drawing.Point(290, 20);
            this.btn_LockScale.Name = "btn_LockScale";
            this.btn_LockScale.Size = new System.Drawing.Size(111, 23);
            this.btn_LockScale.TabIndex = 1;
            this.btn_LockScale.Text = "Lock Scale";
            this.btn_LockScale.UseVisualStyleBackColor = true;
            // 
            // dgvScale
            // 
            this.dgvScale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvScale.Location = new System.Drawing.Point(7, 20);
            this.dgvScale.Name = "dgvScale";
            this.dgvScale.RowHeadersVisible = false;
            this.dgvScale.Size = new System.Drawing.Size(277, 137);
            this.dgvScale.TabIndex = 0;
            this.dgvScale.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvScale_DataError);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btn_FilterRates);
            this.groupBox6.Controls.Add(this.clb_Rates);
            this.groupBox6.Location = new System.Drawing.Point(264, 353);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(407, 119);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Filter - Rate Codes - Uncheck to Exclude";
            // 
            // btn_FilterRates
            // 
            this.btn_FilterRates.Location = new System.Drawing.Point(291, 20);
            this.btn_FilterRates.Name = "btn_FilterRates";
            this.btn_FilterRates.Size = new System.Drawing.Size(110, 23);
            this.btn_FilterRates.TabIndex = 1;
            this.btn_FilterRates.Text = "Filter Rates";
            this.btn_FilterRates.UseVisualStyleBackColor = true;
            this.btn_FilterRates.Click += new System.EventHandler(this.btn_FilterRates_Click);
            // 
            // clb_Rates
            // 
            this.clb_Rates.FormattingEnabled = true;
            this.clb_Rates.Location = new System.Drawing.Point(7, 19);
            this.clb_Rates.Name = "clb_Rates";
            this.clb_Rates.Size = new System.Drawing.Size(277, 94);
            this.clb_Rates.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btn_FilterMajors);
            this.groupBox7.Controls.Add(this.clb_Majors);
            this.groupBox7.Location = new System.Drawing.Point(264, 478);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(407, 119);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Filter - Major Codes - Uncheck to Exclude";
            // 
            // btn_FilterMajors
            // 
            this.btn_FilterMajors.Location = new System.Drawing.Point(290, 19);
            this.btn_FilterMajors.Name = "btn_FilterMajors";
            this.btn_FilterMajors.Size = new System.Drawing.Size(110, 23);
            this.btn_FilterMajors.TabIndex = 2;
            this.btn_FilterMajors.Text = "Filter Majors";
            this.btn_FilterMajors.UseVisualStyleBackColor = true;
            this.btn_FilterMajors.Click += new System.EventHandler(this.btn_FilterMajors_Click);
            // 
            // clb_Majors
            // 
            this.clb_Majors.FormattingEnabled = true;
            this.clb_Majors.Location = new System.Drawing.Point(7, 19);
            this.clb_Majors.Name = "clb_Majors";
            this.clb_Majors.Size = new System.Drawing.Size(277, 94);
            this.clb_Majors.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btn_SaveWorkbook);
            this.groupBox8.Location = new System.Drawing.Point(684, 13);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(211, 54);
            this.groupBox8.TabIndex = 7;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Save Workbook";
            // 
            // btn_SaveWorkbook
            // 
            this.btn_SaveWorkbook.Location = new System.Drawing.Point(6, 20);
            this.btn_SaveWorkbook.Name = "btn_SaveWorkbook";
            this.btn_SaveWorkbook.Size = new System.Drawing.Size(198, 23);
            this.btn_SaveWorkbook.TabIndex = 0;
            this.btn_SaveWorkbook.Text = "Save Workbook";
            this.btn_SaveWorkbook.UseVisualStyleBackColor = true;
            this.btn_SaveWorkbook.Click += new System.EventHandler(this.btn_SaveWorkbook_Click);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 603);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmReport";
            this.Text = "Produce Assessment Reports";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvScale)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbCatalog;
        private System.Windows.Forms.Label lb_StudentData;
        private System.Windows.Forms.Label lb_CourseData;
        private System.Windows.Forms.Label lb_AssessmentData;
        private System.Windows.Forms.Button btn_LoadStudentFile;
        private System.Windows.Forms.Button btn_LoadCourseFile;
        private System.Windows.Forms.Button btn_LoadAssessmentFile;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_FilterSubjects;
        private System.Windows.Forms.CheckedListBox clb_Subjects;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_FilterAssessments;
        private System.Windows.Forms.CheckedListBox clb_AssessmentTypes;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_AddSelectedCourses;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.TreeView tvCourses;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btn_LockScale;
        private System.Windows.Forms.DataGridView dgvScale;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btn_FilterRates;
        private System.Windows.Forms.CheckedListBox clb_Rates;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btn_FilterMajors;
        private System.Windows.Forms.CheckedListBox clb_Majors;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btn_SaveWorkbook;
    }
}