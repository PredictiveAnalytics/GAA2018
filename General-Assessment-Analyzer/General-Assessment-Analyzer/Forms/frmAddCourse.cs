using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace General_Assessment_Analyzer.Forms
{
    public partial class frmAddCourse : Form
    {
        public string courseKey { get; set; }
        public frmAddCourse()
        {
            InitializeComponent();
        }

        private void frmAddCourse_Load(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCourseKey.Text))
            {
                this.courseKey = tbCourseKey.Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
