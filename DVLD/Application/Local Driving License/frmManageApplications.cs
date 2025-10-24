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
    public partial class frmManageApplications : Form
    {
        DataTable _dt;
        public frmManageApplications()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _dt = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplications.DataSource = _dt;

            lblRecordsCount.Text = _dt.Rows.Count.ToString();
        }
        private void frmManageApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _LoadData();

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 270;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 340;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 100;
            }

            
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddLocalDrivingLicenseApplication(-1);
            frm.ShowDialog();
            _RefreshData();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }

        }

        private void _RefreshData()
        {
            DataTable dt = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplications.DataSource = dt;

            lblRecordsCount.Text = _dt.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterMode = "";

            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterMode = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    FilterMode = "NationalNo";
                    break;
                case "Status":
                    FilterMode = "Status";
                    break;
                case "Full Name":
                    FilterMode = "FullName";
                    break;
                default:
                    FilterMode = "None";
                    break;
            }
            if(txtFilterValue.Text.Trim() == "" || FilterMode == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if(FilterMode == "LocalDrivingLicenseApplicationID")
            {
                _dt.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, txtFilterValue.Text.Trim());
            }
            else
            {
                _dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterMode, txtFilterValue.Text.Trim());
            }
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowApplicationInfo(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddLocalDrivingLicenseApplication(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshData();
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This Application ?", "Delete Aplication", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Application Deleted Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshData();
                }
                else
                {
                    MessageBox.Show("Error Deleting The Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }
           
        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LdlID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            clsApplication application = clsApplication.Find(clsLocalDrivingLicenseApplication.FindByLdlID(LdlID).ApplicationID);

            if (MessageBox.Show("Are You Sure You Want To Cancel This Application ?", "Cancel Aplication", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (application.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshData();
                }
                else
                {
                    MessageBox.Show("Error Cancelling The Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }
        }

        private void cmsApplications_Opened(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            clsLocalDrivingLicenseApplication _LocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.FindByLdlID(LocalDrivingLicenseAppID);
            int PassedTests = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value);
            bool isLicenseExist = clsLicense.IsLicenseExistByPersonID(_LocalDrivingLicenseApp.ApplicantPersonID, _LocalDrivingLicenseApp.LicenseClassID);




            editToolStripMenuItem.Enabled = (_LocalDrivingLicenseApp.ApplicationStatus == clsApplication.enApplicationStatus.New);
            DeleteApplicationToolStripMenuItem.Enabled = (_LocalDrivingLicenseApp.ApplicationStatus == clsApplication.enApplicationStatus.New);
            CancelApplicaitonToolStripMenuItem.Enabled = (_LocalDrivingLicenseApp.ApplicationStatus == clsApplication.enApplicationStatus.New);
            ScheduleTestsMenu.Enabled = (_LocalDrivingLicenseApp.ApplicationStatus == clsApplication.enApplicationStatus.New) && (PassedTests != 3);
            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = PassedTests == 3 && !isLicenseExist;

            showLicenseToolStripMenuItem.Enabled = isLicenseExist;

          

            bool DoesPassVisionTest = _LocalDrivingLicenseApp.DidPassTestType(clsTestTypes.enTestTypes.VisionTest);
            bool DoesPassWrittenTest = _LocalDrivingLicenseApp.DidPassTestType(clsTestTypes.enTestTypes.WrittenTest);
            bool DoesPassStreetTest = _LocalDrivingLicenseApp.DidPassTestType(clsTestTypes.enTestTypes.StreetTest);

            if(ScheduleTestsMenu.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !DoesPassVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = !DoesPassWrittenTest && DoesPassVisionTest;
                scheduleStreetTestToolStripMenuItem.Enabled = !DoesPassStreetTest && DoesPassWrittenTest;

            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestAppointmentcs(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value), clsTestTypes.enTestTypes.VisionTest);
            frm.ShowDialog();
            _RefreshData();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ScheduleTestsMenue_Click(object sender, EventArgs e)
        {

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestAppointmentcs(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value), clsTestTypes.enTestTypes.WrittenTest);
            frm.ShowDialog();
            _RefreshData();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestAppointmentcs(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value), clsTestTypes.enTestTypes.StreetTest);
            frm.ShowDialog();
            _RefreshData();
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueDriverLicenseFirstTime(Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshData();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            clsLocalDrivingLicenseApplication _LocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.FindByLdlID(LocalDrivingLicenseAppID);

            Form frm = new frmShowLicenseInfo(clsLicense.GetActiveLicenseIDByPersonID(_LocalDrivingLicenseApp.ApplicantPersonID, _LocalDrivingLicenseApp.LicenseClassID));
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
            clsLocalDrivingLicenseApplication _LocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.FindByLdlID(LocalDrivingLicenseAppID);

            Form frm = new frmShowLicenseHistory(_LocalDrivingLicenseApp.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
