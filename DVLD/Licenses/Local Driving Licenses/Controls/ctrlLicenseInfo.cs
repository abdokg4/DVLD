using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DVLD_Business;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class ctrlLicenseInfo : UserControl
    {
        private clsLicense _License = new clsLicense();
        private int _LicenseID;

        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicense
        {
            get { return _License; }
        }
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }

        //private void _ResetInfo()
        //{
        //    lblClass.Text = "????";
        //    lblName.Text = "???";
        //    lblLicenseID.Text = "???";
        //    lblNationalNo.Text = "???";
        //    lblGender.Text = "???"; 
        //    lblIssueDate.Text = "???";
        //    lblIssueReason.Text = "???";
        //    lblNotes.Text = "???";
        //    lblIsActive.Text = "???";
        //    lblDateOfBirth.Text = "???";
        //    lblExpirationDate.Text = "???";
        //    lblDriverID.Text = "???";
        //    lblIsDetained.Text = "???";
        //}

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
            _License = clsLicense.FindLicenseByLicenseID(LicenseID);
            _LicenseID = LicenseID;

            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblClass.Text = _License.LicenseClassInfo.ClassName;
            lblName.Text = _License.DriverInfo.PersonInfo.FullName();
            lblLicenseID.Text = _LicenseID.ToString();
            lblNationalNo.Text = _License.DriverInfo.PersonInfo.NationalNo;
            lblGender.Text = (_License.DriverInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            lblIssueReason.Text = _License.IssueReasonText;
            lblIssueDate.Text = clsFormat.DateToShort(_License.IssueDate);
            lblNotes.Text = _License.Notes;
            lblIsActive.Text = (_License.IsActive) ? "Yes" : "No";
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_License.ExpirationDate);
            lblDateOfBirth.Text = clsFormat.DateToShort(_License.DriverInfo.PersonInfo.DateOfBirth);
            lblIsDetained.Text = (_License.IsDetained) ? "Yes" : "No";
            _LoadPersonImage();
        }
        private void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
