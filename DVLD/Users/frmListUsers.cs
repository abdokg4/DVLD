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
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;
       
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            lblRecordsNum.Text = dgvUsers.Rows.Count.ToString();
        }
        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsNum.Text = dgvUsers.Rows.Count.ToString();

            if(dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;

                dgvUsers.Columns[3].HeaderText = "UserName";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(cbFilterBy.Text == "Is Active")
            {
                cbIsActive.Visible = true;
                txtFilterBy.Visible = false;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterBy.Visible = (cbFilterBy.Text != "None");

                if(txtFilterBy.Visible)
                {
                    txtFilterBy.Text = "";
                    txtFilterBy.Focus();
                    cbIsActive.Visible = false;
                }
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterMode = "";

            switch(cbFilterBy.Text)
            {
                case "User ID":
                    FilterMode = "UserID";
                    break;
                case "Person ID":
                    FilterMode = "PersonID";
                    break;
                case "UserName":
                    FilterMode = "UserName";
                    break;
                case "Full Name":
                    FilterMode = "FullName";
                    break;
                default:
                    FilterMode = "None";
                    break;
            }

            if(txtFilterBy.Text.Trim() == "" || FilterMode == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsNum.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if(FilterMode == "PersonID" || FilterMode == "UserID")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterMode, txtFilterBy.Text.Trim());
            }

            lblRecordsNum.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterMode = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch(FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if(FilterValue == "All")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsNum.Text = dgvUsers.Rows.Count.ToString();
                return;
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, FilterValue);
                lblRecordsNum.Text = _dtAllUsers.Rows.Count.ToString();
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUser(-1);
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void AddUsertoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUser(-1);
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void EditStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUser(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshUsersList();
        }

        private void DeleteStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Delete This User ?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("User Deleted Successfully.");
                }
                else
                    MessageBox.Show("Error");
            }

            _RefreshUsersList();
        }

        private void SendEmailtoolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowUserDetails(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(Convert.ToInt32(dgvUsers.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }
    }
}
