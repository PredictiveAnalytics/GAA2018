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
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml.Wordprocessing;
using AutoMapper;
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
        public List<AssessmentCourseRecord> _MatchedCourseRecords;
        public List<BannerCourseRecord> _BannerCourseRecords;
        public List<BannerStudentRecord> _BannerStudentRecords;

        public List<string> _selectedAssessments; 
        #endregion
        public frmReport()
        {
            InitializeComponent();

            // Configure our open file dialog by setting it's initial directory, and file filter.
           // openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog1.RestoreDirectory = true;

            //Hide the "Done" labels 
            lb_AssessmentData.Visible = false;
            lb_CourseData.Visible = false;
            lb_StudentData.Visible = false; 

            //Initilize the Matched Course List 
            _MatchedCourseRecords = new List<AssessmentCourseRecord>();
            _selectedAssessments = new List<string>();
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
                    lb_AssessmentData.Visible = true;
                    foreach (AssessmentRow ar in _AssessmentRows)
                    {
                        if (ar.Rubric_Row_Header.Contains((char)13))
                        {
                            //remove returns from data. 
                            ar.Rubric_Row_Header = ar.Rubric_Row_Header.Replace((char)13, ' ');
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
                else
                {
                    lb_CourseData.Visible = true;
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
                else
                {
                    lb_StudentData.Visible = true;
                }
            }
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            MatchCourseRecords();
        }

        private void btn_FilterSubjects_Click(object sender, EventArgs e)
        {
            //Check to make sure the user selected at least one subject and remove those courses that don't match.
            if (clb_Subjects.CheckedItems.Count > 0)
            {
                List<string> Subjects = new List<string>();
                List<string> CourseIDs = new List<string>();
                List<string> Assessments = new List<string>();
                List<string> Types = new List<string>();
                foreach (var item in clb_Subjects.CheckedItems)
                {
                    Subjects.Add(item.ToString());
                }
                //Remove courses that don't have the selected subject.
                _MatchedCourseRecords.RemoveAll(x => !Subjects.Contains(x.Subject));
                //Grab the remaining CourseIDs.
                CourseIDs = _MatchedCourseRecords.Select(x => x.BB_Course_ID).Distinct().ToList();
                //Remove Assessment Data for courses other than the remaining ones
                _AssessmentRows.RemoveAll(x => !CourseIDs.Contains(x.Course_ID));
                //Get a list of remaining Assessments.
                Assessments = _AssessmentRows.Select(x => x.Rubric_Name).Distinct().ToList();
                //Remove all those asssessments that do not have a double underscore.
                Assessments.RemoveAll(x => !x.Contains("__"));
                //Remove all assessment data that aren't in the selected assessments.
                _AssessmentRows.RemoveAll(x => !Assessments.Contains(x.Rubric_Name));
                foreach (string a in Assessments)
                {
                    //Assessment names are formatted like: [Name][Double Underscore][Type][Number]
                    //This is an example__KA4 
                    //To get the type, pull anything after the double underscore and remove the number if its there. 
                    string tmp = a.Substring(a.LastIndexOf("__"));
                    tmp = tmp.Replace("_", string.Empty);
                    tmp = Regex.Replace(tmp, @"[\d-]", string.Empty);
                    Types.Add(tmp);
                }
                //Populate the assessment types checkbox.
                //If there is only one type (KA,CDA, LA), automatically check the box and disable the button.
                Types = Types.Distinct().ToList();
                Types.Sort();
                if (Types.Count > 0)
                {
                    foreach (string t in Types)
                    {
                        clb_AssessmentTypes.Items.Add(t);
                    }
                    if (Types.Count == 1)
                    {
                        clb_AssessmentTypes.SetItemChecked(0, true);
                        btn_FilterAssessments.Enabled = false; 
                    }
                }
                _selectedAssessments = Assessments;
            }
            else
            {
                MessageBox.Show("Please select at least one subject for filtering.", "Filtering error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_FilterAssessments_Click(object sender, EventArgs e)
        {
            List<string> Types = new List<string>();
            List<string> RemainingAssessments = new List<string>();
            Debug.WriteLine("Filtering Types");
            if (clb_AssessmentTypes.CheckedItems.Count > 0)
            {
                foreach (var item in clb_AssessmentTypes.CheckedItems)
                {
                    Types.Add("__" + item);
                    Debug.WriteLine("__" + item);
                }
                foreach (string a in _selectedAssessments)
                {
                    string assessment = a.Substring(a.LastIndexOf("__"));
                    //Debug.WriteLine(assessment);
                    if (Types.Contains(assessment))
                    {
                        RemainingAssessments.Add(a);
                    }
                }

                Debug.WriteLine("old List");
                _selectedAssessments.ForEach(x=>Debug.WriteLine(x));
                Debug.WriteLine("new list");
                RemainingAssessments.ForEach(x => Debug.WriteLine(x));
            }
            else
            {
                MessageBox.Show("Please select at least one assessment type for filtering.", "Filtering error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_AddSelectedCourses_Click(object sender, EventArgs e)
        {

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

        private void MatchCourseRecords()
        {
            //Blackboard Course IDs include a '.' so lets remove all items in our Assessment Data that DO NOT include a '.'
            Debug.WriteLine("Prior: " + _AssessmentRows.Count);
            _AssessmentRows.RemoveAll(x => !x.Course_ID.Contains("."));
            Debug.WriteLine("Post: " + _AssessmentRows.Count);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<BannerCourseRecord, AssessmentCourseRecord>());
            var mapper = config.CreateMapper();

            Debug.WriteLine("Prior to match: " + _BannerCourseRecords.Count);
            foreach (BannerCourseRecord bcr in _BannerCourseRecords)
            {
                AssessmentCourseRecord acr = mapper.Map<AssessmentCourseRecord>(bcr);
                acr.BB_Course_ID = bcr.CRN + "." + bcr.Term;
                _MatchedCourseRecords.Add(acr);
                Debug.WriteLine(acr.BB_Course_ID.Length);
            }
            Debug.WriteLine("Post Match: " + _MatchedCourseRecords.Count);

            List<string> CourseIDs = _MatchedCourseRecords.Select(x => x.BB_Course_ID).Distinct().ToList();
            Debug.WriteLine("Prior to Match: " + _MatchedCourseRecords.Count);
            _MatchedCourseRecords.RemoveAll(x => !CourseIDs.Contains(x.BB_Course_ID));
            Debug.WriteLine("Post  Match: " + _MatchedCourseRecords.Count);

            List<string> Subjects = _MatchedCourseRecords.Select(x => x.Subject).Distinct().ToList();
            Subjects.Sort();
            Subjects.ForEach(x => clb_Subjects.Items.Add(x));
        }


        #endregion

        #region Workbook Methods
        #endregion

        #region Mathmatical Methods
        #endregion

    }
}
