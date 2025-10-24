using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace WindowsFormsApp1
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        private int _ApplicationID;
        private clsApplication _Application;
        public int ApplicationID { get { return _ApplicationID; } }
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        private void _ResetInfo()
        {
            _ApplicationID = -1;
            lblID.Text = "???";
            lblStatus.Text = "???";
            lblType.Text = "???";
            lblFees.Text = "???";
            lblApplicant.Text = "???";
            lblDate.Text = "???";
            lblStatusDate.Text = "???";
            lblCreatedBy.Text = "???";
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _Application = clsApplication.Find(ApplicationID);
            if (_Application == null)
            {
                _ResetInfo();
                MessageBox.Show("No Person with Person ID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }
        
        private void _FillApplicationInfo()
        {
            _ApplicationID = _Application.ApplicationID;
            llPersonInfo.Enabled = true;
            lblID.Text = _Application.ApplicationID.ToString();
            lblStatus.Text = _Application.StatusText;
            lblFees.Text = _Application.PaidFees.ToString();
            lblType.Text = clsApplicationTypes.Find(_Application.ApplicationTypeID).ApplicationTypeTitle;
            lblApplicant.Text = _Application.ApplicantFullName;
            lblDate.Text = clsFormat.DateToShort(_Application.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_Application.LastStatusDate);
            lblCreatedBy.Text = clsUser.Find(_Application.CreatedByUserID).UserName;

        }
        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmShowPersonInfo(_Application.ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
