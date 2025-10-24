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
    public partial class frmListInternationalLicensesApplications : Form
    {
        DataTable dtInternationalApplications;
        public frmListInternationalLicensesApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicensesApplications_Load(object sender, EventArgs e)
        {
            dtInternationalApplications = clsInternationalDrivingLicense.GetAllInternationalDrivingLicenses();
            dgvInternationalLicenseApplications.DataSource = dtInternationalApplications;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();

            if (dgvInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvInternationalLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseApplications.Columns[0].Width = 180;

                dgvInternationalLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseApplications.Columns[1].Width = 150;

                dgvInternationalLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseApplications.Columns[2].Width = 150;

                dgvInternationalLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseApplications.Columns[3].Width = 150;

                dgvInternationalLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseApplications.Columns[4].Width = 250;

                dgvInternationalLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenseApplications.Columns[5].Width = 250;

                dgvInternationalLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseApplications.Columns[6].Width = 150;
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
            {
                cbIsActive.Visible = true;
                txtFilterValue.Visible = false;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = (cbFilterBy.Text != "None");

                if (txtFilterValue.Visible)
                {
                    cbFilterBy.Text = "";
                    txtFilterValue.Focus();
                    cbIsActive.Visible = false;
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterMode = "";
           
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterMode = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterMode = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterMode = "DriverID";
                    break;
                case "Local License ID":
                    FilterMode = "IssuedUsingLocalLicenseID";
                    break;
                default:
                    FilterMode = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterMode == "None")
            {
                dtInternationalApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
                return;
            }
            else
            {
                dtInternationalApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, txtFilterValue.Text);
                lblRecordsCount.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
            }


        }


        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterMode = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
                default:
                    FilterValue = "";
                    break;
            }
            if (FilterValue == "All")
            {
                dtInternationalApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
                return;
            }
            else
            {
                dtInternationalApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, FilterValue);
                lblRecordsCount.Text = dgvInternationalLicenseApplications.Rows.Count.ToString();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmNewInternationalLicense();
            frm.ShowDialog();
            frmListInternationalLicensesApplications_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalDrivingLicense License = clsInternationalDrivingLicense.FindByInternationalDrivingLicenseID(Convert.ToInt32(dgvInternationalLicenseApplications.CurrentRow.Cells[0].Value));
            Form frm = new frmShowPersonInfo(License.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo(Convert.ToInt32(dgvInternationalLicenseApplications.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsInternationalDrivingLicense License = clsInternationalDrivingLicense.FindByInternationalDrivingLicenseID(Convert.ToInt32(dgvInternationalLicenseApplications.CurrentRow.Cells[0].Value));
            Form frm = new frmShowLicenseHistory(License.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
