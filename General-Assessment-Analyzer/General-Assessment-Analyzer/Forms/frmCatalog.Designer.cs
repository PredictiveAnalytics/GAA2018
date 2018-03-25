namespace General_Assessment_Analyzer.Forms
{
    partial class frmCatalog
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
            this.lbEntries = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_DeleteCourse = new System.Windows.Forms.Button();
            this.btn_AddCourse = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_AddAssessment = new System.Windows.Forms.Button();
            this.btn_DeleteAssessment = new System.Windows.Forms.Button();
            this.lbAssessments = new System.Windows.Forms.ListBox();
            this.Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbEntries
            // 
            this.lbEntries.FormattingEnabled = true;
            this.lbEntries.Location = new System.Drawing.Point(6, 19);
            this.lbEntries.Name = "lbEntries";
            this.lbEntries.Size = new System.Drawing.Size(125, 329);
            this.lbEntries.TabIndex = 0;
            this.lbEntries.SelectedIndexChanged += new System.EventHandler(this.lbEntries_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_DeleteCourse);
            this.groupBox1.Controls.Add(this.btn_AddCourse);
            this.groupBox1.Controls.Add(this.lbEntries);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 414);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Courses in Catalog";
            // 
            // btn_DeleteCourse
            // 
            this.btn_DeleteCourse.Location = new System.Drawing.Point(7, 383);
            this.btn_DeleteCourse.Name = "btn_DeleteCourse";
            this.btn_DeleteCourse.Size = new System.Drawing.Size(124, 23);
            this.btn_DeleteCourse.TabIndex = 2;
            this.btn_DeleteCourse.Text = "Delete Selected";
            this.btn_DeleteCourse.UseVisualStyleBackColor = true;
            this.btn_DeleteCourse.Click += new System.EventHandler(this.btn_DeleteCourse_Click);
            // 
            // btn_AddCourse
            // 
            this.btn_AddCourse.Location = new System.Drawing.Point(7, 354);
            this.btn_AddCourse.Name = "btn_AddCourse";
            this.btn_AddCourse.Size = new System.Drawing.Size(124, 23);
            this.btn_AddCourse.TabIndex = 1;
            this.btn_AddCourse.Text = "Add New";
            this.btn_AddCourse.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_AddAssessment);
            this.groupBox2.Controls.Add(this.btn_DeleteAssessment);
            this.groupBox2.Controls.Add(this.lbAssessments);
            this.groupBox2.Location = new System.Drawing.Point(157, 32);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 329);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Assessments in Course";
            // 
            // btn_AddAssessment
            // 
            this.btn_AddAssessment.Location = new System.Drawing.Point(7, 300);
            this.btn_AddAssessment.Name = "btn_AddAssessment";
            this.btn_AddAssessment.Size = new System.Drawing.Size(172, 23);
            this.btn_AddAssessment.TabIndex = 2;
            this.btn_AddAssessment.Text = "Add New";
            this.btn_AddAssessment.UseVisualStyleBackColor = true;
            this.btn_AddAssessment.Click += new System.EventHandler(this.btn_AddAssessment_Click);
            // 
            // btn_DeleteAssessment
            // 
            this.btn_DeleteAssessment.Location = new System.Drawing.Point(6, 271);
            this.btn_DeleteAssessment.Name = "btn_DeleteAssessment";
            this.btn_DeleteAssessment.Size = new System.Drawing.Size(172, 23);
            this.btn_DeleteAssessment.TabIndex = 1;
            this.btn_DeleteAssessment.Text = "Delete Selected";
            this.btn_DeleteAssessment.UseVisualStyleBackColor = true;
            this.btn_DeleteAssessment.Click += new System.EventHandler(this.btn_DeleteAssessment_Click);
            // 
            // lbAssessments
            // 
            this.lbAssessments.FormattingEnabled = true;
            this.lbAssessments.Location = new System.Drawing.Point(7, 20);
            this.lbAssessments.Name = "lbAssessments";
            this.lbAssessments.Size = new System.Drawing.Size(172, 238);
            this.lbAssessments.TabIndex = 0;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(272, 399);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 3;
            this.Save.Text = "btn_Save";
            this.Save.UseVisualStyleBackColor = true;
            // 
            // frmCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 434);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCatalog";
            this.Text = "Manage Assessment and Course Catalogs";
            this.Load += new System.EventHandler(this.frmCatalog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbEntries;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_DeleteCourse;
        private System.Windows.Forms.Button btn_AddCourse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_AddAssessment;
        private System.Windows.Forms.Button btn_DeleteAssessment;
        private System.Windows.Forms.ListBox lbAssessments;
        private System.Windows.Forms.Button Save;
    }
}