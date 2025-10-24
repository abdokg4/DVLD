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
    public partial class frmAddLocalDrivingLicenseApplication : Form
    {
         enum enMode { AddNew = 1, Update = 2};
        private enMode _Mode = enMode.AddNew;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApp;
        private int _LocalDrivingLicenseAppID;

        public frmAddLocalDrivingLicenseApplication(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseAppID = LocalDrivingLicenseID;

            if(LocalDrivingLicenseID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;   
        }

        private void _LoadInfo()
        {
            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApp = new clsLocalDrivingLicenseApplication();
                return;
            }

            _LocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.FindByLdlID(_LocalDrivingLicenseAppID);
            if (_LocalDrivingLicenseApp == null)
            {
                MessageBox.Show("This form will be closed because No Local Driving License Application with ID = " + _LocalDrivingLicenseApp);
                this.Close();

                return;
            }
            lblMode.Text = "Update Local Driving License Application";
            lblAppID.Text = _LocalDrivingLicenseApp.LocalDrivingLicenseApplicationID.ToString();
            lblAppDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApp.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClass.FindByID(_LocalDrivingLicenseApp.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseApp.PaidFees.ToString();
            lblCreatedByUser.Text = clsUser.Find(_LocalDrivingLicenseApp.CreatedByUserID).UserName;
            ctrlPersonCardWithFilter1.DisableFilter();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApp.ApplicantPersonID);

        }

        private void _ResetInfo()
        {
            _FillClassesInComboBox();
            
            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "New Local Driving License Application";
                _LocalDrivingLicenseApp = new clsLocalDrivingLicenseApplication();
                tabAppInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblMode.Text = "Update Local Driving License Application";
                
                tabAppInfo.Enabled = true;
                btnSave.Enabled = true;
            }
                lblAppID.Text = "[???]";
            lblAppDate.Text = clsFormat.DateToShort(DateTime.Now);
            cbLicenseClass.SelectedIndex = 2;
            lblFees.Text = "15";
            lblCreatedByUser.Text = clsGlobalUser.CurrentUser.UserName;

            btnSave.Enabled = false;
        }
        private void _FillClassesInComboBox()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow dr in dt.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"]);
            }
        }
        private void frmAddLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetInfo();

            if (_Mode == enMode.Update)
            {
                _LoadInfo();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please Select A Person!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            tabControl1.SelectTab(1);
            btnSave.Enabled = true ;
            tabAppInfo.Enabled = true ;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _LocalDrivingLicenseApp.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApp.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApp.LicenseClassID = clsLicenseClass.FindByName(cbLicenseClass.Text).LicenseClassID;
            _LocalDrivingLicenseApp.PaidFees = Convert.ToDecimal(lblFees.Text);
            _LocalDrivingLicenseApp.ApplicationTypeID = 1;
            _LocalDrivingLicenseApp.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LocalDrivingLicenseApp.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApp.CreatedByUserID = clsGlobalUser.CurrentUser.UserID;

            int AppID = clsApplication.ApplicationExistWithSameClassID(_LocalDrivingLicenseApp.ApplicantPersonID, _LocalDrivingLicenseApp.ApplicationTypeID, _LocalDrivingLicenseApp.LicenseClassID);

            if (AppID != -1)
            {
                MessageBox.Show("Person Already Has A New Application With The Same Class With ID = " + AppID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int LicenseID = clsLicense.GetActiveLicenseIDByPersonID(_LocalDrivingLicenseApp.ApplicantPersonID, _LocalDrivingLicenseApp.LicenseClassID);

            if (LicenseID != -1)
            {
                MessageBox.Show("Person Already Has A License With The Same Class With ID = " + LicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (_LocalDrivingLicenseApp.Save())
            {
                MessageBox.Show("Data Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMode.Text = "Update Local Driving License Application";
                lblAppID.Text = _LocalDrivingLicenseApp.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;
            }
            else
            {
                MessageBox.Show("Data WASN'T Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
