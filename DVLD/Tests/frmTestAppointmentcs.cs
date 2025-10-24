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
    public partial class frmTestAppointmentcs : Form
    {
        private clsTestTypes.enTestTypes _TestType;
        private int _LocalDrivingLicenseAppID;
        private DataTable _dt;
        public frmTestAppointmentcs(int LocalDrivingLicenseAppID, clsTestTypes.enTestTypes TestType)
        {
            InitializeComponent();
            _TestType = TestType;
            _LocalDrivingLicenseAppID = LocalDrivingLicenseAppID;
        }

        private void _HandleImageAndLable()
        {
            switch (_TestType)
            {
                case clsTestTypes.enTestTypes.VisionTest:
                    lblTestType.Text = "Vision Test Appointment";
                    lblTestPic.Image = Resources.Vision_512;
                    break;
                case clsTestTypes.enTestTypes.WrittenTest:
                    lblTestType.Text = "Written Test Appointment";
                    lblTestPic.Image = Resources.Written_Test_512;
                    break;
                case clsTestTypes.enTestTypes.StreetTest:
                    lblTestType.Text = "Driving Test Appointment";
                    lblTestPic.Image = Resources.driving_test_512;
                    break;
            }

            }


        private void frmTestAppointmentcs_Load(object sender, EventArgs e)
        {
            _HandleImageAndLable();

            ctrlApplicationInfo1.LoadInfo(_LocalDrivingLicenseAppID);
            _dt = clsTestAppointments.GetAllApplcationTestAppointmentsPerDataType(_LocalDrivingLicenseAppID, (byte)_TestType);
            dgvAppointments.DataSource = _dt;

            if (dgvAppointments.Rows.Count > 0)
            {
                dgvAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvAppointments.Columns[0].Width = 150;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 200;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 150;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 100;
            }

            lblRecordsCount.Text = dgvAppointments.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLdlID(_LocalDrivingLicenseAppID);
            if (localDrivingLicenseApplication.DoesHaveActiveApplicationWithTestType(_TestType))
            {
                MessageBox.Show("Person Already Has An Active Test Appointment !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (localDrivingLicenseApplication.DidPassTestType(_TestType))
            {
                MessageBox.Show("Person Already Passed Test Type, He Cannot Retake The Test !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form frm = new frmScheduleTest(_LocalDrivingLicenseAppID, _TestType, -1);
            frm.ShowDialog();
            frmTestAppointmentcs_Load(null, null);
        }

        private void editTest_Click(object sender, EventArgs e)
        {
            Form frm = new frmScheduleTest(_LocalDrivingLicenseAppID, _TestType, Convert.ToInt32(dgvAppointments.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            frmTestAppointmentcs_Load(null, null);
        }

        private void takeTest_Click(object sender, EventArgs e)
        {
            Form frm = new frmTakeTest(Convert.ToInt32(dgvAppointments.CurrentRow.Cells[0].Value), _TestType);
            frm.ShowDialog();
            frmTestAppointmentcs_Load(null, null);
        }
    }
}
