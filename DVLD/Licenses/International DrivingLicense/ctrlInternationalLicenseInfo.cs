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
using System.IO;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private int _LicenseID;
        private clsInternationalDrivingLicense _License = new clsInternationalDrivingLicense();

        public int LicenseID
        {
            get { return _LicenseID; }
        }



        public clsInternationalDrivingLicense SelectedLicense
            { get { return _License; } }

        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public void LoadInfo(int LicenseID)
        {
            
            _License = clsInternationalDrivingLicense.FindByInternationalDrivingLicenseID(LicenseID);

            if (_License == null)
            {
                MessageBox.Show("Error Finding International License With ID = " + LicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillInfo();
        }

        private void _FillInfo()
        {
            _LicenseID = _License.InternationalDrivingLicenseID;

            lblFullName.Text = _License.ApplicantFullName;
            lblInternationalLicenseID.Text = _LicenseID.ToString();
            lblLocalLicenseID.Text = _License.IssuedUsingLocalDrivingLicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = (_License.DriverInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblApplicationID.Text = _License.ApplicationID.ToString();
            lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            _LoadPersonImage();
        }

        private void ctrlInternationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
