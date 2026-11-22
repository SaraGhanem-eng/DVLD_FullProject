using DVLD.Business;
using DVLD.Classes;
using DVLD.Tests.Schedual_Tests;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicenseApplications : Form
    {
        DataTable _dtLocalDrivingLicenseApplicationsList;
        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _Load()
        {
            _dtLocalDrivingLicenseApplicationsList = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();

            dgvLocalDrivingLicenseApplications.DataSource = _dtLocalDrivingLicenseApplicationsList;
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();

        }

        private void _InitializeDataGridView()
        {

            if (dgvLocalDrivingLicenseApplications.Columns.Count > 0)
            {
                dgvLocalDrivingLicenseApplications.ReadOnly = true;

                dgvLocalDrivingLicenseApplications.AllowUserToAddRows = false;
                dgvLocalDrivingLicenseApplications.AllowUserToDeleteRows = false;

                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 210;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 110;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 200;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 100;

                dgvLocalDrivingLicenseApplications.Columns[6].HeaderText = "Status";
                dgvLocalDrivingLicenseApplications.Columns[6].Width = 700;
            }

        }
       
        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _Load();
            _InitializeDataGridView();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = "";
            txtFilterValue.Enabled = false;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
                txtFilterValue.Enabled = false;
            else
                txtFilterValue.Enabled = true;

            _dtLocalDrivingLicenseApplicationsList.DefaultView.RowFilter = "";
            txtFilterValue.Clear();
            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.RowCount.ToString();

        }
       
        private string _GetSelectedColumn(int SelectedIndex)
        {
            switch (SelectedIndex)
            {
                case 0:
                    return "None";
                case 1:
                    return "LocalDrivingLicenseApplicationID";
                case 2:
                    return "NationalNo";
                case 3:
                    return "FullName";
                case 4:
                    return "Status";
                default:
                    return "None";
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = _GetSelectedColumn(cbFilterBy.SelectedIndex);

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtLocalDrivingLicenseApplicationsList.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                //in this case we deal with integer not string.

                _dtLocalDrivingLicenseApplicationsList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtLocalDrivingLicenseApplicationsList.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void btnAddApplication_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDrivingLicenseApplication frm = new frmAddEditLocalDrivingLicenseApplication();
            frm.ShowDialog();
            _Load();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                int LocalDrivingLicenseAppID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);


                if (LocalDrivingLicenseAppID != -1)
                {
                    frmAddEditLocalDrivingLicenseApplication frm = new frmAddEditLocalDrivingLicenseApplication(LocalDrivingLicenseAppID);
                    frm.ShowDialog();
                    _Load();
                }

            }
        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            if (MessageBox.Show("Are you sure do want to Delete this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Delete())
                {
                    MessageBox.Show("Application Deleted Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Load();
                    // optimization : make the item Cancel in the cmstripe disabled 
                }
                else
                {
                    MessageBox.Show("Could not Delete applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to cancel this application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                if (LocalDrivingLicenseApplication.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //refresh the form again.
                    _Load();
                }
                else
                {
                    MessageBox.Show("Could not cancel applicatoin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocalDrivingLicenseApplications.CurrentRow != null)
            {
                int LocalDrivingLicenseApplicationID = Convert.ToInt32(dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value);
                clsLocalDrivingLicenseApplication _LocalApplication =  clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);

                if (_LocalApplication != null)
                {
                    frmShowLocalApplicationInfo frm = new frmShowLocalApplicationInfo(LocalDrivingLicenseApplicationID);
                    frm.ShowDialog();
                    _Load();
                }
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
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 4:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;


                default:
                    e.Handled = !clsValidation.IsNumberOrLetter(e.KeyChar);
                    break;
        }

    }

        private void _ScheduleTest(clsTestsTypes.enTestType TestType)
        {

            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            frmTestAppointment frm = new frmTestAppointment(LocalDrivingLicenseApplicationID, TestType);
            frm.ShowDialog();
            _Load();

        }

        private void cmsApplications_Opening_1(object sender, CancelEventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[0].Value;
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication =
                    clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID
                                                    (LocalDrivingLicenseApplicationID);

            int TotalPassedTests = (int)dgvLocalDrivingLicenseApplications.CurrentRow.Cells[5].Value;

            //bool LicenseExists = LocalDrivingLicenseApplication.IsLicenseIssued();


            //issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = (TotalPassedTests == 3) && !LicenseExists;

            // showLicenseToolStripMenuItem.Enabled = LicenseExists;
            // editToolStripMenuItem.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            // ScheduleTestsMenue.Enabled = !LicenseExists;


            CancelApplicaitonToolStripMenuItem.Enabled = (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);


            DeleteApplicationToolStripMenuItem.Enabled =
                (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);



            bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestsTypes.enTestType.VisionTest); ;
            bool PassedWrittenTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestsTypes.enTestType.WrittenTest);
            bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassTestType(clsTestsTypes.enTestType.StreetTest);

            ScheduleTestsMenue.Enabled = (!PassedVisionTest || !PassedWrittenTest || !PassedStreetTest) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

            if (ScheduleTestsMenue.Enabled)
            {

                scheduleVisionTestToolStripMenuItem.Enabled = !PassedVisionTest;

                scheduleWrittenTestToolStripMenuItem.Enabled = PassedVisionTest && !PassedWrittenTest;

                scheduleStreetTestToolStripMenuItem.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;

            }

        }

        private void scheduleVisionTestToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestsTypes.enTestType.VisionTest);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestsTypes.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestsTypes.enTestType.StreetTest);
        }
    }
}
