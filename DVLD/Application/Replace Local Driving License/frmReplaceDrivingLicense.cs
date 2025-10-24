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
    public partial class frmReplaceDrivingLicense : Form
    {
        int NewLicenseID = 0;
        clsLicense.enIssueReasons IssueReason;
        public frmReplaceDrivingLicense()
        {
            InitializeComponent();
        }

        private void frmReplaceDrivingLicense_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobalUser.CurrentUser.UserName;
            
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int LicenseID = obj;

            lblOldLicenseID.Text = LicenseID.ToString();
            llShowLicenseHistory.Enabled = (LicenseID != -1);

            if (LicenseID == -1)
            {
                return;
            }

            if (ctrlLicenseInfoWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                MessageBox.Show("Selected License Is Expired, Please Choose Another License.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.SelectedLicense.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, Please Choose An Active License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbDamagedLicense.Checked)
            {
                return;
            }
            lblTitle.Text = "Replacement For Damaged License";
            lblApplicationFees.Text = Convert.ToSingle(clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationFees).ToString();
            IssueReason = clsLicense.enIssueReasons.ReplaceDamaged;

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbLostLicense.Checked)
            {
                return;
            }
            lblTitle.Text = "Replacement For Lost License";
            lblApplicationFees.Text = Convert.ToSingle(clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).ApplicationFees).ToString();
            IssueReason = clsLicense.enIssueReasons.ReplaceLost;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Replace The License ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrlLicenseInfoWithFilter1.SelectedLicense.Replace(IssueReason, clsGlobalUser.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Error In Replacing The Driving License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License Replaced Successfully With ID = " + NewLicense.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NewLicenseID = NewLicense.LicenseID;


            ctrlLicenseInfoWithFilter1.DisableFilter();
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblRreplacedLicenseID.Text = NewLicenseID.ToString();
            btnIssueReplacement.Enabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseHistory(ctrlLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseInfo(NewLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
