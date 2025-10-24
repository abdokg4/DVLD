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
    public partial class ctrlApplicationInfo : UserControl
    {
        private clsLocalDrivingLicenseApplication _LdlApplication;
        private int _LdlApplicationID;
        public int LdlApplicationID { get { return _LdlApplicationID; } }
        public ctrlApplicationInfo()
        {
            InitializeComponent();
        }

        private void _ResetInfo()
        {
            lblID.Text = "???";
            lblAppliedLicense.Text = "???";
            lblTests.Text = "???";
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID)
        {
            _LdlApplication = clsLocalDrivingLicenseApplication.FindByLdlID(LocalDrivingLicenseApplicationID);
            if (_LdlApplication == null)
            {
                _ResetInfo();
                MessageBox.Show("Error, No Application With ID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillInfo();
        }

        private void _FillInfo()
        {
            _LdlApplicationID = _LdlApplication.LocalDrivingLicenseApplicationID;
            lblID.Text = _LdlApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedLicense.Text = clsLicenseClass.FindByID(_LdlApplication.LicenseClassID).ClassName;
            lblTests.Text = _LdlApplication.GetPassedTests().ToString() + "/3";
            llShowLicense.Enabled = (clsLicense.GetActiveLicenseIDByPersonID(_LdlApplication.ApplicantPersonID, _LdlApplication.LicenseClassID) != -1);

            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LdlApplication.ApplicationID);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void llShowLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseInfo(clsLicense.GetActiveLicenseIDByPersonID(_LdlApplication.ApplicantPersonID, _LdlApplication.LicenseClassID));
            frm.ShowDialog();
        }
    }
}
