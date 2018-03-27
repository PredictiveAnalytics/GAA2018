using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using General_Assessment_Analyzer.Classes;
using System.Diagnostics;
using ClosedXML;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace General_Assessment_Analyzer.Forms
{
    public partial class frmCOEReports : Form
    {
        string _pathToAssessmentFile;
        List<AssessmentRow> _AssessmentRows;
        List<COEStudentRecord> _StudentRecords; 
        List<string> StudentIds;
        string sql; 
        public frmCOEReports()
        {
            InitializeComponent();
            openFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog1.RestoreDirectory = true;
            StudentIds = new List<string>();
        }

        private void frmCOEReports_Load(object sender, EventArgs e)
        {

        }

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
            //Debug.WriteLine("Assessment Data loaded: " + _AssessmentRows.Count);

            //This is important.  Trying to correct an error in the rubric data.  Will review with Lisa B when I have a chance. 
            _AssessmentRows.RemoveAll(x => x.Rubric_Cell_Score == "NULL");
            StudentIds = _AssessmentRows.Select(x => x.STUDENT_ID).Distinct().ToList();

            return return_value;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
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
                    foreach (AssessmentRow ar in _AssessmentRows)
                    {
                        if (ar.Rubric_Row_Header.Contains((char)13))
                        {
                            //remove returns from data. 
                            ar.Rubric_Row_Header = ar.Rubric_Row_Header.Replace((char)13, ' ');
                        }
                    }
                    StudentIds.RemoveAll(x => string.IsNullOrEmpty(x));
                    //StudentIds.ForEach(x => Debug.WriteLine(x));
                    string instatement = "";
                    for (int i =0; i<StudentIds.Count;i++)
                    {
                        if (i==StudentIds.Count-1)
                        {
                            instatement = instatement + "'" + StudentIds[i] + "'";
                        }
                        else
                        {
                            instatement = instatement + "'" + StudentIds[i] + "',";
                        }
                    }
                    sql = "SELECT " + Environment.NewLine +
                          "s.SPRIDEN_ID as ID," + Environment.NewLine +
                          "s.SPRIDEN_LAST_NAME as LAST," + Environment.NewLine +  
                          "s.SPRIDEN_FIRST_NAME as FIRST," + Environment.NewLine +
                          "sgb.sgbstdn_program_1 as PROGRAM_1," + Environment.NewLine +
                          "sgb.sgbstdn_majr_code_1 as PROGRAM_1_MAJOR_1," + Environment.NewLine +
                          "sgb.sgbstdn_majr_code_1_2 as PROGRAM_1_MAJOR_2," + Environment.NewLine +
                          "sgb.sgbstdn_program_2 as PROGRAM_2," + Environment.NewLine +
                          "sgb.sgbstdn_majr_code_2 as PROGRAM_2_MAJOR_1," + Environment.NewLine +
                          "sgb.sgbstdn_majr_code_2_2 as PROGRAM_2_MAJOR_2" + Environment.NewLine +
                          @"FROM SPRIDEN S 
                            left join ( 
                                select a.sgbstdn_pidm, 
                                a.sgbstdn_program_1, 
                                a.sgbstdn_majr_code_1, 
                                a.sgbstdn_majr_code_1_2, 
                                a.sgbstdn_program_2, 
                                a.sgbstdn_majr_code_2, 
                                a.sgbstdn_majr_code_2_2
                                from sgbstdn a 
                                where 
                                  a.sgbstdn_term_code_eff=
                                    (select max(b.sgbstdn_term_code_eff) 
                                    from sgbstdn b 
                                    where a.sgbstdn_pidm=b.sgbstdn_pidm 
                                    and b.sgbstdn_term_code_eff <= '" + tbTerm.Text + "')) "
                                    + Environment.NewLine +
                                    "sgb on s.spriden_pidm=sgb.sgbstdn_pidm " +
                                    "WHERE S.SPRIDEN_CHANGE_IND IS NULL" + Environment.NewLine +
                                    "AND S.SPRIDEN_ID IN (" + instatement + ");";

                  
                    richTextBox1.Text = sql;
                }
            }

        }

        private void btnLoadResults_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                bool success = LoadStudentFile(openFileDialog1.FileName);
                if (success)
                {
                    _StudentRecords.ForEach(x => Debug.WriteLine("StudentInfo: " + x.ID));
                }
            }
        }

        private bool LoadStudentFile(string path)
        {
            bool return_value = false;
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var sr = new StreamReader(fs);
            var csv = new CsvHelper.CsvReader(sr);
            csv.Configuration.MissingFieldFound = null;
            csv.Configuration.BadDataFound = null;

            IEnumerable<COEStudentRecord> records = csv.GetRecords<COEStudentRecord>();
            _StudentRecords = records.ToList();
            Debug.WriteLine(_StudentRecords.Count);
            return_value = true;
            return return_value;
        }

        private void btn_SaveReport_Click(object sender, EventArgs e)
        {
            IXLWorkbook wb = new XLWorkbook();
            IXLWorksheet ws = wb.AddWorksheet("Student Information");

            int row = 1;
            ws.Cell(row, 1).Value = "ID";
            ws.Cell(row, 2).Value = "Last";
            ws.Cell(row, 3).Value = "First";
            ws.Cell(row, 4).Value = "Program 1";
            ws.Cell(row, 5).Value = "Program 1 Major 1";
            ws.Cell(row, 6).Value = "Program 2 Major 2";
            ws.Cell(row, 7).Value = "Program 2";
            ws.Cell(row, 8).Value = "Program 2 Major 1";
            ws.Cell(row, 9).Value = "Program 2 Major 2";
            row++;
            _StudentRecords.OrderBy(x => x.LAST).ThenByDescending(x => x.FIRST);

            foreach (COEStudentRecord sr in _StudentRecords)
            {
                ws.Cell(row, 1).Value = sr.ID;
                ws.Cell(row, 2).Value = sr.LAST;
                ws.Cell(row, 3).Value = sr.FIRST;
                ws.Cell(row, 4).Value = sr.PROGRAM_1;
                ws.Cell(row, 5).Value = sr.PROGRAM_1_MAJOR_1;
                ws.Cell(row, 6).Value = sr.PROGRAM_1_MAJOR_2;
                ws.Cell(row, 7).Value = sr.PROGRAM_2;
                ws.Cell(row, 8).Value = sr.PROGRAM_2_MAJOR_1;
                ws.Cell(row, 9).Value = sr.PROGRAM_2_MAJOR_2;
                row++;
            }
            BuildAssessmentSheets(wb);
            saveFileDialog1.Title = "Save Output";
            saveFileDialog1.Filter = "Excel Workbook | *.xlsx";
            DialogResult dr = saveFileDialog1.ShowDialog();
            wb.SaveAs(saveFileDialog1.FileName);
        }

        private void BuildAssessmentSheets(IXLWorkbook wb)
        {
            foreach (COEStudentRecord coe in _StudentRecords)
            {
                IXLWorksheet ws = wb.AddWorksheet("temp");
                List<string> Assessments = _AssessmentRows.Where(x => x.STUDENT_ID == coe.ID && x.Rubric_Name != null && !string.IsNullOrEmpty(x.Rubric_Name)).Select(x => x.Rubric_Name).Distinct().ToList();
                Assessments.Sort();
                int row = 1;
                int max = 30; 
                string sheetName = coe.ID + " " + coe.FIRST.Substring(0, 1);
                if (max-(sheetName.Length+coe.LAST.Length)>0)
                {
                    sheetName = sheetName + coe.LAST;
                    Debug.WriteLine("LT: " + sheetName); 
                }
                else
                {
                    Debug.WriteLine("GT");
                    sheetName = sheetName + " " + coe.LAST.Substring(0, 1);
                }

                ws.Cell(row, 1).Value = "Student Information";
                row++;
                ws.Cell(row, 1).Value = "ID";
                ws.Cell(row, 2).Value = "Last";
                ws.Cell(row, 3).Value = "First";
                ws.Cell(row, 4).Value = "Program 1";
                ws.Cell(row, 5).Value = "Program 1 Major 1";
                ws.Cell(row, 6).Value = "Program 1 Major 2 ";
                ws.Cell(row, 7).Value = "Program 2";
                ws.Cell(row, 8).Value = "Program 2 Major 1";
                ws.Cell(row, 9).Value = "Program 2 Major 2";
                row++;
                ws.Cell(row, 1).Value = coe.ID;
                ws.Cell(row, 2).Value = coe.LAST;
                ws.Cell(row, 3).Value = coe.FIRST;
                ws.Cell(row, 4).Value = coe.PROGRAM_1;
                ws.Cell(row, 5).Value = coe.PROGRAM_1_MAJOR_1;
                ws.Cell(row, 6).Value = coe.PROGRAM_1_MAJOR_2;
                ws.Cell(row, 7).Value = coe.PROGRAM_2;
                ws.Cell(row, 8).Value = coe.PROGRAM_2_MAJOR_1;
                ws.Cell(row, 9).Value = coe.PROGRAM_2_MAJOR_2;
                row+=2;
                ws.Cell(row, 1).Value = "Included Assessments";
                row++;
                foreach (string a in Assessments)
                {
                    ws.Cell(row, 1).Value = a;
                    row++;
                }
                row++; 
                foreach (string a in Assessments)
                {
                    List<string> standards = _AssessmentRows.Where(x => x.STUDENT_ID == coe.ID && x.Rubric_Name == a).Select(x => x.Rubric_Row_Header).Distinct().ToList();
                    ws.Cell(row, 1).Value = a;
                    row++;
                    standards.Sort();
                    foreach (string s in standards)
                    {
                        //ws.Cell(row, 10).Value = Regex.Replace(ar.Rubric_Row_Header, "<.*?>", " ");
                        ws.Cell(row, 1).Value = Regex.Replace(s, "<.*?>", " ");
                        string result = _AssessmentRows.Where(x => x.STUDENT_ID == coe.ID && x.Rubric_Name == a && x.Rubric_Row_Header == s).Select(x => x.Rubric_Column_Header).First();
                        ws.Cell(row, 2).Value = result;
                        //ws.Cell(row, 2).Value = _AssessmentRows.Where(x => x.STUDENT_ID == coe.ID && x.Rubric_Name == a && x.Rubric_Row_Header == s).Select(x => x.Rubric_Column_Header);
                        row++;
                    }
                    row++;
                }
                ws.Columns().AdjustToContents();
                ws.Column(1).Width = 150;
                ws.Column(1).Style.Alignment.WrapText = true;
                ws.Name = sheetName;
                //wb.AddWorksheet(sheetName);
            }

            
        }

    }
}
