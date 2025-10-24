using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace WindowsFormsApp1
{
    public partial class frmDrivers : Form
    {
        DataTable _dtDrivers = clsDrivers.GetAllDrivers();

        public frmDrivers()
        {
            InitializeComponent();
        }

        private void frmDrivers_Load(object sender, EventArgs e)
        {
            _dtDrivers = clsDrivers.GetAllDrivers();
            dgvDrivers.DataSource = _dtDrivers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsNum.Text = dgvDrivers.Rows.Count.ToString();

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 130;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 130;
                   
                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 350;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 210;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 190;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterMode = "";
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterMode = "DriverID";
                    break;
                case "Person ID":
                    FilterMode = "PersonID";
                    break;
                case "National No.":
                    FilterMode = "NationalNo";
                    break;
                case "Full Name":
                    FilterMode = "FullName";
                    break;
                default:
                    FilterMode = "None";
                    break;
            }

            if (txtFilterBy.Text.Trim() == "" || FilterMode == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblRecordsNum.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if(FilterMode == "PersonID" || FilterMode == "DriverID")
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, txtFilterBy.Text);
            }
            else
            {
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterMode, txtFilterBy.Text);
            }
            lblRecordsNum.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo(Convert.ToInt32(dgvDrivers.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicenseHistory(Convert.ToInt32(dgvDrivers.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
            frmDrivers_Load(null, null);
        }
    }
}
