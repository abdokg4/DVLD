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
    public partial class frmShowUserDetails : Form
    {
        int _UserID;
        public frmShowUserDetails(int UserID)
        {
            InitializeComponent();
            ctrlUserInformation1.LoadUserInfo(UserID);
            _UserID = UserID;
        }

        private void frmShowUserDetails_Load(object sender, EventArgs e)
        {
            if(clsGlobalUser.CurrentUser.UserID == _UserID)
            {
                llblChangePassword.Visible = true;
                llblChangePassword.Enabled = true;
            }
        }

        private void ctrlUserInformation1_Load(object sender, EventArgs e)
        {

        }

        private void llblChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(_UserID);
            frm.ShowDialog();
        }
    }
}
