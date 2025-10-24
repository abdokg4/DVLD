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
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class ctrlScheduleTest : UserControl
    {
        private enum enMode { AddNew = 1, Update = 2 }
        enMode Mode = enMode.AddNew;

        private enum enTestMode { NewTestAppointment = 1, RetakeTestAppointment = 2 }
        enTestMode TestMode = enTestMode.NewTestAppointment;
        private DateTime MinAppointmentDate {  get; set; }
        private int _TestAppointmentID {  get; set; }
        private clsTestAppointments TestAppointment { get; set; }
        private clsTestTypes.enTestTypes _TestTypeID = clsTestTypes.enTestTypes.VisionTest;

        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public clsTestTypes.enTestTypes TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestTypes.enTestTypes.VisionTest:
                        pbTestPic.Image = Resources.Vision_512;
                        gbTestType.Text = "Vision Test";
                        break;
                    case clsTestTypes.enTestTypes.WrittenTest:
                        pbTestPic.Image = Resources.Written_Test_512;
                        gbTestType.Text = "Written Test";
                        break;
                    case clsTestTypes.enTestTypes.StreetTest:
                        pbTestPic.Image = Resources.driving_test_512;
                        gbTestType.Text = "Driving Test";
                        break;
                    default:
                        break;
                }
            }
        }
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        public void LoadAppointmentInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID)
        {
           
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;

             _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLdlID(_LocalDrivingLicenseApplicationID);
            if(TestAppointmentID != -1)
            {
               TestAppointment = clsTestAppointments.FindTestAppointment(TestAppointmentID);
                Mode = enMode.Update;
            }
            else
            {
                TestAppointment = new clsTestAppointments();
                Mode = enMode.AddNew;

            }

            if(_LocalDrivingLicenseApplication ==  null || TestAppointment == null)
            {
                _ResetInfo();
                MessageBox.Show("Error");
                return;
            }

           

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(TestTypeID))
            {
                TestMode = enTestMode.RetakeTestAppointment;
            }
            else
            {
                TestMode = enTestMode.NewTestAppointment;
            }

            if(TestMode == enTestMode.RetakeTestAppointment)
            {
                
                lblTest.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                lblRetakeAppFees.Text = Convert.ToSingle(clsApplicationTypes.Find(7).ApplicationFees).ToString();
                
            }
            else
            {
                lblTest.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
                lblRetakeAppFees.Text = "0";
            }

            MinAppointmentDate = clsTestAppointments.GetMinimumAppointmentDate(LocalDrivingLicenseApplicationID, TestTypeID);
            if (DateTime.Compare(DateTime.Now, MinAppointmentDate) > 0)
            {
                MinAppointmentDate = DateTime.Now;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLicenseClass.FindByID(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            if (Mode == enMode.AddNew)
            {
            dtpTestDate.MinDate = MinAppointmentDate;
            lblFees.Text = Convert.ToSingle(clsTestTypes.Find(_TestTypeID).TestFees).ToString();
            
            TestAppointment = new clsTestAppointments();
            }

            else
            {
                _FillAppointmentInfo();
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeAppFees.Text)).ToString();

            if (!HandleDoesHaveActiveAppointment())
                return;

            if (!_HandleDidTakeTest())
                return;

            if (!_HandleDidPassPreviousTestType())
                return;
        }

        private void _FillAppointmentInfo()
        {
            

            
            lblFees.Text = TestAppointment.PaidFees.ToString();
            gbRetakeTestInfo.Enabled = (_LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID) > 0);
            
            TestMode = (gbRetakeTestInfo.Enabled ? enTestMode.RetakeTestAppointment : enTestMode.NewTestAppointment);
           

            if(TestMode == enTestMode.RetakeTestAppointment)
            {
                
                
                if (TestAppointment.RetakeTestApplicationID != -1)
                {
                    lblRetakeTestAppID.Text = TestAppointment.RetakeTestApplicationID.ToString();
                }

                lblRetakeAppFees.Text = clsApplicationTypes.Find(7).ApplicationFees.ToString();

            }
            //dtpTestDate.MinDate = DateTime.MinValue;
            dtpTestDate.Value = TestAppointment.AppointmentDate;
            
            
           
        }

        private bool _HandleDidPassPreviousTestType()
        {
            switch (_TestTypeID)
            {
                case clsTestTypes.enTestTypes.VisionTest:
                    return true;
                case clsTestTypes.enTestTypes.WrittenTest:
                   if(!_LocalDrivingLicenseApplication.DidPassTestType(clsTestTypes.enTestTypes.VisionTest))
                    {
                        lblUserMessage.Visible = true;
                        lblUserMessage.Text = "Cannot Sechule, Vision Test Should be Passed First.";
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;

                        return false;
                    }
                 break;
                   
                case clsTestTypes.enTestTypes.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DidPassTestType(clsTestTypes.enTestTypes.WrittenTest))
                    {
                        lblUserMessage.Visible = true;
                        lblUserMessage.Text = "Cannot Sechule, Written Test Should be Passed First.";
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;

                        return false;
                    }
                    return true;

            }
            return true;
        }

        private bool _HandleRetakeTest()
        {
            if(Mode == enMode.AddNew && TestMode == enTestMode.RetakeTestAppointment)
            {
                clsApplication application = new clsApplication();

                application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                application.ApplicationDate = dtpTestDate.Value;
                application.ApplicationStatus = clsApplication.enApplicationStatus.New;
                application.ApplicationTypeID = clsApplicationTypes.Find(7).ApplicationTypeID;
                application.LastStatusDate = dtpTestDate.Value;
                application.PaidFees = clsApplicationTypes.Find(7).ApplicationFees;
                application.CreatedByUserID = clsGlobalUser.CurrentUser.UserID;

                if(!application.Save())
                {
                    TestAppointment.TestAppointmentID = -1;
                    MessageBox.Show("The Retake Test Application WASN'T Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                TestAppointment.RetakeTestApplicationID = application.ApplicationID;
            }
            return true;
        }

        private bool HandleDoesHaveActiveAppointment()
        {
            if(_LocalDrivingLicenseApplication.DoesHaveActiveApplicationWithTestType(TestTypeID) && TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person Already Have Active Appointment For This Test !";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;

                return false;
            }
            return true;
        }

        private bool _HandleDidTakeTest()
        {
            if(TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person Already Took Test, Can't Modify Appointment.";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;

                return false;
            }
            return true;
        }
        private void _ResetInfo()
        {
            lblLocalDrivingLicenseAppID.Text = "??";
            lblDrivingClass.Text = "???????";
            lblFullName.Text = "??????";
            lblTrial.Text = "??????";
            dtpTestDate.Value = DateTime.Now;
            lblFees.Text = "???";
            gbRetakeTestInfo.Enabled = false;
            lblRetakeAppFees.Text = "???";
            lblTotalFees.Text = "???";
            lblRetakeTestAppID.Text = "????";
        }
        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeTest())
            {
                return;
            }

            TestAppointment.TestTypeID = TestTypeID;
            TestAppointment.LocalDrivingLicenseID = _LocalDrivingLicenseApplicationID;
            TestAppointment.AppointmentDate = dtpTestDate.Value;
            TestAppointment.PaidFees = clsTestTypes.Find(_TestTypeID).TestFees;
            TestAppointment.CreatedByUserID = clsGlobalUser.CurrentUser.UserID;
            TestAppointment.IsLocked = false;

            

            if (TestAppointment.Save())
            {
                Mode = enMode.Update;
                MessageBox.Show("Appointment Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Appointment WASN'T Saved Successfull.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }
    }
}
