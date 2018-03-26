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
                if (!string.IsNullOrEmpty(ce.Assessment))
                {
                    lbAssessments.Items.Add(ce.Assessment);
                }
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

        private void btn_AddAssessment_Click(object sender, EventArgs e)
        {
            if (lbEntries.SelectedItems.Count > 0)
            {
                List<string> included = new List<string>();
                string key = lbEntries.SelectedItem.ToString().Replace("-", string.Empty).Trim();
                string subject = lbEntries.SelectedItem.ToString();
                string course = lbEntries.SelectedItem.ToString();
                subject = subject.Substring(0, subject.IndexOf("-")).Trim();
                course = lbEntries.SelectedItem.ToString().Replace(subject, string.Empty).Replace("-",string.Empty).Trim();
                Debug.WriteLine("Subject:" + subject);                    
                Debug.WriteLine("Course: " + course);
                frmAddAssessmentType frm = new frmAddAssessmentType();
                DialogResult dr = frm.ShowDialog();
                string test = frm.returnValue;
                foreach (var item in lbAssessments.Items)
                {
                    //Debug.WriteLine(item);
                    included.Add(item.ToString());
                }
                if (!included.Contains(test))
                {
                    included.Add(test);
                }
                lbAssessments.Items.Clear();
                included.ForEach(x=>lbAssessments.Items.Add(x));
                catalog.Entries.RemoveAll(x => x.CourseKey == key);
                foreach (string i in included)
                {
                    CatalogEntry n = new CatalogEntry();
                    n.CourseKey = key;
                    n.Course = course;
                    n.Subject = subject;
                    n.Assessment = i;
                    catalog.Entries.Add(n);
                }
            }
        }

        private void lbAssessments_SelectedIndexChanged(object sender, EventArgs e)
        {
            string course = lbEntries.SelectedItem.ToString().Replace("-", string.Empty).Trim();
            string assessment = lbAssessments.SelectedItem.ToString();
            foreach (CatalogEntry ce in catalog.Entries)
            {
                if (ce.CourseKey == course && ce.Assessment == assessment)
                {
                    Debug.WriteLine("found " + assessment);
                }
            }
        }

        private void btn_DeleteAssessment_Click(object sender, EventArgs e)
        {
            string course = lbEntries.SelectedItem.ToString().Replace("-", string.Empty).Trim();
            string assessment = lbAssessments.SelectedItem.ToString();
            for (int i = 0; i < catalog.Entries.Count; i++)
            {
                if (catalog.Entries[i].CourseKey == course && catalog.Entries[i].Assessment == assessment)
                {
                    Debug.WriteLine("found " + assessment);
                    catalog.Entries.RemoveAt(i);
                }
            }
            lbAssessments.Items.Clear();
            List<CatalogEntry> assessments = catalog.Entries.Where(x => x.CourseKey == course).ToList();
            foreach (CatalogEntry ce in assessments)
            {
                lbAssessments.Items.Add(ce.Assessment);
            }
            /*
             * List<CatalogEntry> Assessments = new List<CatalogEntry>();
            string key = lbEntries.GetItemText(lbEntries.SelectedItem).Replace("-",string.Empty).Trim();
            Assessments = catalog.Entries.Where(x => x.CourseKey == key).ToList();
            foreach (CatalogEntry ce in Assessments)
            {
                lbAssessments.Items.Add(ce.Assessment);
            }*/

        }

        private void btn_AddCourse_Click(object sender, EventArgs e)
        {
            List<string> Courses = new List<string>();

            string key = "" ;
            string subject = "";
            string course = ""; 
            //string subject = lbEntries.SelectedItem.ToString();
            //string course = lbEntries.SelectedItem.ToString();
            //subject = subject.Substring(0, subject.IndexOf("-")).Trim();
            //course = lbEntries.SelectedItem.ToString().Replace(subject, string.Empty).Replace("-", string.Empty).Trim();
            frmAddCourse frm = new frmAddCourse();
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                key = frm.courseKey;
                if (!key.Contains("-"))
                {
                    MessageBox.Show(
                        "CourseKey missformatted.  Please include both subject and number with a '-' between them.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                subject = key;
                course = key;
                subject = subject.Substring(0, subject.IndexOf("-")).Trim();
                course = course.Replace(subject, string.Empty).Replace("-", string.Empty).Trim();
            }

            foreach (var item in lbEntries.Items)
            {
                Courses.Add(item.ToString());
            }
            if (!string.IsNullOrEmpty(key))
            {
                if (!Courses.Contains(key))
                {
                    Debug.WriteLine("Added: " + key);
                    lbEntries.Items.Add(key);
                    CatalogEntry ce = new CatalogEntry();
                    ce.CourseKey = key.Replace("-",string.Empty);
                    ce.Course = course;
                    ce.Subject = subject;
                    //ce.Assessment = "";
                    catalog.Entries.Add(ce);
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 * string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string filepath = Path.Combine(path, "Resources");
            string file = Path.Combine(filepath, "CatalogFormatted.xml");
            if (!File.Exists(file))
            {
                File.WriteAllBytes(file, Encoding.ASCII.GetBytes(Resources.CatalogFormatted));
            }*/
                string path = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                string filepath = Path.Combine(path, "Resources");
                string file = Path.Combine(filepath, "CatalogFormatted.xml");

                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(Catalog));
                using (var writer = new StreamWriter(file))
                {
                    serializer.Serialize(writer, catalog);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving Catalog File: " + ex.ToString());
            }
        }
    }
}
