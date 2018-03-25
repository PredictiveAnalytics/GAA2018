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
using General_Assessment_Analyzer.Properties;
using General_Assessment_Analyzer.Classes;
using System.Xml.Serialization;
using System.Diagnostics;

namespace General_Assessment_Analyzer.Forms
{
    public partial class frmCatalog : Form
    {
        Catalog catalog;
        List<string> Courses;

        public frmCatalog()
        {
            InitializeComponent();
            Courses = new List<string>();
        }

        private void frmCatalog_Load(object sender, EventArgs e)
        {
            //if the catalog file doesn't exist, load the default one from project resources and save it.


            string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string filepath = Path.Combine(path, "Resources");
            string file = Path.Combine(filepath, "CatalogFormatted.xml");
            if (!File.Exists(file))
            {
                File.WriteAllBytes(file, Encoding.ASCII.GetBytes(Resources.CatalogFormatted));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
            TextReader reader = new StreamReader(file);
            object obj = serializer.Deserialize(reader);
            catalog = (Catalog)obj;
            reader.Close();
            
            if (catalog.Entries.Count>0)
            {
                Courses = catalog.Entries.Select(x => x.Subject + "-" + x.Course).Distinct().ToList();
                Courses.Sort();
                Courses.ForEach(x => lbEntries.Items.Add(x));
            }
        }

        private void lbEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbAssessments.Items.Count>0)
            {
                lbAssessments.Items.Clear();
            }
            List<CatalogEntry> Assessments = new List<CatalogEntry>();
            string key = lbEntries.GetItemText(lbEntries.SelectedItem).Replace("-",string.Empty).Trim();
            Assessments = catalog.Entries.Where(x => x.CourseKey == key).ToList();
            foreach (CatalogEntry ce in Assessments)
            {
                lbAssessments.Items.Add(ce.Assessment);
            }
            

        }

        private void btn_DeleteCourse_Click(object sender, EventArgs e)
        {
            if(lbEntries.SelectedItems.Count>0)
            {
                string course = lbEntries.SelectedItem.ToString();
                string key = course.Replace("-", string.Empty).Trim();
                DialogResult dr = MessageBox.Show("You are about to delete this course.  Assessments in this course will still be included in the the Assessment Reports, but not tracked in the Completions report.", "Continue deletion of " + course + "?",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Debug.WriteLine("Entries before Delete: " + catalog.Entries.Count());
                    catalog.Entries.RemoveAll(x => x.CourseKey == key);
                    Debug.WriteLine("Entries after Delete: " + catalog.Entries.Count());
                    lbEntries.Items.Clear();
                    Courses = catalog.Entries.Select(x => x.Subject + "-" + x.Course).Distinct().ToList();
                    Courses.ForEach(x => lbEntries.Items.Add(x));
                    /*
                     * Courses = catalog.Entries.Select(x => x.Subject + "-" + x.Course).Distinct().ToList();
                       Courses.Sort();
                       Courses.ForEach(x => lbEntries.Items.Add(x));
                     */
                }
            }
        }

        private void btn_DeleteAssessment_Click(object sender, EventArgs e)
        {

        }

        private void btn_AddAssessment_Click(object sender, EventArgs e)
        {
            if (lbEntries.SelectedItems.Count > 0)
            {
                List<string> included = new List<string>();
                frmAddAssessmentType frm = new frmAddAssessmentType();
                DialogResult dr = frm.ShowDialog();
                string test = frm.returnValue;
                if (dr == DialogResult.OK)
                {
                    Debug.WriteLine(test);
                    lbAssessments.Items.Add(test);
                }
                foreach (var item in lbAssessments.Items)
                {
                    Debug.WriteLine(item);
                    included.Add(item.ToString());
                }
            }
        }
    }
}
