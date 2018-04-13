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
    public partial class frmAddAssessmentType : Form
    {
        public string returnValue { get; set; }
        public frmAddAssessmentType()
        {
            InitializeComponent();
        }

        private void frmAddAssessmentType_Load(object sender, EventArgs e)
        {

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (tb_Assessments.Text.Trim() != string.Empty)
            {
                returnValue = tb_Assessments.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
