using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {

        private int _UserID = -1;
        private int _PersonID = -1;

        private clsUsers _User;

        private enum enMode { enAddNew , enUpdate };
        private enMode _Mode;


        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = enMode.enAddNew;
            this.Text = "Add New User";

            // subscribe at the event OnPersonSelected in ctrlPersonCardWithFilter
            // Person Id is Recieved at the function (CtrlPersonCardWithFilter2_OnPersonSelected(int PersonID))
            // with each Successfull Search
            ctrlPersonCardWithFilter2.OnPersonSelected +=
            CtrlPersonCardWithFilter2_OnPersonSelected;

        }

        public frmAddEditUser(int UserID )
        {
            InitializeComponent();
            // For Edit Mode 
            this._UserID = UserID;
            _Mode = enMode.enUpdate;
            this.Text = "Upadate User";

        }

        private void CtrlPersonCardWithFilter2_OnPersonSelected(int PersonID)
        {
            // Event Handler
            if (_Mode == enMode.enAddNew)
            {

                btnNext.Enabled = !clsUsers.IsUserExistByPersonID(PersonID);

                bool IsValid = btnNext.Enabled ? true : false;
                if (!IsValid)
                {
                    MessageBox.Show(
                    "Selected Person Already Has A User, Choose Another One",
                    "Select Another Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }


            }
        }

        private void _Load()
        {
            switch (_Mode)
            {

                case enMode.enAddNew:
                {
                        lblAddUpdate.Text = "Add New User";
                        ctrlPersonCardWithFilter2.FilterEnabled = true;
                        btnSave.Enabled = false;
                        btnNext.Enabled = false;

                        _User = new clsUsers();

                        _ResetData();
                        break;
                }
                case enMode.enUpdate:
                {
                        lblAddUpdate.Text = "Update User";
                        ctrlPersonCardWithFilter2.FilterEnabled = false ;

                        _User = clsUsers.Find(_UserID);


                        if (_User == null)
                        {
                            MessageBox.Show("This User is Not Exist ! ");
                            this.Close();
                            return;
                        }
                        _PersonID = _User.PersonID;
                        _LoadData();

                        break;
                }
            }
        }
       
        private void _LoadData()
        {
            // Load User Info
            lblUserID.Text = _UserID.ToString();
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            txtUserName.Text = _User.UserName;
            cbIsActive.Checked = _User.IsActive;

            // Load Person Info 
            ctrlPersonCardWithFilter2.LoadPersonInfo(_PersonID);
        }

        private void _ResetData()
        {
            lblUserID.Text = "[????]";

            txtPassword.Clear();
            txtConfirmPassword.Clear();
            txtUserName.Clear();
            cbIsActive.Checked = true;

        }

        private void _Save()
        {
            _User.UserName = txtUserName.Text;
            _User.Password = txtPassword.Text;
            _User.PersonID = _PersonID; // ??
            _User.IsActive = cbIsActive.Checked ? true : false;

                if (_User.Save())
                {
                  MessageBox.Show("Data Saved Successfully ! ");
                 _Mode = enMode.enUpdate;
                 _UserID = _User.UserID;
                 lblAddUpdate.Text = "Update User ";
                this.Text = "Update User";
                 lblUserID.Text = _UserID.ToString();
                
                }
                else
                {
                 MessageBox.Show("Error : Data Is Not Saved Successfully !");
                }

        }
        private void btnNext_Click(object sender, EventArgs e)
        {

            int SelectedPersonID = ctrlPersonCardWithFilter2.PersonID;

            if (SelectedPersonID == -1)
            {
                MessageBox.Show("Please select a person first.");
                return;
            }

            if (_Mode == enMode.enAddNew &&
                clsUsers.IsUserExistByPersonID(SelectedPersonID))
            {
                MessageBox.Show(
                    "Selected Person Already Has A User, Choose Another One",
                    "Select Another Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            _PersonID = SelectedPersonID;
            tabControl1.SelectedTab = tabPage2;
            btnSave.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
               errorProvider1.SetError(txtUserName, "This field is required!");
                return;
            }


            if (_Mode == enMode.enAddNew ||
                txtUserName.Text.Trim() != _User.UserName)
            {
                if (clsUsers.IsUserExistByUserName(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(
                        txtUserName,
                        "Username is already used by another user.");
                    return;
                }
            }

            errorProvider1.SetError(txtUserName, "");
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }



        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This field is required!");
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                errorProvider1.SetError(
                    txtConfirmPassword,
                    "Passwords do not match.");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (!ValidateChildren())
                return;
            
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()) ||
                string.IsNullOrEmpty(txtUserName.Text.Trim()) ||
                string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                return ;

            if (txtPassword.Text != txtConfirmPassword.Text)
                return;
            

            _Save();
        }
    }
}
