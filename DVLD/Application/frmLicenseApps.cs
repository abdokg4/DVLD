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
    public partial class frmLicenseApps : Form
    {
        public frmLicenseApps()
        {
            InitializeComponent();
        }

        private void btnLocalLicense_Click(object sender, EventArgs e)
        {
            frmManageApplications frmManageApplications = new frmManageApplications();
            frmManageApplications.ShowDialog();
        }

        private void btnIntLicense_Click(object sender, EventArgs e)
        {
            frmListInternationalLicensesApplications frm = new frmListInternationalLicensesApplications();
            frm.ShowDialog();
        }
    }
}
