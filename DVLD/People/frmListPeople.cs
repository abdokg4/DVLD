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


    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();

        //only select the columns that you want to show in the grid
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                         "FirstName", "SecondName", "ThirdName", "LastName",
                                                         "GendorCaption", "DateOfBirth", "CountryName",
                                                         "Phone", "Email");
        

        private void _RefreshPersonList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvPeople.DataSource = _dtPeople;
            lblRecordsNum.Text = dgvPeople.Rows.Count.ToString();
        }

        public frmListPeople()
        {
            InitializeComponent();
        }
        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsNum.Text = dgvPeople.Rows.Count.ToString();

            if (dgvPeople.Rows.Count > 0)
            {

                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No.";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gendor";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cbFilterBy.Text != "None");

            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            

            string FilterMode = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterMode = "PersonID";
                    break;
                case "National No.":
                    FilterMode = "NationalNo";
                    break;
                case "First Name":
                    FilterMode = "FirstName";
                    break;
                case "Second Name":
                    FilterMode = "SecondName";
                    break;
                case "Third Name":
                    FilterMode = "ThirdName";
                    break;
                case "Last Name":
                    FilterMode = "LastName";
                    break;
                case "Nationality":
                    FilterMode = "CountryName";
                    break;
                case "Gender":
                    FilterMode = "GendorCaption";
                    break;
                case "Phone":
                    FilterMode = "Phone";
                    break;
                case "Email":
                    FilterMode = "Email";
                    break;
                default:
                    FilterMode = "None";
                    break;
            }

            if (txtFilterBy.Text.Trim() == "" || FilterMode == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsNum.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterMode == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterMode, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterMode, txtFilterBy.Text.Trim());
            }

            lblRecordsNum.Text = dgvPeople.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddPerson(-1);
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddPerson(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void EditStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddPerson(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void AddPersontoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddPerson(-1);
            frm.ShowDialog();
            _RefreshPersonList();
        }

        private void DeleteStripMenuItem3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want To Delete This Contact ?", "Delete", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                }
                else
                    MessageBox.Show("Error");
            }

            _RefreshPersonList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowPersonInfo(Convert.ToInt32(dgvPeople.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
            _RefreshPersonList();
        }
    }
}
