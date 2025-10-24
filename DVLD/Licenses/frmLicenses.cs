using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmLicenses : Form
    {
        public frmLicenses()
        {
            InitializeComponent();
        }

        private void dreamButton1_Click(object sender, EventArgs e)
        {
            frmAddLocalDrivingLicenseApplication frm = new frmAddLocalDrivingLicenseApplication(-1);
            frm.ShowDialog();
        }

        private void btnIntLicense_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicense frm = new frmNewInternationalLicense();
            frm.ShowDialog();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            frmRenewDrivingLicense application = new frmRenewDrivingLicense();
            application.ShowDialog();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            frmReplaceDrivingLicense app = new frmReplaceDrivingLicense();
            app.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense application = new frmReleaseDetainedLicense();
            application.ShowDialog();
        }

        private void btnRetake_Click(object sender, EventArgs e)
        {
            frmManageApplications frmManageApplications = new frmManageApplications();
            frmManageApplications.ShowDialog();
        }
    }
}
