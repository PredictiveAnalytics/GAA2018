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

namespace General_Assessment_Analyzer.Forms
{
    public partial class frmCOEReports : Form
    {
        string _pathToAssessmentFile;
        List<AssessmentRow> _AssessmentRows;
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
            Debug.WriteLine("Assessment Data loaded: " + _AssessmentRows.Count);

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
                    StudentIds.ForEach(x => Debug.WriteLine(x));
                    sql = "SELECT SPRIDEN_ID, SPRIDEN_LAST_NAME, SPRIDEN_FIRST_NAME " +
                        Environment.NewLine +
                        "FROM SPRIDEN " + Environment.NewLine + 
                        "WHERE SPRIDEN_CHANGE_IND IS NULL " +
                        Environment.NewLine +
                        "AND SPRIDEN_ID IN (" + Environment.NewLine;
                    /*foreach (string id in StudentIds)
                    {
                        sql = sql + "'" + id + "'," + Environment.NewLine;
                    }*/
                    for (int i =0; i<StudentIds.Count; i++)
                    {
                        if (i==(StudentIds.Count-1))
                        {
                            sql = sql + "'" + StudentIds[i] + "'" + Environment.NewLine;
                        }
                        else
                        {
                            sql = sql + "'" + StudentIds[i] + "'," + Environment.NewLine;
                        }
                    }
                    sql = sql.TrimEnd(',');
                    sql = sql + ");";
                    richTextBox1.Text = sql;
                       

                }
            }

        }
    }
}
