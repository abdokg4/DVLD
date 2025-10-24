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
    public partial class ctrlLicenseHistory : UserControl
    {
        private int _DriverID;
        private clsDrivers _Driver;
        private DataTable dtLocalDrivingLicenses;
        private DataTable dtInternationalDrivingLicenses;
        public ctrlLicenseHistory()
        {
            InitializeComponent();
        }

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _Driver = clsDrivers.FindDriverByDriverID(_DriverID);

            if (_Driver == null)
            {
                MessageBox.Show("Error Finding Driver With ID = " + DriverID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadLocalDrivingLicenseData();
        }
        
        public void LoadInfoByPersonID(int PersonID)
        {
            
            _Driver = clsDrivers.FindDriverByPersonID(PersonID);

            if (_Driver == null)
            {
                MessageBox.Show("Error Finding Driver With Person ID = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = _Driver.DriverID;

            _LoadLocalDrivingLicenseData();
            _InternationalDrivingLicenseData();
        }
        private void _LoadLocalDrivingLicenseData()
        {
            dtLocalDrivingLicenses = clsLicense.GetLocalDrivingLicensesByDriverID(_DriverID);
            dgvLocal.DataSource = dtLocalDrivingLicenses;

            if (dgvLocal.Rows.Count > 0)
            {
                dgvLocal.Columns[0].HeaderText = "Lic.ID";
                dgvLocal.Columns[0].Width = 120;

                dgvLocal.Columns[1].HeaderText = "App.ID";
                dgvLocal.Columns[1].Width = 100;

                dgvLocal.Columns[2].HeaderText = "Class Name";
                dgvLocal.Columns[2].Width = 320;

                dgvLocal.Columns[3].HeaderText = "Issue Date";
                dgvLocal.Columns[3].Width = 170;

                dgvLocal.Columns[4].HeaderText = "Expiration Date";
                dgvLocal.Columns[4].Width = 170;

                dgvLocal.Columns[5].HeaderText = "Is Active";
                dgvLocal.Columns[5].Width = 100;
            }
            else
            {
                cmsLocalLicenseInfo.Enabled = false;
            }
                lblLocalRecords.Text = dgvLocal.Rows.Count.ToString();
        }

        private void _InternationalDrivingLicenseData()
        {
            dtInternationalDrivingLicenses = clsInternationalDrivingLicense.GetInternationalDrivingLicensesByDriverID(_DriverID);
            dgvInternational.DataSource = dtInternationalDrivingLicenses;

            if (dgvInternational.Rows.Count > 0)
            {
                dgvInternational.Columns[0].HeaderText = "Int.License ID";
                dgvInternational.Columns[0].Width = 180;

                dgvInternational.Columns[1].HeaderText = "Application ID";
                dgvInternational.Columns[1].Width = 150;

                dgvInternational.Columns[2].HeaderText = "L.License ID";
                dgvInternational.Columns[2].Width = 150;

                dgvInternational.Columns[3].HeaderText = "Issue Date";
                dgvInternational.Columns[3].Width = 190;

                dgvInternational.Columns[4].HeaderText = "Expiration Date";
                dgvInternational.Columns[4].Width = 190;

                dgvInternational.Columns[5].HeaderText = "Is Active";
                dgvInternational.Columns[5].Width = 120;
            }
            else
            {
                cmsInternationalLicenseInfo.Enabled = false;
            }
                lblInternationalRecords.Text = dgvInternational.Rows.Count.ToString();
        }

        private void ctrlLicenseHistory_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowLicenseInfo(Convert.ToInt32(dgvLocal.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo(Convert.ToInt32(dgvInternational.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
    }
}
