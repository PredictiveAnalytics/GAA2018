using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;
using General_Assessment_Analyzer.Classes;

namespace General_Assessment_Analyzer.Forms
{
    public partial class frmReport : Form
    {
        #region Class Variables
        public string _pathToCourseFile; 
        public string _pathToStudentFile;
        public string _pathToAssessmentFile;
        public List<AssessmentRow> _AssessmentRows;
        public List<BannerCourseRecord> _BannerCourseRecords;
        public List<BannerStudentRecord> _BannerStudentRecords;

        #endregion
        public frmReport()
        {
            InitializeComponent();

            // Configure our open file dialog by setting it's initial directory, and file filter.
           // openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog1.RestoreDirectory = true;
        }

        #region Form Methods

        private void frmReport_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region Controls
        private void btn_LoadAssessmentFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Assessment Data File";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _pathToAssessmentFile = openFileDialog1.FileName;
                bool success = LoadAssessmentFile(_pathToAssessmentFile);
                if (!success)
                {
                    MessageBox.Show(
                        "There was a problem reading the Assessment Data file.  Please check the documenation to check formatting.",
                        "Error Reading Assessment Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    //db.Items.Where(x=> x.userid == user_ID).Select(x=>x.Id).Distinct();
                    /*var test = _AssessmentRows.Where(x => x.Rubric_ID == "95904" & x.STUDENT_ID == "H00667089")
                        .Select(x => x.Rubric_Row_Header).ToString();
                    Debug.WriteLine(test);*/
                    foreach (AssessmentRow ar in _AssessmentRows)
                    {
                        if (ar.Rubric_ID=="95904" && ar.STUDENT_ID == "H00667089")
                        {
                            //Debug.WriteLine(ar.Rubric_Row_Header);
                            if (ar.Rubric_Row_Header.Contains((char)13))
                            {
                                Debug.WriteLine(ar.Rubric_Row_Header);
                            }
                        }
                    }
                }
            }
        }

        private void btn_LoadCourseFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Course Data File";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _pathToCourseFile = openFileDialog1.FileName;
                bool success = LoadCourseFile(_pathToCourseFile);
                if (!success)
                {
                    MessageBox.Show(
                        "There was a problem reading the Course Data file.  Please check the documenation to check formatting.",
                        "Error Reading Course Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btn_LoadStudentFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select Student Data File";
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                _pathToStudentFile = openFileDialog1.FileName;
                bool success = LoadStudentFile(_pathToStudentFile);
                if (!success)
                {
                    MessageBox.Show(
                        "There was a problem reading the Student Data file.  Please check the documenation to check formatting.",
                        "Error Reading Student Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region File Processing Methods

        private bool LoadAssessmentFile(string path)
        {
            bool return_value = false;

                var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var sr = new StreamReader(fs);
                var csv = new CsvHelper.CsvReader(sr);
                csv.Configuration.MissingFieldFound = null;
                csv.Configuration.BadDataFound = null;

                IEnumerable<AssessmentRow> records = csv.GetRecords<AssessmentRow>();
                _AssessmentRows = records.ToList();
                return_value = true;
                Debug.WriteLine("Assessment Data loaded: " + _AssessmentRows.Count);
            
            return return_value;
        }

        private bool LoadCourseFile(string path)
        {
            bool return_value = false;

            try
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var sr = new StreamReader(fs);
                var csv = new CsvHelper.CsvReader(sr);
                csv.Configuration.MissingFieldFound = null;

                IEnumerable<BannerCourseRecord> records = csv.GetRecords<BannerCourseRecord>();
                _BannerCourseRecords = records.ToList();
                return_value = true;
                Debug.WriteLine("Banner Course Records loaded: " + _BannerCourseRecords.Count);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Reading Course File " + path);
                return_value = false;
            }
            return return_value;
        }

        private bool LoadStudentFile(string path)
        {
            bool return_value = false;

            try
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var sr = new StreamReader(fs);
                var csv = new CsvHelper.CsvReader(sr);
                csv.Configuration.MissingFieldFound = null;

                IEnumerable<BannerStudentRecord> records = csv.GetRecords<BannerStudentRecord>();
                _BannerStudentRecords = records.ToList();
                return_value = true;
                Debug.WriteLine("Banner Student Records loaded: " + _BannerStudentRecords.Count);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Reading Student File " + path);
                return_value = false;
            }

            return return_value;
        }
        #endregion

        #region Workbook Methods
        #endregion

        #region Mathmatical Methods
        #endregion

        
    }
}
