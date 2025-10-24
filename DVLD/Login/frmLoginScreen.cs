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
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void cyberButton1_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), clsUtil.GetHash(txtPassword.Text.Trim()));

            if (user != null)
            {

                if (chkRememberMe.Checked)
                {
                    //store username and password
                    clsGlobalUser.RememberLoginInfo(txtUserName.Text.Trim(), txtPassword.Text.Trim());

                }
                else
                {
                    //store empty username and password
                    clsGlobalUser.RememberLoginInfo("", "");

                }

                //incase the user is not active
                if (!user.IsActive)
                {

                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobalUser.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();


            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobalUser.GetLoginCredentials(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                cyberButton1_Click(sender, e);
            }
        }

        private void frmLoginScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

                cyberButton1_Click(sender, e);
            }
        }
    }
}
