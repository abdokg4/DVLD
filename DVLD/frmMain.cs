using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using DVLD_DataAccess;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        clsPerson _Person = clsPerson.Find(clsGlobalUser.CurrentUser.PersonID);
        frmLoginScreen _frmLogin;
        public frmMain(frmLoginScreen frmLogin)
        {
            InitializeComponent();
            _frmLogin = frmLogin;
        }

        private void lostSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void UPDATET_Click(object sender, EventArgs e)
        {
            
        }

        private void Users_Click(object sender, EventArgs e)
        {
            
        }

        private void CLOSEB_Click(object sender, EventArgs e)
        {
            this.Close();
            _frmLogin.Close();
        }

        private void MINIMIZEB_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void People_Click(object sender, EventArgs e)
        {
            
        }

        private void Users_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Drivers_Click(object sender, EventArgs e)
        {

        }

        private void TOPPL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void hopeTabPage1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hopeTabPage1.ForeColorA = System.Drawing.Color.Teal;
            //hopeTabPage1.ThemeColorA = System.Drawing.Color.Teal;
        }

        private void BOTPL_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TEXTPB_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void hopeTabPage1_Selected(object sender, TabControlEventArgs e)
        {
            
            
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            frmFines frm = new frmFines();
            frm.ShowDialog();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            frmLicenseApps frm = new frmLicenseApps();
            frm.ShowDialog();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            Form frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _frmLogin.Show();
            this.Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            frmShowUserDetails frm = new frmShowUserDetails(clsGlobalUser.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            frmLicenses frm = new frmLicenses();
            frm.ShowDialog();
        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(clsLicense.GetActiveLicenseIDByPersonID(clsGlobalUser.CurrentUser.PersonID, 3));
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblName.Text = _Person.FirstName;
            _LoadPersonImage();
            lblLicenseCreate.Text = clsFormat.DateToShort(clsLicense.FindLicenseByLicenseID(clsLicense.GetActiveLicenseIDByPersonID(_Person.PersonID,3)).IssueDate);
            lblLicenseExpire.Text = clsFormat.DateToShort(clsLicense.FindLicenseByLicenseID(clsLicense.GetActiveLicenseIDByPersonID(_Person.PersonID,3)).ExpirationDate);
        }

        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void moonLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
