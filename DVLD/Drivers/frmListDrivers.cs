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

namespace DVLD.Drivers
{
    public partial class frmListDrivers : Form
    {
        DataTable _dtDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void _Load ()
        {

            _dtDrivers = clsDrivers.GetAllDrivers();
            dgvDrivers.DataSource = _dtDrivers;
            lblCount.Text = dgvDrivers.Rows.Count.ToString();
        }
        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _Load();
            cbFilterBy.SelectedIndex = 0;
            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 90;

                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 80;

                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 90;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 250;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 150;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 100;
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtDrivers.DefaultView.RowFilter = "";
                lblCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")

                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;
            frmPresentPersonInfo frm = new frmPresentPersonInfo(PersonID);
            frm.ShowDialog();
            _Load();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int PersonID = (int)dgvDrivers.CurrentRow.Cells[1].Value;


            //frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            //frm.ShowDialog();
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
                    e.Handled = !clsValidation.IsNumber(e.KeyChar);
                    break;
                case 3:
                    e.Handled = !clsValidation.IsNumberOrLetterOrSymbol(e.KeyChar);
                    break;
                case 4:
                    e.Handled = !clsValidation.IsNumberOrLetterOrSymbol(e.KeyChar);
                    break;

                default:
                    e.Handled = !clsValidation.IsNumberOrLetter(e.KeyChar);
                    break;

            }
        }
    }
}
