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
    public partial class ctrlScheduledTest : UserControl
    {
        private int _TestAppointmentID { get; set; }
        private clsTestAppointments TestAppointment { get; set; }

        private clsTestTypes.enTestTypes _TestTypeID = clsTestTypes.enTestTypes.VisionTest;

        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _TestID = -1;

        public int TestID
        {
            get { return _TestID; }
        }

        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }
        public clsTestTypes.enTestTypes TestType
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
        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            TestAppointment = clsTestAppointments.FindTestAppointment(_TestAppointmentID);
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLdlID(TestAppointment.LocalDrivingLicenseID);

            if (TestAppointment == null)
            {
                MessageBox.Show("Error In Finding Test Appointment With Number " + TestAppointmentID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestID = TestAppointment.TestID;
            _TestTypeID = TestAppointment.TestTypeID;

            if(_TestID != -1)
            {
                lblTestID.Text = _TestID.ToString();
            }
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLicenseClass.FindByID(_LocalDrivingLicenseApplication.LicenseClassID).ClassName;

            lblFullName.Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(TestType).ToString();
            dtpTestDate.Value = TestAppointment.AppointmentDate;
            lblFees.Text = TestAppointment.PaidFees.ToString();
        }
        private void ctrlScheduledTest_Load(object sender, EventArgs e)
        {
            
        }
    }
}
