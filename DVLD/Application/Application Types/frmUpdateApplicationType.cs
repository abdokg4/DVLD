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
    public partial class frmUpdateApplicationType : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationTypes _ApplicationTypes;
        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void _LoadInfo()
        {
            _ApplicationTypes = clsApplicationTypes.Find(_ApplicationTypeID);
            if (_ApplicationTypes == null)
            {
                MessageBox.Show("The Application Type Couldn't Be Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblID.Text = _ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationTypes.ApplicationTypeTitle;
            txtFees.Text = _ApplicationTypes.ApplicationFees.ToString();
        }
        private void clsUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            _ApplicationTypes.ApplicationTypeTitle = txtTitle.Text;
            _ApplicationTypes.ApplicationFees = Convert.ToDecimal(txtFees.Text);

            if(_ApplicationTypes.Save())
            {
                MessageBox.Show("Application Type Updated Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Application Type Is NOT Updated Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "This Value Is Required!");

            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "This Value Is Required!");

            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
