﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General_Assessment_Analyzer.Forms;


/// <summary>
/// Main Menu for GAA 2018.
/// </summary>
namespace General_Assessment_Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void prepareReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Quit the Application?", "Quit?", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void manageCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCatalog frm = new frmCatalog();
            frm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmCatalog frm = new frmCatalog();
            frm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmCOEReports frm = new frmCOEReports();
            frm.Show();
        }
    }
}
