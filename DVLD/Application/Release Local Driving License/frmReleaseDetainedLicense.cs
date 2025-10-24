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
    public partial class frmReleaseDetainedLicense : Form
    {
        private int _SelectedLicenseID = -1;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID=LicenseID;
            ctrlLicenseInfoWithFilter1.Find(_SelectedLicenseID);
            ctrlLicenseInfoWithFilter1.DisableFilter();
        }
        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.SelectedLicense.IsDetained)
            {
                MessageBox.Show("Selected License Is Not Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            if (ctrlLicenseInfoWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                MessageBox.Show("Selected License Is Expired On " + clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.SelectedLicense.ExpirationDate), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            if (!ctrlLicenseInfoWithFilter1.SelectedLicense.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, Please Choose An Active License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                return;
            }

            btnRelease.Enabled = true;
            lblDetainID.Text = ctrlLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.DetainDate);
            lblCreatedByUser.Text = ctrlLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.CreatedByUserID.ToString();
            lblFineFees.Text = Convert.ToSingle(ctrlLicenseInfoWithFilter1.SelectedLicense.DetainedInfo.FineFees).ToString();
            lblApplicationFees.Text = Convert.ToSingle(clsApplicationTypes.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees).ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Detain The License ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;

            if(!ctrlLicenseInfoWithFilter1.SelectedLicense.ReleaseDetainedLicense(clsGlobalUser.CurrentUser.UserID,ref ApplicationID))
            {
                return;
            }
            MessageBox.Show("License Has Been Released Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblApplicationID.Text = ApplicationID.ToString();
            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            ctrlLicenseInfoWithFilter1.DisableFilter();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseHistory(ctrlLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseInfo(ctrlLicenseInfoWithFilter1.LicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
