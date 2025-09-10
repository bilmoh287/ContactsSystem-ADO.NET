using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace ContactsPresentationLayer
{
    public partial class frmAddEditContact : Form
    {
        public enum enMode { AddNew = 0, Update = 1};
        private enMode _Mode;

        int _ContactID;
        clsContact _Contact;

        public frmAddEditContact(int ID)
        {
            InitializeComponent();

            _ContactID = ID;
            if(_ContactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountries.GetAllCountries();

            foreach(DataRow row in  dtCountries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        }


        private void _LoadData()
        {
            _FillCountriesInComboBox();
            cbCountries.SelectedIndex = 0;
            if(_Mode == enMode.AddNew)
            {
                lblHead.Text = "Add New Contact";
                _Contact = new clsContact();
                llRemoveImage.Visible = false;
                return;
            }


            _Contact = clsContact.Find(_ContactID);
            lblHead.Text = "Edit Contact with ID = " + _ContactID;

            lblContactID.Text = _ContactID.ToString();
            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtAddress.Text = _Contact.Address;
            txtPhone.Text = _Contact.Phone;
            DateOfBirth.Value = _Contact.DateOfBirth;

            if(_Contact.ImagePath != "")
            {
                pictureBox1.Load(_Contact.ImagePath);
            }

            llRemoveImage.Visible = (_Contact.ImagePath  != "");
            cbCountries.SelectedIndex = cbCountries.FindString(clsCountries.FindCountry(_Contact.CountryID).CountryName);
        }

        private void frmAddEditContact_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountries.FindCountry(cbCountries.Text).ID;

            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.DateOfBirth = DateOfBirth.Value;
            _Contact.CountryID = CountryID;
            if(pictureBox1.ImageLocation != null)
            {
                _Contact.ImagePath = pictureBox1.ImageLocation.ToString();
            }
            else
            {
                _Contact.ImagePath = "";
            }

            if(_Contact.Save())
            {
                MessageBox.Show("Data Saved Successfully.");
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.");
            }

            _Mode = enMode.Update;

            lblHead.Text = "Edit Contact ID = " + _Contact.ID;
            lblContactID.Text = _Contact.ID.ToString();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
            llRemoveImage.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
