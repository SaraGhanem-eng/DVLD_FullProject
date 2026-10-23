using DVLD.Business;
using DVLD.GLobal_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DVLD.Login
{
    public partial class frmLoginScreen : Form
    {


        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace( txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "User Name Is Required !");
            }
            else
            {
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password Is Required !");
            }
            else
            {
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren() || txtPassword.Text == "" || txtUserName.Text == "" )
            {
                return;
            }

            clsUsers User = clsUsers.Find(txtUserName.Text, txtPassword.Text);
            if (User == null )
            {
                MessageBox.Show("Invalid UserName \\ Password " , "Wrong Credentials! " , MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }

            if (User.IsActive == false)
            {
                MessageBox.Show("This Account Is Deactivated , Please Contact Your Admin ", "Deactivated Account ! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsGlobalInfo.CurrentUser = User;

            if (cbRememberMe.Checked)
            {
                clsGlobalInfo.SaveRememberMeInfo(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            }
            else
            {
                clsGlobalInfo.ClearRememberMeInfo();
            }

            frmMain frm = new frmMain();
            frm.ShowDialog();

            if (!cbRememberMe.Checked)
            {
                txtUserName.Clear();
                txtPassword.Clear();
            }

        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string Username = "";
            string Password = "";
            bool IsRememebrMeChecked = false ;

            clsGlobalInfo.LoadRememberMeInfo(ref Username, ref Password, ref IsRememebrMeChecked);
            txtUserName.Text = Username;
            txtPassword.Text = Password;
            cbRememberMe.Checked = IsRememebrMeChecked;

        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
