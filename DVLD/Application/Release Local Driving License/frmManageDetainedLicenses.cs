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
    public partial class frmManageDetainedLicenses : Form
    {
        DataTable dtDetainedLicenses;
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            dtDetainedLicenses = clsDetainedLicenses.GetAllDetainedLicenses();
            dgvDetainedLicenses.DataSource = dtDetainedLicenses;
            cbFilterBy.SelectedIndex = 0;

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 100;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 100;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 110;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 100;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 335;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App ID";
                dgvDetainedLicenses.Columns[8].Width = 150;
            }
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Released")
            {
                cbIsReleased.Visible = true;
                cbIsReleased.SelectedIndex = 0;
                txtFilterValue.Visible = false;
                cbIsReleased.Focus();
            }

            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (txtFilterValue.Visible)
                {
                    txtFilterValue.Text = "";
                    txtFilterValue.Focus();
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
         
            string FilterMode = "";

            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterMode = "DetainID";
                    break;
                case "National No.":
                    FilterMode = "NationalNo";
                    break;
                case "Full Name":
                    FilterMode = "FullName";
                    break;
                case "Release Application ID":
                    FilterMode = "ReleaseApplicationID";
                    break;
                default:
                    FilterMode = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterMode == "None")
            {
                dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }

            if(FilterMode == "DetainID" || FilterMode == "ReleaseApplicationID")
            {
                dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, txtFilterValue.Text);
            }
            else
            {
                dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterMode, txtFilterValue.Text);
            }
            lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterMode = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (cbIsReleased.Text)
            {
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
                default:
                    FilterValue = "All";
                    break;
            }
            if(FilterValue == "All")
            {
                dtDetainedLicenses.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
                return;
            }
            else
            {
                dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, FilterValue);
                lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicense(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void cmsApplications_Opened(object sender, EventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = clsDetainedLicenses.IsLicenseDetained(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            int PersonID = clsLicense.FindLicenseByLicenseID(LicenseID).DriverInfo.PersonID;

            Form frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicenseInfo(Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = Convert.ToInt32(dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            int PersonID = clsLicense.FindLicenseByLicenseID(LicenseID).DriverInfo.PersonID;

            Form frm = new frmShowLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicense();
            frm.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
            frmManageDetainedLicenses_Load(null, null);
        }
    }
}
