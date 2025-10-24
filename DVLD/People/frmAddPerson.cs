using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    public partial class frmAddPerson : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);

        public event DataBackEventHandler DataBack;
        
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        int _PersonID;
        clsPerson _Person;

        
        public frmAddPerson(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
                _Mode = enMode.Update;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {

                cbCountry.Items.Add(row["CountryName"]);

            }
        }

        private void _LoadData()
        {
            _FillCountriesInComboBox();
            cbCountry.SelectedItem = "Egypt";
            dtDateTime.MaxDate = DateTime.Now.AddYears(-18);

            rbMale.Checked = true;
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Person";
                _Person = new clsPerson();
                return;
            }

            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show("This form will be closed because No Person with ID = " + _PersonID);
                this.Close();

                return;
            }

            lblMode.Text = "Update Person";
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtDateTime.Value = _Person.DateOfBirth;

            if(_Person.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            
            if(_Person.ImagePath != "")
            {
                pbProfilePic.Load(_Person.ImagePath);
                lLblRemove.Visible = true;
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmAddPerson_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
        private void rbMale_Click(object sender, EventArgs e)
        {
            if (_Person.ImagePath == "")
                pbProfilePic.Image = Resources.Male_512;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (_Person.ImagePath == "")
                pbProfilePic.Image = Resources.Female_512;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbProfilePic.Image = (rbMale.Checked == true) ? Resources.Male_512 : Resources.Female_512;
            lLblRemove.Visible = false;

        }

        private bool _HandlePersonImage()
        {
            if(pbProfilePic.ImageLocation != _Person.ImagePath)
            {
                if(_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                        
                    }
                    catch(IOException)
                    {

                    }
                }

                if(pbProfilePic.ImageLocation != null)
                {
                    string ImageSourceFile = pbProfilePic.ImageLocation.ToString();
                    if (clsUtil.CopyPicturesToProjectImagesFile(ref ImageSourceFile))
                    {
                        pbProfilePic.ImageLocation = ImageSourceFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }


                }
            }
            return true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pbProfilePic.Load(selectedFilePath);
                lLblRemove.Visible = true;
                // ...
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            int CountryID = clsCountry.Find(cbCountry.Text).CountryID;


            if (!_HandlePersonImage())
                return;

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalNo = txtNationalNo.Text;
            _Person.DateOfBirth = dtDateTime.Value;

            if (rbMale.Checked == true)
                _Person.Gender = 0;
            else
                _Person.Gender = 1;

            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.NationalityCountryID = CountryID;

            if (pbProfilePic.ImageLocation != null)
            {
                _Person.ImagePath = pbProfilePic.ImageLocation;
            }
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
                DataBack?.Invoke(this, _Person.PersonID);
                

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
            lblMode.Text = "Update Contact";
            lblPersonID.Text = _Person.PersonID.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFirstName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFirstName, "This Value Is Required!");
            }
            else
            {
                
                errorProvider1.SetError(txtFirstName, "");
            }
        }

        private void txtSecondName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
               
                errorProvider1.SetError(txtNationalNo, "This Value Is Required!");
            }

            else if(txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.isPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number Is Used For Another Person!");
            }

            else
            {
               
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecondName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSecondName, "This Value Is Required!");
            }
            else
            {
            
                errorProvider1.SetError(txtSecondName, null);
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLastName, "This Value Is Required!");
            }
            else
            {
             
                errorProvider1.SetError(txtLastName, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;


            if(!clsValidate.ValidateEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "A Valid Email Is Required!");
            }

            else
            {
               
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "This Value Is Required!");
            }
            else
            {
             
                errorProvider1.SetError(txtPhone, null);
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtAddress, "This Value Is Required!");
            }
            else
            {
                
                errorProvider1.SetError(txtAddress, null);
            }
        }
    }
}
