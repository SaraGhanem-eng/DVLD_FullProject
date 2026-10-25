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

namespace DVLD.Application_Types
{
    public partial class frmEditApplicationType : Form
    {
        private int _TypeID = -1;
        private clsApplicationTypes _Type;
        public frmEditApplicationType(int TypeID )
        {
            InitializeComponent();
            if (TypeID != -1)
                _TypeID = TypeID; 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            if (_TypeID == -1 )
            {
                this.Close();
                return;
            }
            _Type = clsApplicationTypes.Find(_TypeID);

            if (_Type == null)
            {
                this.Close();
                return;
            }

            lblID.Text = _Type.ApplicationTypeID.ToString();
            txtTitle.Text = _Type.ApplicationTypeTitle;
            txtFees.Text = _Type.Fees.ToString();

            txtTitle.Focus();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title Can Not Be Empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
            }

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees Can Not Be Empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtFees, "");
            }

            if (!double.TryParse(txtFees.Text, out double fees) || fees < 0)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Enter a valid non-negative fee.");
            }
            else
            {
                errorProvider1.SetError(txtFees, "");
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                return;
            }

            if (e.KeyChar == '.' && !txtFees.Text.Contains('.'))
            {
                return;
            }

            e.Handled = true;
        }

        private void txtTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) ||
                char.IsControl(e.KeyChar) ||
                e.KeyChar == ' ')
            {
                return;
            }

            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            _Type.ApplicationTypeTitle = txtTitle.Text;
            _Type.Fees = double.Parse(txtFees.Text);


            if (_Type.UpdateApplicationType())
            {
                MessageBox.Show("Saved Successfully ! " , "Process Succeeded" , MessageBoxButtons.OK , MessageBoxIcon.Information);
                this.Close();

            }
            else
            {
                MessageBox.Show("Save Failed ! ", "Failure  ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
