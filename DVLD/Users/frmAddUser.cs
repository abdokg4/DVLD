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
    public partial class frmAddUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1};
        private enMode _Mode;

        private int _UserID;
        private clsUser _User;

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();

                tpLoginInfo.Enabled = false;

                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else
            {
                lblMode.Text = "Update User";
                this.Text = "Update User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;


            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = true;
        }

        public frmAddUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            if (UserID == -1)
            {
                _Mode = enMode.AddNew;

            }
            else
                _Mode = enMode.Update;
        }

        private void _LoadData()
        {
            if(_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New User";
                _User = new clsUser();
                return;
            }

            _User = clsUser.Find(_UserID);
            if (_User == null)
            {
                MessageBox.Show("This form will be closed because No User with ID = " + _UserID);
                this.Close();

                return;
            }

            lblMode.Text = "Update User";
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonCardWithFilter1.DisableFilter();
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ctrlPersonCardWithFilter1.PersonID != -1)
            {

                if (_Mode == enMode.Update)
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcAdd.SelectTab(1);
                    return;
                }

                if (clsUser.isUserExistWithPersonID(ctrlPersonCardWithFilter1.PersonID))
                {

                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }

                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcAdd.SelectTab(1);
                }
            }

            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            _User.UserName = txtUserName.Text;
            _User.Password = clsUtil.GetHash(txtPassword.Text);
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;

            _User.IsActive = (chkIsActive.Checked) ? true : false;

           if( _User.Save())
            {
            MessageBox.Show("Data Saved Successfully!");
                _Mode = enMode.Update;
                lblUserID.Text = _User.UserID.ToString();
                lblMode.Text = "Update User";
            }
           else
            {
                MessageBox.Show("Data Wasn't Saved Successfully!");
            }

            
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This Value Is Required!");

            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "This Value Is Required!");

            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This Value Is Required!");

            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }

            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This Value Has To Match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
