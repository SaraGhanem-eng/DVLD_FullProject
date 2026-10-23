using DVLD.Business;
using DVLD.Classes;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using DVLD.GLobal_Classes;

namespace DVLD
{
    public partial class frmAdd_EditPersonInfo : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

         public enum enMode { AddNew = 0 , Update = 1 } ;
         private enum enValidationType
        {
            Required =1  ,
            Letters = 2 ,
            Numbers = 4 ,
            LettersAndNumbers = 8 ,
            Email = 16 
        }
         private enMode _Mode;
         clsPerson _Person;
         int _PersonID;
         private enum enImageState
         {
            None, // Default Value , No action needs to be exist .
            New,
            Removed
         }

         private enImageState _ImageState = enImageState.None;
         private string _ImagesFolder = @"C:\DVLD-People-Images";
         private string _SelectedImagePath = ""; 

        public frmAdd_EditPersonInfo(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            
            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
        }

        // Image Handling
        private void _SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _SelectedImagePath = openFileDialog.FileName;

                pbPerson.Load(_SelectedImagePath);

                llRemoveImage.Visible = true;
                // set image state in case selctind one
                _ImageState = enImageState.New;
            }
        }

        private bool _HandleImage()
        {
            switch (_ImageState)
            {
                case enImageState.None:
                {
                        return true;
                }
                case enImageState.New:
                {
                        string NewFilePath = "";
                        if (clsUtil._SaveImage(_SelectedImagePath , ref NewFilePath))
                        { 
                            _Person._ImagePath = NewFilePath;
                            return true;
                        }
                        return false;
                }
                case enImageState.Removed:
                {
                        _Person._ImagePath = ""; // will be deleted after person is saved 
                        return true;
                }
                default:
                {
                        return false;
                }
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _SelectImage();
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Set image state in case removing picture 
            _ImageState = enImageState.Removed;
            _SelectedImagePath = "";
            if (_Person._Gender == 0)
            {
                pbPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Male 512.png");
            }
            else
            {
                pbPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Female 512.png");
            }

            llRemoveImage.Visible = false;
        }

        // Validation Handling
        private void _SetValidateRules()
        {
            txtFirstName.Tag = enValidationType.Required | enValidationType.Letters;
            txtSecondName.Tag = enValidationType.Required | enValidationType.Letters;
            txtLastName.Tag = enValidationType.Required | enValidationType.Letters;
            txtAddress.Tag = enValidationType.Required;
            txtPhone.Tag = enValidationType.Required | enValidationType.Numbers;
            txtThirdName.Tag = enValidationType.Letters;
            txtEmail.Tag = enValidationType.Email;
            txtNationalNo.Tag = enValidationType.Required | enValidationType.LettersAndNumbers;

        }

        private bool _ValidateTextBox(TextBox textBox, enValidationType vtype, ErrorProvider errorProvider)
        {

                // Required
                if ((vtype & enValidationType.Required) != 0)
                {
                    if (textBox.Text == "")
                    {
                        errorProvider.SetError(textBox, "This field is required");
                        return false;
                    }
                }

                // Optional and empty
                if (textBox.Text == "")
                {
                    errorProvider.SetError(textBox, "");
                    return true;
                }

                // Letters
                if ((vtype & enValidationType.Letters) != 0)
                {
                    if (!clsValidation.IsLetters(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "Only letters are allowed");
                        return false;
                    }
                }

                // Numbers
                if ((vtype & enValidationType.Numbers) != 0)
                {
                    if (!clsValidation.IsNumberOnly(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "Only numbers are allowed");
                        return false;
                    }
                }

                // Letters and Numbers
                if ((vtype & enValidationType.LettersAndNumbers) != 0)
                {
                    if (!clsValidation.IsLettersAndNumbers(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "Only letters and numbers are allowed");
                        return false;
                    }
                }

                // Email
                if ((vtype & enValidationType.Email) != 0)
                {
                    if (!clsValidation.IsValidEmail(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "Invalid email address");
                        return false;
                    }
                }

            
            errorProvider.SetError(textBox, "");
            return true;
        }
      
        private bool _ValidateTextBoxes(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox)
                {
                    enValidationType rules =
                        (enValidationType)textBox.Tag;

                    if (!_ValidateTextBox(textBox, rules, errorProvider1))
                        return false;
                }

                if (control.HasChildren)
                {
                    if (!_ValidateTextBoxes(control))
                        return false;
                }
            }

            return true;
        }

        // this event (TextBox_Validating) is called by each text box in the form .
        private void TextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            enValidationType rules = (enValidationType)textBox.Tag;

            _ValidateTextBox(textBox, rules, errorProvider1);
        }

        private bool _ValidateBeforeSave()
        {
            if (!_ValidateTextBoxes(this))
                return false;

            if (!_ValidateNationalNo())
                return false;


            return true;
        }

        private bool _ValidateNationalNo()
        {
            if (_Mode == enMode.AddNew)
            {
                if (clsPerson.IsPersonExist(txtNationalNo.Text))
                {
                    errorProvider1.SetError(
                        txtNationalNo,
                        "National No already exists");

                    return false;
                }
            }
            else // Update
            {
                if (clsPerson.IsPersonExistExceptPersonID(
                    txtNationalNo.Text, _PersonID))
                {
                    errorProvider1.SetError(
                        txtNationalNo,
                        "National No already exists");

                    return false;
                }
            }

            errorProvider1.SetError(txtNationalNo, "");

            return true;
        }

        // form initialization 
        private void _Load()
        {
            //  Prepare the form
            _SetValidateRules();
            _FillCountryInComboBox();
            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);

            //  Initialize the Business Object
            if (_Mode == enMode.AddNew)
            {
                _Person = new clsPerson();
            }
            else
            {
                _Person = clsPerson.Find(_PersonID);

                if (_Person == null)
                {
                    MessageBox.Show(
                        "This Form Will be closed because no Person Exist with ID : "
                        + _PersonID);

                    this.Close();
                    return;
                }
            }

            // Set default values for Add New
            if (_Mode == enMode.AddNew)
            {
                lblAddUpdate.Text = "Add New Person";

                rbMale.Checked = true;
                cbCountry.SelectedIndex = cbCountry.FindString("Egypt");

                return;
            }

            // Load existing Person data for Update
            lblAddUpdate.Text = "Update Person";

            lblPersonID.Text = _Person._PersonID.ToString();
            txtNationalNo.Text = _Person._NationalNo;
            txtFirstName.Text = _Person._FirstName;
            txtSecondName.Text = _Person._SecondName;
            txtThirdName.Text = _Person._ThirdName;
            txtLastName.Text = _Person._LastName;
            txtPhone.Text = _Person._Phone;
            txtEmail.Text = _Person._Email;
            txtAddress.Text = _Person._Address;

            dtpDateOfBirth.Value = _Person._DateOfBirth;

            cbCountry.SelectedIndex =
                cbCountry.FindString(
                    clsCountry.FindByCountryID(
                        _Person._NationalityCountryID)._CountryName);

            // Gender
            if (_Person._Gender == 0)
            {
                rbMale.Checked = true;
            }
            else if (_Person._Gender == 1)
            {
                rbFemale.Checked = true;
            }

            // Image
            if (_Person._ImagePath != "")
            {
                llRemoveImage.Visible = true;
                string FullImagePath = Path.Combine(_ImagesFolder, _Person._ImagePath);
                pbPerson.ImageLocation = FullImagePath;
               // pbPerson.Load(FullImagePath);
            }
            else
            {
                if (_Person._Gender == 0)
                {
                    pbPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Male 512.png");
                }
                else
                {
                    pbPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Female 512.png");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillCountryInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
                
            }
        }

        private void frmAdd_EditPersonInfo_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_ValidateBeforeSave())
                return;


            int CountryID = clsCountry.FindByCountryName(cbCountry.Text)._CountryID;

           _Person._FirstName = txtFirstName.Text;
           _Person._SecondName = txtSecondName.Text;
           _Person._ThirdName = txtThirdName.Text;
           _Person._LastName = txtLastName.Text;
           _Person._Address = txtAddress.Text;
           _Person._Phone = txtPhone.Text;
           _Person._DateOfBirth = dtpDateOfBirth.Value;
           _Person._NationalityCountryID = CountryID;
           _Person._Email = txtEmail.Text;
           _Person._NationalNo = txtNationalNo.Text;

            if (rbMale.Checked)
            {
                _Person._Gender = 0;
            }
            else
            {
                _Person._Gender = 1;
            }

            string OldImagePath = _Person._ImagePath;
            if (!_HandleImage())
            {
                MessageBox.Show("Error while handling image.");
                return;
            }

            if (_Person.Save())
            {
                if (_ImageState != enImageState.None &&
                   !string.IsNullOrEmpty(OldImagePath))
                {
                   if (!clsUtil.DeleteImage(OldImagePath))
                    {
                        MessageBox.Show("Failed to delete Old Image !");
                    }
                }

                MessageBox.Show("Data Saved Successfully ! ");


                _Mode = enMode.Update;
                _PersonID = _Person._PersonID; 
                lblAddUpdate.Text = "Update Person";
                lblPersonID.Text = _Person._PersonID.ToString();

                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person._PersonID);
            }
            else
            {
                MessageBox.Show("Error : Data Is Not Saved Successfully !");
            }


           
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (_Person != null && _Person._ImagePath == "" )
            {
                if (rbMale.Checked)
                {
                    pbPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Male 512.png");

                }
                else
                {
                    pbPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Female 512.png");
                }
            }
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
