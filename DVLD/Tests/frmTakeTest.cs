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
    public partial class frmTakeTest : Form
    {
        int _TestAppointmentID = -1;
        clsTestTypes.enTestTypes _TestType;
        int _TestID;
        clsTests Test;
        public frmTakeTest(int testAppointmentID, clsTestTypes.enTestTypes TestType)
        {

            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
            _TestType = TestType;
        }

        private void _LoadInfo()
        {
         ctrlScheduledTest1.TestType = _TestType;
            ctrlScheduledTest1.LoadInfo(_TestAppointmentID);
          _TestID = ctrlScheduledTest1.TestID;
            rbPass.Checked = true;

            if (ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            if (_TestID != -1)
            {
                lblUserMessage.Visible = true;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
                Test = clsTests.Find(_TestID);
                txtNotes.Text = Test.Notes;
                if(Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;
            }
            else
            {
                Test = new clsTests();
            }
        }
        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Test.TestAppointmentID = _TestAppointmentID;
            Test.TestResult = (rbPass.Checked) ? true : false;
            Test.Notes = txtNotes.Text;
            Test.CreatedByUserID = clsGlobalUser.CurrentUser.UserID;

            if(MessageBox.Show("Are You Sure You Want To Save These Test Results ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if(Test.Save())
                {
                    MessageBox.Show("Test Results Saved Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Test Results WASN'T Saved Successfully !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
