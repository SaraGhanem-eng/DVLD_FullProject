using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests_Types
{
    public partial class frmEditTestType : Form
    {
        private int _TestTypeID = -1;
        private clsTestsTypes _Test;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            if (_TestTypeID == -1)
            {
                this.Close();
                return;
            }
            _Test = clsTestsTypes.Find(_TestTypeID);

            if (_Test == null)
            {
                this.Close();
                return;
            }

            lblID.Text = _Test.TestTypeID.ToString();
            txtTitle.Text = _Test.TestTypeTitle;
            txtDescription.Text = _Test.TestTypeDescription;
            txtFees.Text = _Test.Fees.ToString();

            txtTitle.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "Title Can Not Be Empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");
            }

        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "Description Can Not Be Empty ! ");
            }
            else
            {
                errorProvider1.SetError(txtDescription, "");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            _Test.TestTypeTitle = txtTitle.Text;
            _Test.Fees = double.Parse(txtFees.Text);
            _Test.TestTypeDescription = txtDescription.Text;

            if (_Test.UpdateTestType())
            {
                MessageBox.Show("Saved Successfully ! ", "Process Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            else
            {
                MessageBox.Show("Save Failed ! ", "Failure  ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
