using DVLD.Business;
using DVLD.Classes;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {
        private DataTable _dtAllUsers ;
        public frmListUsers()
        {
            InitializeComponent();

        }


        private void _LoadUsers()
        {
            _dtAllUsers = clsUsers.GetAllUsers();
            dgvListUsers.DataSource = _dtAllUsers;
            lblRecordsCount.Text = dgvListUsers.RowCount.ToString();


            dgvListUsers.ReadOnly = true;

            dgvListUsers.AllowUserToAddRows = false;
            dgvListUsers.AllowUserToDeleteRows = false;

            dgvListUsers.Columns[0].Width = 90;
            dgvListUsers.Columns[1].Width = 90;
            dgvListUsers.Columns[2].Width = 200;
            dgvListUsers.Columns[3].Width = 120;
            dgvListUsers.Columns[4].Width = 100;
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _LoadUsers();
            cbFilterBy.SelectedIndex = 0;
            cbIsActive.SelectedIndex = 0;
            cbIsActive.Visible = false;
            txtFilterValue.Text = "";

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _dtAllUsers.DefaultView.RowFilter = "";
            txtFilterValue.Clear();

            if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilterValue.Visible = true;
                cbIsActive.Visible = false;
                txtFilterValue.Enabled = false;
                

            }
            else if (cbFilterBy.SelectedIndex == 5 )
            {

                txtFilterValue.Visible = false;
                txtFilterValue.Enabled = false;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Visible = true;

            }
            else
            {
                    txtFilterValue.Visible = true;
                    cbIsActive.Visible = false;
                    txtFilterValue.Enabled = true;
            }

            lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = cbFilterBy.Text.Replace(" ", "");


            if (FilterColumn == "None" || txtFilterValue.Text.Trim() == "")
            {

                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();
                return;
            }      
            else   
            {
                if (FilterColumn == "UserID" || FilterColumn == "PersonID")
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
                else
                    _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

                lblRecordsCount.Text = dgvListUsers.RowCount.ToString();
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    e.Handled = !clsValidation.IsNumber(e.KeyChar);
                    break;
                case 2:
                    e.Handled = !clsValidation.IsNumberOrLetter(e.KeyChar);
                    break;
                case 3:
                    e.Handled = !clsValidation.IsNumber(e.KeyChar);
                    break;
                case 4:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;

                default:
                    e.Handled = !clsValidation.IsNumberOrLetter(e.KeyChar);
                    break;

            }

        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text.Replace(" ", "") != "IsActive")
                return;

            switch (cbIsActive.Text)
            {
                case "All":
                    _dtAllUsers.DefaultView.RowFilter = "";
                    break;

                case "Yes":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = true";
                    break;

                case "No":
                    _dtAllUsers.DefaultView.RowFilter = "IsActive = false";
                    break;
            }

            lblRecordsCount.Text = dgvListUsers.Rows.Count.ToString();
        
    }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListUsers.CurrentRow != null)
            {
                int UserID = Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value);

                if (UserID != 1)
                {
                    frmUserInfo frm = new frmUserInfo(UserID);
                    frm.ShowDialog();
                }
                _LoadUsers();
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListUsers.CurrentRow != null)
            {
                int UserID = Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value);
                clsUsers User = clsUsers.Find(UserID);


                if (MessageBox.Show("Are you sure that you want to delete User With ID [ " + dgvListUsers.CurrentRow.Cells[0].Value
                 + " ]", "Confirm Delete Message ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (clsUsers.DeleteUser(UserID))
                    {
                        MessageBox.Show("User Deleted Successfully ! ", "Deleted Successfuly ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("User Was Not Deleted Because it has data linked to it ! ", "Failed ! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListUsers.CurrentRow != null)
            {
                int UserID = Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value);


                if (UserID != -1)
                {
                    frmChangePassword frm = new frmChangePassword(UserID);
                    frm.ShowDialog();
                }
                _LoadUsers();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

            _LoadUsers();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListUsers.CurrentRow != null)
            {
                int UserID = Convert.ToInt32(dgvListUsers.CurrentRow.Cells[0].Value);


                if (UserID != -1)
                {
                    frmAddEditUser frm = new frmAddEditUser(UserID);
                    frm.ShowDialog();
                }
                _LoadUsers();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();

            _LoadUsers();
        }
    }
}
