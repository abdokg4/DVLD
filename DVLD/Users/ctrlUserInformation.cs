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
    public partial class ctrlUserInformation : UserControl
    {

        private clsPerson _Person;
        private clsUser _User;

        private int _UserID = -1;
        private int _PersonID = -1;
        public ctrlUserInformation()
        {
            InitializeComponent();
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUser.Find(UserID);
            if (_User == null)
            {
               
                MessageBox.Show("No User with User ID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            _FillUserInfo();

        }

        private void _FillUserInfo()
        {
            lblUserID.Text = _User.UserID.ToString();
            _UserID = _User.UserID;
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";

        }
        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlUserInformation_Load(object sender, EventArgs e)
        {

        }


    }
}
