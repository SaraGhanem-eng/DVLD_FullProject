using DVLD.Business;
using DVLD.License.Local_License;
using DVLD.License.Local_License.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmShowPersonLicenseHistory : Form
    {
        private int _PersonID;
        public frmShowPersonLicenseHistory(int PersonID )
        {
            InitializeComponent();
            _PersonID = PersonID;
        }
        private void _LoadLocalDrivingLicenses()
        {
            clsDrivers Driver = clsDrivers.FindByPersonID(_PersonID);
            if (Driver == null)
                return;

            dgvLocalLicensesHistory.DataSource = clsLicense.GetDriverLicenses(Driver.DriverID);
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();


     
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            clsPerson _Person = clsPerson.Find(_PersonID);
            if (_Person == null )
            {
                MessageBox.Show("This Person Is Not Exist ! ");
                this.Close();
                return;
            }    
            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            _LoadLocalDrivingLicenses();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 90;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 80;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 300;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 150;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 150;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 90;

            }

            //dgvInternationalLicensesHistory.DataSource = 

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            if (LicenseID != -1)
            {
                frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);
                frm.ShowDialog();

            }
        }
    }
}
