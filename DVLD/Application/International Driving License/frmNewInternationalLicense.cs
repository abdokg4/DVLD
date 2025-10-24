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
    public partial class frmNewInternationalLicense : Form
    {
        public frmNewInternationalLicense()
        {
            InitializeComponent();
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            if(obj == -1)
            {
                return;
            }
            lblLocalLicenseID.Text = obj.ToString();
            clsLicense license = clsLicense.FindLicenseByLicenseID(obj);
            int ActiveLicenseID = clsInternationalDrivingLicense.GetActiveInternationalLicenseIDByPersonID(license.DriverInfo.PersonID);

            if(ctrlLicenseInfoWithFilter1.SelectedLicense.IsLicenseExpired())
            {
                
                    MessageBox.Show("International License Cant Be Issued With An Expired Local License, Please Choose Another License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                
            }

            if (ActiveLicenseID != -1)
            {
                MessageBox.Show("Person Already Has An International License With ID = " + ActiveLicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseHistory.Enabled = true;
                llShowLicenseInfo.Enabled = true;
                return;
            }

            if (license.LicenseClassID != 3)
            {
                MessageBox.Show("International License Cant Be Issued To Any Class Other Than Class 3, Please Choose A Diffrent License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!license.IsActive)
            {
                MessageBox.Show("International License Cant Be Issued Without An Active Local License, Please Choose Another License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnIssue.Enabled = true;
            llShowLicenseHistory.Enabled = true ;
        }

        private void frmNewInternationalLicense_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedBy.Text = clsGlobalUser.CurrentUser.UserName;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowLicenseHistory(ctrlLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo(clsInternationalDrivingLicense.GetActiveInternationalLicenseIDByPersonID(ctrlLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID));
            frm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Issue This License ?","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                clsInternationalDrivingLicense internationalDrivingLicense = new clsInternationalDrivingLicense();

                internationalDrivingLicense.ApplicantPersonID = ctrlLicenseInfoWithFilter1.SelectedLicense.DriverInfo.PersonID;
                internationalDrivingLicense.ApplicationDate = DateTime.Now;
                internationalDrivingLicense.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
                internationalDrivingLicense.LastStatusDate = DateTime.Now;
                internationalDrivingLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                internationalDrivingLicense.PaidFees = clsApplicationTypes.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees;
                internationalDrivingLicense.CreatedByUserID = clsGlobalUser.CurrentUser.UserID;

                internationalDrivingLicense.DriverID = ctrlLicenseInfoWithFilter1.SelectedLicense.DriverID;
                internationalDrivingLicense.IssuedUsingLocalDrivingLicenseID = ctrlLicenseInfoWithFilter1.LicenseID;
                internationalDrivingLicense.IssueDate = DateTime.Now;
                internationalDrivingLicense.ExpirationDate = DateTime.Now.AddYears(1);
                internationalDrivingLicense.IsActive = true;

                if(!internationalDrivingLicense.Save())
                {
                    MessageBox.Show("Error Issuing The License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("International License Issued Successfully With ID = " + internationalDrivingLicense.InternationalDrivingLicenseID, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblInternationalAppID.Text = internationalDrivingLicense.ApplicationID.ToString();
                lblInternationalLicenseID.Text = internationalDrivingLicense.InternationalDrivingLicenseID.ToString();
                llShowLicenseInfo.Enabled = true;
                btnIssue.Enabled = false;
                ctrlLicenseInfoWithFilter1.DisableFilter();
            }
        }
    }
}
