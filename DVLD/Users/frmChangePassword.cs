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

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            if (UserID != -1)
            {
                _UserID = UserID;
                _User = clsUsers.Find(UserID);
            }

        }
        int _UserID = -1;
        clsUsers _User;
        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoadUserInfo(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "This field cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");
            }


            if (txtCurrentPassword.Text != _User.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Wrong current password");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "This field can not be blank");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, "");
            }
        }
        
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "This field can not be blank");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
            }


            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Does Not Match New Password !");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }
        
        private bool _ValidateAll()
        { 

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtCurrentPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                return false;

            if (txtCurrentPassword.Text != _User.Password || txtNewPassword.Text != txtConfirmPassword.Text)
                return false;

            return true;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
                return;

            if (!_ValidateAll())
                return;

            if (clsUsers.ChangePassword(_UserID, txtNewPassword.Text))
            {
                MessageBox.Show("Password Changed Successfully ! ", "Saved",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtConfirmPassword.Clear();
                txtCurrentPassword.Clear();
                txtNewPassword.Clear();
                errorProvider1.Clear();
                
            }
            else
            {
                MessageBox.Show("Password Was Not Changed ! ", "Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
