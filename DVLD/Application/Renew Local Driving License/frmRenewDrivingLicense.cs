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
    public partial class frmRenewDrivingLicense : Form
    {
        int NewLicenseID = -1;
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }

        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees.ToString();
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

            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseClassInfo.ClassFees.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(ctrlLicenseInfoWithFilter1.SelectedLicense.LicenseClassInfo.DefaultValidityLength));
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();

            if (!ctrlLicenseInfoWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                MessageBox.Show("Selected License Is Not Yet Expired, It Will Expire On " + clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.SelectedLicense.ExpirationDate), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }
            
            if(!ctrlLicenseInfoWithFilter1.SelectedLicense.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, Please Choose An Active License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Renew The License ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrlLicenseInfoWithFilter1.SelectedLicense.RenewDrivingLicense(txtNotes.Text.Trim(), clsGlobalUser.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Error In Renewing The Driving License", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("License Renewed Successfully With ID = " + NewLicense.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NewLicenseID = NewLicense.LicenseID;

            ctrlLicenseInfoWithFilter1.DisableFilter();
            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicenseID.Text = NewLicenseID.ToString();
            llShowLicenseInfo.Enabled = true;
            btnRenewLicense.Enabled = false;
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseInfo(NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseHistory(ctrlLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }
    }
}
