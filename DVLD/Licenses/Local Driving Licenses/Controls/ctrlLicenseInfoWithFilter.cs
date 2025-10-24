using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace WindowsFormsApp1
{
    public partial class ctrlLicenseInfoWithFilter : UserControl
    {
        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectedLicense
        {
            get { return ctrlLicenseInfo1.SelectedLicense; }
        }

        public ctrlLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public event Action<int> OnLicenseSelected;

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }
        public void Find(int LicenseID)
        {
            txtFilterBy.Text = LicenseID.ToString();
            ctrlLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = LicenseID;

            if(OnLicenseSelected != null && groupBox1.Enabled)
            {
                LicenseSelected(ctrlLicenseInfo1.LicenseID);
            }
        }
        private void ctrlLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            txtFilterBy.Focus();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        public void FilterFocus()
        {
            txtFilterBy.Focus();
        }

        public void DisableFilter()
        {
            groupBox1.Enabled = false;
        }

        private void txtFilterBy_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterBy, "This Value Is Required!");

            }
            else
            {
                errorProvider1.SetError(txtFilterBy, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenseID = int.Parse(txtFilterBy.Text.Trim());
            Find(_LicenseID);
           
        }

        private void ctrlLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
