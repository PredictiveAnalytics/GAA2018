using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AutoMapper;
using ClosedXML.Excel;
using General_Assessment_Analyzer.Classes;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using General_Assessment_Analyzer.Properties;


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
        public List<BannerCourseRecord> _UnFilteredBannerCourseRecords;
        public List<BannerStudentRecord> _BannerStudentRecords;
        public List<ScalePoint> _assessmentScale;
        public List<string> _selectedSubjects; 

        public List<string> _selectedAssessments;
        public List<string> _selectedCourseIDs;

        public IXLWorkbook _workbook;
        public int _frequency_col;
        public int _mean_col;
        public int _stDev_col;
        public int current_sheet_row;
        public Catalog _assessmentCatalog;

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

            //Initilize The Scale 
            _assessmentScale = new List<ScalePoint>();
        }

        #region Form Methods

        private void frmReport_Load(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string filepath = Path.Combine(path, "Resources");
            string file = Path.Combine(filepath, "CatalogFormatted.xml");
            if (!File.Exists(file))
            {
                File.WriteAllBytes(file, Encoding.ASCII.GetBytes(Resources.CatalogFormatted));
            }

            LoadCatalog(file, new XmlSerializer(typeof(Catalog)));

        }

        private void dgvScale_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Please enter only a number in the Value Column.", "Scale Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
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
                        "There was a problem reading the Course Data file.  Please check the documenation to check formatting. ",
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
                _selectedSubjects = Subjects;
                //Remove courses that don't have the selected subject.
                Debug.WriteLine("MatchedCourseRecordsPrv: " + _MatchedCourseRecords.Count);
                _MatchedCourseRecords.RemoveAll(x => !Subjects.Contains(x.Subject));
                Debug.WriteLine("MatchedCourseRecordsPost: " + _MatchedCourseRecords.Count);
                //Grab the remaining CourseIDs.
                CourseIDs = _MatchedCourseRecords.Select(x => x.BB_Course_ID).Distinct().ToList();
                Debug.WriteLine("CourseIDCount: " + CourseIDs.Count);
                //Remove Assessment Data for courses other than the remaining ones
                Debug.WriteLine("ARPrv: " + _AssessmentRows.Count);
                //_AssessmentRows.ForEach(x=>Debug.WriteLine(x.Course_ID));
                _AssessmentRows.RemoveAll(x => !CourseIDs.Contains(x.Course_ID));
                Debug.WriteLine("ARPost: " + _AssessmentRows.Count);
                //Get a list of remaining Assessments.
                Assessments = _AssessmentRows.Select(x => x.Rubric_Name).Distinct().ToList();
                Debug.WriteLine("Assessments: " + Assessments.Count);
                //Remove all those asssessments that do not have a double underscore.
                Assessments.RemoveAll(x => !x.Contains("__"));
                //Remove all assessment data that aren't in the selected assessments.
                _AssessmentRows.RemoveAll(x => !Assessments.Contains(x.Rubric_Name));
                foreach (string a in Assessments)
                {
                    Debug.WriteLine(a);
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
                        PopulateCourseInformation(CourseIDs);
                    }
                }
                _selectedAssessments = Assessments;
                _selectedCourseIDs = CourseIDs;
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
            List<string> RemainingCourseIDs = new List<string>();
            Debug.WriteLine("Filtering Types");
            if (clb_AssessmentTypes.CheckedItems.Count > 0)
            {
                Debug.WriteLine("Begin Filter of Assessment Types");
                foreach (var item in clb_AssessmentTypes.CheckedItems)
                {
                    Types.Add("__" + item);
                    Debug.WriteLine("__" + item);
                }

                Debug.WriteLine("Idents");
                foreach (string a in _selectedAssessments)
                {
                    //tmp = Regex.Replace(tmp, @"[\d-]", string.Empty);
                    string ident = a.Substring(a.LastIndexOf("__"));
                    ident = Regex.Replace(ident, @"[\d-]", string.Empty);
                    if (Types.Contains(ident))
                    {
                        Debug.WriteLine("Keeping: " + a);
                        RemainingAssessments.Add(a);
                    }
                    else
                    {
                        Debug.WriteLine("Removing: " + a);
                    }
                }
                _selectedAssessments = RemainingAssessments;
                //_AssessmentRows.RemoveAll(x => !Assessments.Contains(x.Rubric_Name));
                _AssessmentRows.RemoveAll(x => !_selectedAssessments.Contains(x.Rubric_Name));
                //CourseIDs = _MatchedCourseRecords.Select(x => x.BB_Course_ID).Distinct().ToList();
                RemainingCourseIDs = _AssessmentRows.Select(x => x.Course_ID).Distinct().ToList();
                //RemainingCourseIDs.ForEach(x=>clb_CoursesWithAssessments.Items.Add(x));
                PopulateCourseInformation(RemainingCourseIDs);
            }

            else
            {
                MessageBox.Show("Please select at least one assessment type for filtering.", "Filtering error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode tn in tvCourses.Nodes)
            {
                if (!tn.Checked)
                {
                    tn.Checked = true;
                }
            }
        }

        private void btn_AddSelectedCourses_Click(object sender, EventArgs e)
        {
            List<string> SelectedCourses = new List<string>();
            List<string> SelectedCIDS = new List<string>();
            List<string> Scale = new List<string>();
            int count = 0;
            foreach (TreeNode tn in tvCourses.Nodes)
            {
                if (tn.Parent == null && tn.Checked)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                btn_AddSelectedCourses.Enabled = false;
                btn_SelectAll.Enabled = false;
                //string input = "User name (sales)";
                //string output = input.Split('(', ')')[1];
                foreach (TreeNode tn in tvCourses.Nodes)
                {
                    if (tn.Checked)
                    {
                        SelectedCourses.Add(tn.Text);
                    }
                }
                foreach (string c in SelectedCourses)
                {
                    string tmp = c.Split('(', ')')[1];
                    SelectedCIDS.Add(tmp);
                    Debug.WriteLine(tmp);
                }

                _AssessmentRows.RemoveAll(x => !SelectedCIDS.Contains(x.Course_ID));
                _AssessmentRows.RemoveAll(x => !_selectedAssessments.Contains(x.Rubric_Name));
                Scale = _AssessmentRows.Where(x => !string.IsNullOrEmpty(x.Rubric_Column_Header))
                    .Select(x => x.Rubric_Column_Header).Distinct().ToList();
                //Scale = _AssessmentRows.Select(x => x.Rubric_Column_Header).Distinct().ToList();
                Scale.ForEach(x=>Debug.WriteLine(x));
                foreach (string s in Scale)
                {
                    if (s.Length > 0)
                    {
                        ScalePoint p = new ScalePoint(s, 0);
                        _assessmentScale.Add(p);
                    }
                }
                dgvScale.DataSource = _assessmentScale;
                dgvScale.Columns[0].ReadOnly = true;




                List<string> StudentIDs = _AssessmentRows.Where(x => SelectedCIDS.Contains(x.Course_ID))
                    .Select(x => x.STUDENT_ID).Distinct().ToList();
                _BannerStudentRecords.RemoveAll(x => !StudentIDs.Contains(x.ID));
                StudentIDs.ForEach(x=>Debug.WriteLine(x));
                List<string> RateCodes = _BannerStudentRecords.Where(x => StudentIDs.Contains(x.ID))
                    .Select(x => x.RateCode + " (" + x.RateDesc + ")").Distinct().ToList();
                List<string> MajorCodes = _BannerStudentRecords.Where(x => StudentIDs.Contains(x.ID))
                    .Select(x => x.MajorCode + " (" + x.MajorDesc + ")").Distinct().ToList();

                RateCodes.Sort();
                MajorCodes.Sort();

                RateCodes.ForEach(x=>clb_Rates.Items.Add(x));
                MajorCodes.ForEach(x=>clb_Majors.Items.Add(x));

                for (int i = 0; i < clb_Rates.Items.Count; i++)
                {
                    clb_Rates.SetItemCheckState(i, CheckState.Checked);
                }

                for (int i = 0; i < clb_Majors.Items.Count; i++)
                {
                    clb_Majors.SetItemCheckState(i, CheckState.Checked);
                }
                btn_AddSelectedCourses.Enabled = false;

            }
            else
            {
                MessageBox.Show("Please select at least one Course for inclusion in the report.", "Selection error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void tvCourses_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null && e.Node.Checked)
            {
                //Parent Node checked
                Debug.WriteLine(e.Node.Text);
                foreach (TreeNode p in tvCourses.Nodes)
                {
                    if (e.Node.Text == p.Text)
                    {
                        foreach (TreeNode subNode in p.Nodes)
                        {
                            subNode.Checked = true;
                            if (subNode.Nodes.Count > 0)
                            {
                                foreach (TreeNode c in subNode.Nodes)
                                {
                                    c.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (e.Node.Parent == null && !e.Node.Checked)
            {
                Debug.WriteLine("Unchecked");
                foreach (TreeNode p in tvCourses.Nodes)
                {
                    if (e.Node.Text == p.Text)
                    {
                        foreach (TreeNode subNode in p.Nodes)
                        {
                            subNode.Checked = false;
                            if (subNode.Nodes.Count > 0)
                            {
                                foreach (TreeNode c in subNode.Nodes)
                                {
                                    c.Checked = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_FilterRates_Click(object sender, EventArgs e)
        {
            List<string> excludedRateCodes = new List<string>();
            IEnumerable<object> UncheckedItems = (from object item in clb_Rates.Items
                                                  where !clb_Rates.CheckedItems.Contains(item)
                                                  select item);
            //excluded.ForEach(x=>Debug.WriteLine(x));
            List<string> excludedStudents = new List<string>();
            foreach (object item in UncheckedItems)
            {
                string tmp = item.ToString();
                tmp = tmp.Substring(0, tmp.IndexOf('(')).Trim();
                excludedRateCodes.Add(tmp);
                Debug.WriteLine(tmp);
            }
            Debug.WriteLine("Before Removing Students: " + _BannerStudentRecords.Count);
            excludedStudents = _BannerStudentRecords.Where(x => excludedRateCodes.Contains(x.RateCode))
                .Select(x => x.ID).Distinct().ToList();
            _BannerStudentRecords =
                _BannerStudentRecords.Where(x => !excludedStudents.Contains(x.ID)).Distinct().ToList();
            Debug.WriteLine("After Removing Students: " + _BannerStudentRecords.Count);

        }

        private void btn_FilterMajors_Click(object sender, EventArgs e)
        {
            List<string> excludedMajorCodes = new List<string>();
            IEnumerable<object> UncheckedItems = (from object item in clb_Majors.Items
                                                  where !clb_Majors.CheckedItems.Contains(item)
                                                  select item);
            //excluded.ForEach(x=>Debug.WriteLine(x));
            List<string> excludedStudents = new List<string>();
            foreach (object item in UncheckedItems)
            {
                string tmp = item.ToString();
                tmp = tmp.Substring(0, tmp.IndexOf('(')).Trim();
                excludedMajorCodes.Add(tmp);
                Debug.WriteLine(tmp);
            }
            Debug.WriteLine("Before Removing Students: " + _BannerStudentRecords.Count);
            excludedStudents = _BannerStudentRecords.Where(x => excludedMajorCodes.Contains(x.MajorCode))
                .Select(x => x.ID).Distinct().ToList();
            _BannerStudentRecords =
                _BannerStudentRecords.Where(x => !excludedStudents.Contains(x.ID)).Distinct().ToList();
            Debug.WriteLine("After Removing Students: " + _BannerStudentRecords.Count);
        }

        private void btn_SaveWorkbook_Click(object sender, EventArgs e)
        {
            //
            _assessmentScale.ForEach(x=>Debug.WriteLine("BugHunt:" + x.Label + " " + x.Value));
           _assessmentScale.Sort();
            BuildWorkbook();
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

            //This is important.  Trying to correct an error in the rubric data.  Will review with Lisa B when I have a chance. 
            _AssessmentRows.RemoveAll(x => x.Rubric_Cell_Score == "NULL");
            
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
                _UnFilteredBannerCourseRecords = _BannerCourseRecords;
                return_value = true;
                Debug.WriteLine("Banner Course Records loaded: " + _BannerCourseRecords.Count);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error Reading Course File " + path + e.ToString());
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

        private void PopulateCourseInformation(List<string> CourseIDs)
        {
            foreach (string cid in CourseIDs)
            {
                string[] c = cid.Split('.');
                string crn = c[0];
                string term = c[1];

                //clb_CoursesWithAssessments.Items.Add(crn + "." + term);
                foreach (BannerCourseRecord bcr in _BannerCourseRecords)
                {
                    if (bcr.CRN == crn && bcr.Term == term)
                    {
                        List<string> includedAssessments = new List<string>();
                        //Found
                        //clb_CoursesWithAssessments.Items.Add((bcr.Subject + "-" + bcr.Number + ": " + bcr.Title + " (" + cid + ")"));
                        TreeNode rootNode = new TreeNode((bcr.Subject + "-" + bcr.Number + ": " + bcr.Title + " (" + cid + ")"));
                        TreeNode childInfoNode = new TreeNode("Course Information");
                        childInfoNode.Nodes.Add("Term: " + bcr.Term_Desc + "(" + bcr.Term + ")");
                        childInfoNode.Nodes.Add("PTRM : " + bcr.PTRM);
                        childInfoNode.Nodes.Add("Instructor: " + bcr.Instructor_Last + ", " + bcr.Instructor_First);
                        childInfoNode.Nodes.Add("Enrollment: " + bcr.Enrollment);
                        TreeNode childAssessmentNode = new TreeNode("Assessments");
                        foreach (AssessmentRow ar in _AssessmentRows)
                        {
                            if (ar.Course_ID == cid)
                            {
                                includedAssessments.Add(ar.Rubric_Name);
                            }
                        }
                        includedAssessments = includedAssessments.Distinct().ToList();
                        includedAssessments.Sort();
                        foreach (string a in includedAssessments)
                        {
                            if (_selectedAssessments.Contains(a))
                            {
                                childAssessmentNode.Nodes.Add(a);
                            }
                        }
                        rootNode.Nodes.Add(childInfoNode);
                        rootNode.Nodes.Add(childAssessmentNode);
                        tvCourses.Nodes.Add(rootNode);
                    }
                }
            }
            
        }

        #endregion

        #region Workbook Methods

        private void BuildWorkbook()
        {
            _workbook = new XLWorkbook();
            //List<string> revisedNames = new List<string>();
            foreach (string assessment in _selectedAssessments)
            {
                string assessment_ident = assessment.Substring(assessment.LastIndexOf("__"));
                string revised_name; 
                int max_char = 29 - assessment_ident.Length;
                if (assessment.Length >= 30)
                {
                    revised_name = assessment.Substring(0, max_char) + assessment_ident;
                    revised_name = revised_name.Replace(" ", "").Trim();
                    Debug.WriteLine(revised_name + " : " + assessment);
                    AddWorksheet(_workbook, revised_name, assessment);
                }
                else
                {
                    AddWorksheet(_workbook, assessment, assessment);
                }
                //Debug.WriteLine(assessment + " " + assessment.Length);
            }
            BuildCompletionsReport(_workbook);
            BuildStudentReport(_workbook);
            SaveWorkbook(_workbook);
        }

        private void BuildStudentReport(IXLWorkbook wb)
        {
            IXLWorksheet ws = wb.AddWorksheet("Student Report");
            ws.Position = 2;
            int row = 1;
            ws.Cell(row, 1).Value = "Student Performance Report";
            ws.Range(row, 1, row, 12).Merge();
            ws.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Font.FontColor = XLColor.Black;
            row++;
            ws.Cell(row, 1).Value = "ID";
            ws.Cell(row, 2).Value = "Last Name";
            ws.Cell(row, 3).Value = "First Name";
            ws.Cell(row, 4).Value = "Middle";
            ws.Cell(row, 5).Value = "Degree";
            ws.Cell(row, 6).Value = "Major";
            ws.Cell(row, 7).Value = "Cohort";
            ws.Cell(row, 8).Value = "Course";
            ws.Cell(row, 9).Value = "Assessment";
            ws.Cell(row, 10).Value = "Standard";
            ws.Cell(row, 11).Value = "Result";
            ws.Cell(row, 12).Value = "Score";
            ws.Range(row, 1, row, 12).Style.Fill.BackgroundColor = XLColor.Maroon;
            ws.Range(row, 1, row, 12).Style.Font.Bold = true;
            ws.Range(row, 1, row, 12).Style.Font.FontColor = XLColor.White;
            row++;
            foreach (BannerStudentRecord bsr in _BannerStudentRecords)
            {
                List<AssessmentRow> bsr_assessments = _AssessmentRows.Where(x => x.STUDENT_ID == bsr.ID && x.Rubric_Column_Header != "").ToList();
                foreach (AssessmentRow ar in bsr_assessments)
                {
                    ws.Cell(row, 1).Value = bsr.ID;
                    ws.Cell(row, 2).Value = bsr.Last;
                    ws.Cell(row, 3).Value = bsr.First;
                    ws.Cell(row, 4).Value = bsr.Middle;
                    ws.Cell(row, 5).Value = bsr.Degree;
                    ws.Cell(row, 6).Value = bsr.MajorCode + " (" + bsr.MajorDesc + ")";
                    ws.Cell(row, 7).Value = bsr.Cohort;
                    ws.Cell(row, 8).Value = CourseNumberAndTitle(ar.Course_ID);
                    ws.Cell(row, 9).Value = ar.Rubric_Name;
                    //ws.Cell(row, 9).Value = ar.Rubric_Row_Header;
                    //string tmp = Regex.Replace(s, "<.*?>", " ");
                    ws.Cell(row, 10).Value = Regex.Replace(ar.Rubric_Row_Header, "<.*?>", " ");
                    ws.Cell(row, 11).Value = ar.Rubric_Column_Header;
                    ws.Cell(row, 12).Value = _assessmentScale.Where(x => x.Label == ar.Rubric_Column_Header)
                        .Select(y => y.Value);
                    row++;
                }
            }
            ws.Columns().AdjustToContents();
        }

        private void BuildCompletionsReport(IXLWorkbook wb)
        {
            IXLWorksheet ws = wb.AddWorksheet("Completions Report");
            Debug.WriteLine("Filter Checked: " + _selectedSubjects.Count);
            Debug.WriteLine("Unfiltered Courses: " + _UnFilteredBannerCourseRecords.Count);
            List<BannerCourseRecord> courses = _UnFilteredBannerCourseRecords.Where(x => _selectedSubjects.Contains(x.Subject)).Distinct().ToList();
            Debug.WriteLine("Filter Courses: " + courses.Count);
            Debug.WriteLine("CompReport");
            courses.ForEach(x=>Debug.WriteLine(x));
            ws.Position = 1;
            int row = 1;
            ws.Cell(row, 1).Value = "Completions Report";
            ws.Range(row, 1, row, 6).Merge();
            ws.Cell(row, 1).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, 1).Style.Font.Bold = true;
            row++;
            ws.Cell(row, 1).Value = "Term";
            ws.Cell(row, 2).Value = "CRN";
            ws.Cell(row, 3).Value = "Course";
            ws.Cell(row, 4).Value = "Instructor"; 
            ws.Cell(row, 5).Value = "Assessment";
            ws.Cell(row, 6).Value = "Completed";
            ws.Range(row, 1, row, 6).Style.Fill.BackgroundColor = XLColor.Maroon;
            ws.Range(row, 1, row, 6).Style.Font.Bold = true;
            ws.Range(row, 1, row, 6).Style.Font.FontColor = XLColor.White;
            row++;
            foreach (BannerCourseRecord bcr in courses)
            {
                string key = bcr.Subject + bcr.Number;
                bool found = false;
                foreach (CatalogEntry ce in _assessmentCatalog.Entries)
                {
                    if (ce.CourseKey == key)
                    {
                        found = true;
                    }
                }
                if (found)
                {
                    List<string> idents = _assessmentCatalog.Entries.Where(x => x.CourseKey == key)
                        .Select(x => x.Assessment).ToList();
                    List<string> compAssessments = AssessmentIdentsInCID(bcr.CRN + "." + bcr.Term);
                    if (idents.Count > 0)
                    {
                        idents.ForEach(x=>Debug.WriteLine(key + " " + x));
                        foreach (string ident in idents)
                        {
                            bool completed = false;
                            ws.Cell(row, 1).Value = bcr.Term;
                            ws.Cell(row, 2).Value = bcr.CRN;
                            ws.Cell(row, 3).Value = bcr.Subject + "-" + bcr.Number + "-" + bcr.Section;
                            ws.Cell(row, 4).Value = bcr.Instructor_First + " " + bcr.Instructor_Last;
                            ws.Cell(row, 5).Value = ident;
                            foreach (string comp in compAssessments)
                            {
                                Debug.WriteLine("Ident: " + ident + " " + "Comp:" + comp);
                                string assessment = comp.Replace("_", string.Empty).Trim().ToLower();
                                string checkIdent = ident.Trim().ToLower();
                                if (checkIdent == assessment)
                                {
                                    completed = true;
                                }
                            }
                            if (completed)
                            {
                                ws.Cell(row, 6).Value = "Yes";
                            }
                            else
                            {
                                ws.Cell(row, 6).Value = "No";
                                ws.Cell(row, 1).Style.Font.FontColor = XLColor.Red;
                                ws.Cell(row, 2).Style.Font.FontColor = XLColor.Red;
                                ws.Cell(row, 3).Style.Font.FontColor = XLColor.Red;
                                ws.Cell(row, 4).Style.Font.FontColor = XLColor.Red;
                                ws.Cell(row, 5).Style.Font.FontColor = XLColor.Red;
                                ws.Cell(row, 6).Style.Font.FontColor = XLColor.Red;
                            }
                            row++;
                        }
                    }
                }
                else
                {
                    ws.Cell(row, 1).Value = bcr.Term;
                    ws.Cell(row, 2).Value = bcr.CRN;
                    ws.Cell(row, 3).Value = bcr.Subject + "-" + bcr.Number + "-" + bcr.Section;
                    ws.Cell(row, 4).Value = bcr.Instructor_First + " " + bcr.Instructor_Last;
                    ws.Cell(row, 5).Value = "Course not in Catalog";
                    row++;
                }
                
            }
            ws.Columns().AdjustToContents();
        }

        private void AddWorksheet(IXLWorkbook wb, string sheetName, string assessment)
        {
            sheetName = sheetName.Replace(":", string.Empty);
            sheetName = sheetName.Replace(@"\", string.Empty);
            sheetName = sheetName.Replace("/", string.Empty);
            sheetName = sheetName.Replace("?", string.Empty);
            sheetName = sheetName.Replace("*", string.Empty);
            sheetName = sheetName.Replace("[", string.Empty);
            sheetName = sheetName.Replace("]", string.Empty);
            IXLWorksheet ws = wb.AddWorksheet(sheetName);
            int maxCol = 1 + _assessmentScale.Count + 3;
            BuildHeaderRow(wb, ws, 1, assessment);
            current_sheet_row = BuildTotals(wb, ws,assessment,3);
            ws.Cell(current_sheet_row, 1).Value = "Assessments by Course";
            ws.Range(current_sheet_row, 1, current_sheet_row, maxCol).Merge();
            ws.Cell(current_sheet_row, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(current_sheet_row, 1).Style.Font.Bold = true;
            ws.Cell(current_sheet_row, 1).Style.Fill.BackgroundColor = XLColor.Maroon;
            ws.Cell(current_sheet_row, 1).Style.Font.FontColor = XLColor.White;
            current_sheet_row++;
            foreach (string cid in _selectedCourseIDs)
            {
                bool found_assessment = AssessmentInCourse(assessment, cid);
                if (found_assessment)
                {
                    current_sheet_row = BuildCourseHeaderRow(wb, ws, current_sheet_row,cid, assessment);
                    current_sheet_row = BuildCourseTotals(wb, ws, assessment, cid, current_sheet_row);
                }
            }
            current_sheet_row = BuildFooter(ws, current_sheet_row);
        }

        private void BuildHeaderRow(IXLWorkbook wb, IXLWorksheet ws, int row, string assessment)
        {
            //maxCol: Col 1 + number of Scale Points + 3;
            // 3 is for each of the calculated columns: Frequency, Mean, and St Dev
            int col = 1;
            int maxCol = 1 + _assessmentScale.Count + 3;
            ws.Range(row, col, row, maxCol).Merge();
            ws.Cell(row, col).Value = "Assessment Report -- " + assessment;
            ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.Maroon;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.White;

            row++;
            ws.Cell(row, col).Value = "Standard";
            ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            col++;
            foreach (ScalePoint p in _assessmentScale)
            {
                ws.Cell(row, col).Value = p.Label + "--" + p.Value.ToString();
                ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(row, col).Style.Font.Bold = true;
                ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
                col++;
            }
            _frequency_col = col;
            ws.Cell(row, col).Value = "Frequency (N)";
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            col++;
            ws.Cell(row, col).Value = "Mean";
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            _mean_col = col;
            col++;
            ws.Cell(row, col).Value = "St Dev";
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            _stDev_col = col;
        }

        private int BuildFooter(IXLWorksheet ws, int row)
        {
            int col = 1;
            int maxCol = 1 + _assessmentScale.Count + 3;
            ws.Range(row, col, row, maxCol).Merge();
            ws.Cell(row, col).Value = "Assessment Report Provided by The Office of Insitutional Research and Assessment";
            ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.Maroon;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.White;

            row++;
            ws.Range(row, col, row, maxCol).Merge();
            ws.Cell(row, col).Value = "Prepared on: " + DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
            ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row,col).Style.Fill.BackgroundColor = XLColor.Maroon;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.White;

            //ws.Column(1).Width = 100;
            //ws.Column(1).AdjustToContents();
            for (int i = 1; i <= maxCol; i++)
            {
                ws.Column(i).AdjustToContents();
            }
            ws.Column(1).Width = 100;
            ws.Column(1).Style.Alignment.WrapText = true;

            return row;
        }

        private int BuildCourseHeaderRow(IXLWorkbook wb, IXLWorksheet ws, int row, string cid, string assessment)
        {
            //maxCol: Col 1 + number of Scale Points + 3;
            // 3 is for each of the calculated columns: Frequency, Mean, and St Dev
            int col = 1;
            int maxCol = 1 + _assessmentScale.Count + 3;
            ws.Range(row, col, row, maxCol).Merge();
            //ws.Cell(row, col).Value = "Course: " + cid;
            ws.Cell(row, col).Value = CourseNumberAndTitle(cid);
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            row++;
            ws.Range(row, col, row, maxCol).Merge();
            ws.Cell(row, col).Value = "Term: " + CourseTerm(cid);
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            row++;
            ws.Range(row, col, row, maxCol).Merge();
            ws.Cell(row, col).Value = "Dates: " + CourseDates(cid);
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            row++;
            ws.Range(row, col, row, maxCol).Merge();
            ws.Cell(row, col).Value = "Instructor: " + Instructor(cid);
            ws.Cell(row, col).Hyperlink.ExternalAddress = InstEmailUri(cid, assessment);
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            row++;
            ws.Cell(row, col).Value = "Standard";
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            col++;
            foreach (ScalePoint p in _assessmentScale)
            {
                ws.Cell(row, col).Value = p.Label + "--" + p.Value.ToString();
                ws.Cell(row, col).Style.Font.Bold = true;
                ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
                ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
                col++;
            }
            _frequency_col = col;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            ws.Cell(row, col).Value = "Frequency (N)";
            col++;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            ws.Cell(row, col).Value = "Mean";
            _mean_col = col;
            col++;
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = XLColor.LightGray;
            ws.Cell(row, col).Style.Font.FontColor = XLColor.Black;
            ws.Cell(row, col).Value = "St Dev";
            _stDev_col = col;
            row++;
            return row;
        }

        private int BuildTotals(IXLWorkbook wb, IXLWorksheet ws, string assessment, int row)
        {
            int rt_row = row;
            List<string> Standards = AssessmentStandards(assessment);
            Standards.Sort();
            foreach (string s in Standards)
            {
                //ws.Cell(row, 1).Value = s;
                //ws.Cell(row, 1).Value = Regex.Replace(s, "<.*?>", " ");
                string tmp = Regex.Replace(s, "<.*?>", " ");
                tmp = Regex.Replace(tmp, "&amp;", "&");
                ws.Cell(row, 1).Value = tmp;
                int col = 2;
                Dictionary<string, int> dic = AssessmentTotals(assessment, s);
                foreach (ScalePoint p in _assessmentScale)
                {
                    ws.Cell(row, col).Value = dic[p.Label];
                    col++;
                }
                ws.Cell(row, _frequency_col).Value = StandardFrequencyByAssessment(assessment, s);
                ws.Cell(row, _mean_col).Value = MeanFromDictionary(dic);
                ws.Cell(row, _stDev_col).Value = StDevFromDic(dic);
                row++;
            }
            rt_row = row;
            return rt_row;
        }

        private int BuildCourseTotals(IXLWorkbook wb, IXLWorksheet ws, string assessment, string cid, int row)
        {
            int rt_row = row;
            List<string> Standards = AssessmentStandards(assessment);
            Standards.Sort();
            foreach (string s in Standards)
            {
                //ws.Cell(row, 1).Value = s;
                //ws.Cell(row, 1).Value = Regex.Replace(s, "<.*?>", " ");
                string tmp = Regex.Replace(s, "<.*?>", " ");
                tmp = Regex.Replace(tmp, "&amp;", "&");
                ws.Cell(row, 1).Value = tmp;
                int col = 2;
                Dictionary<string, int> dic = CourseTotals(assessment, s, cid, row);
                foreach (ScalePoint p in _assessmentScale)
                {
                    ws.Cell(row, col).Value = dic[p.Label];
                    col++;
                }
                ws.Cell(row, _frequency_col).Value = StandardFrequencyByAssessmentAndCourse(assessment, s, cid);
                ws.Cell(row, _mean_col).Value = MeanFromDictionary(dic);
                ws.Cell(row, _stDev_col).Value = StDevFromDic(dic);
                row++; 
            }
            rt_row = row;
            return rt_row;
        }

        private bool AssessmentInCourse(string assessment, string cid)
        {
            int count = _AssessmentRows.Where(x => x.Rubric_Name == assessment && x.Course_ID == cid).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SaveWorkbook(IXLWorkbook wb)
        {
            saveFileDialog1.Title = "Save Output";
            saveFileDialog1.Filter = "Excel Workbook | *.xlsx";
            DialogResult dr = saveFileDialog1.ShowDialog();
            wb.SaveAs(saveFileDialog1.FileName);
            lb_WbSaved.Visible = true;
            lb_WbPath.Text = saveFileDialog1.FileName.ToString();
        }

        private List<string> AssessmentStandards(string assessment)
        {
            List<string> rt_List = new List<string>();
            rt_List = _AssessmentRows.Where(x => x.Rubric_Name == assessment && x.Rubric_Column_Header !="").Select(x => x.Rubric_Row_Header)
                .Distinct().ToList();
            return rt_List;

        }

        private int StandardFrequencyByAssessment(string assessment, string standard)
        {
            int rt_value = 0;

            rt_value = _AssessmentRows.Where(x => x.Rubric_Name == assessment && x.Rubric_Row_Header == standard && x.Rubric_Column_Header !="")
                .Count();

            return rt_value;
            
        }

        private int TotalNFromDictionary(Dictionary<string, int> dictionary)
        {
            int value = 0;
            foreach (KeyValuePair<string, int> kp in dictionary)
            {
                value += kp.Value;
            }
            return value;
        }

        private double MeanFromDictionary(Dictionary<string, int> dictionary)
        {
            double value = 0;
            int totalN = TotalNFromDictionary(dictionary);
            int timesValue = 0;

            foreach (KeyValuePair<string, int> kp in dictionary)
            {
                foreach (ScalePoint p in _assessmentScale)
                {
                    if (p.Label == kp.Key)
                    {
                        timesValue += kp.Value * p.Value;
                    }
                }
            }
            if (timesValue > 0)
            {
                value = Math.Round((double) timesValue / totalN, 2);
            }
            return value;
        }

        private double StDevFromDic(Dictionary<string, int> dictionary)
        {
            double rt_StDev = 0;
            List<int> Values = new List<int>();

            foreach (KeyValuePair<string, int> kv in dictionary)
            {
                foreach (ScalePoint p in _assessmentScale)
                {
                    if (p.Label == kv.Key)
                    {
                        for (int i = 0; i < kv.Value; i++)
                        {
                            Values.Add(p.Value);
                        }
                    }
                }
            }
            if (Values.Count > 1)
            {
                double avg = Values.Average();
                double sum = Values.Sum(d => Math.Pow(d - avg, 2));
                rt_StDev = Math.Round(Math.Sqrt((sum) / (Values.Count() - 1)), 2);
            }
            else
            {
                rt_StDev = 0;
            }
            return rt_StDev;
        }

        private int StandardFrequencyByAssessmentAndCourse(string assessment, string standard, string cid)
        {
            int rt_value = 0;

            rt_value = _AssessmentRows.Where(x => x.Rubric_Name == assessment && x.Rubric_Row_Header == standard && x.Course_ID== cid && x.Rubric_Column_Header != "")
                .Count();

            return rt_value;
        }

        private Dictionary<string, int> AssessmentTotals(string assessment, string standard)
        {
            Dictionary<string, int> rtDictionary = new Dictionary<string, int>();
            foreach (ScalePoint p in _assessmentScale)
            {
                if (p.Label != "")
                {
                    rtDictionary.Add(p.Label, 0);
                }
            }
            foreach (AssessmentRow ar in _AssessmentRows)
            {
                if (ar.Rubric_Name == assessment && ar.Rubric_Row_Header == standard && ar.Rubric_Column_Header !="")
                {
                    rtDictionary[ar.Rubric_Column_Header]++;
                }
            }
            return rtDictionary;
        }

        private Dictionary<string, int> CourseTotals(string assessment, string standard, string courseID, int start_row)
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();
            foreach (ScalePoint p in _assessmentScale)
            {
                if (p.Label != "")
                {
                    tmp.Add(p.Label, 0);
                }
            }

            foreach (AssessmentRow ar in _AssessmentRows)
            {
                if (ar.Rubric_Name == assessment
                    && ar.Rubric_Row_Header == standard
                    && ar.Course_ID == courseID && ar.Rubric_Column_Header !="")
                {
                    tmp[ar.Rubric_Column_Header]++;
                }
            }
            return tmp;
        }

        private string CourseNumberAndTitle(string cid)
        {
            return _MatchedCourseRecords.Where(x => x.BB_Course_ID == cid)
                .Select(x => x.Subject + "-" + x.Number + "-" + x.Section + ": " + x.Title + " (" + cid + ")").First();
        }

        private string CourseKeyFromCID(string cid)
        {
            return _MatchedCourseRecords.Where(x => x.BB_Course_ID == cid)
                .Select(x => x.Subject + x.Number).First();
        }

        private List<string> AssessmentIdentsInCID(string cid)
        {
            List<string> rt_list = new List<string>();
            List<string> assessmentsinCourse = _AssessmentRows.Where(x => x.Course_ID == cid).Select(x => x.Rubric_Name)
                .Distinct().ToList();
            foreach (string a in assessmentsinCourse)
            {
                string tmp = a.Substring(a.LastIndexOf("__"));
                tmp = tmp.ToUpper();
                //Debug.WriteLine(tmp);
                rt_list.Add(tmp);
            }
            rt_list = rt_list.Distinct().ToList();
            return rt_list;
        }

        private string CourseTerm(string cid)
        {
            return _MatchedCourseRecords.Where(x => x.BB_Course_ID == cid)
                .Select(x => x.Term_Desc + " (" + x.Term + ")").First();
        }

        private string CourseCRN(string cid)
        {
            return _MatchedCourseRecords.Where(x => x.BB_Course_ID == cid)
                .Select(x => x.CRN).First();
        }

        private string CourseDates(string cid)
        {
            return _MatchedCourseRecords.Where(x => x.BB_Course_ID == cid)
                .Select(x => x.Start_Date + " through " + x.End_Date).First();
        }

        private string Instructor(string cid)
        {
            return _MatchedCourseRecords.Where(x => x.BB_Course_ID == cid)
                .Select(x => x.Instructor_First + " " + x.Instructor_Last).First();
        }

        private Uri InstEmailUri(string courseID, string assessment)
        {
            Uri rt = null;
            string crn = _MatchedCourseRecords.Where(x => x.BB_Course_ID == courseID).Select(x => x.CRN).First();
            string term = _MatchedCourseRecords.Where(x => x.BB_Course_ID == courseID).Select(x => x.Term).First();
            string instEmail = _MatchedCourseRecords.Where(x=>x.BB_Course_ID == courseID).Select(x=>x.Email).First();
            string mailto = "mailto:";
            string subject = "?subject=Assessment Data (" + crn + "." + term + ") " + assessment;

            string uri = mailto + instEmail + subject;
            rt = new Uri(uri);

            return rt;
        }

        private void LoadCatalog(string path, XmlSerializer serializer)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                _assessmentCatalog = (Catalog)serializer.Deserialize(fs);
                lbCatalog.Visible = true;
                fs.Close();
                fs.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " " + ex.Source);
            }
        }

        #endregion
    }
}
