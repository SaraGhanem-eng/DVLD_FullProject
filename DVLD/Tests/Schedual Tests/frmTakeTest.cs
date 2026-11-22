using DVLD.Business;
using DVLD.Classes;
using DVLD.GLobal_Classes;
using DVLD.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.Business.clsTestsTypes;

namespace DVLD.Tests.Schedual_Tests
{
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID;
        private clsTestAppointment _TestAppointmentInfo;

        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
        }

        private void _LoadTestType()
        {
            switch (_TestAppointmentInfo.TestTypeID)
            {
                case clsTestsTypes.enTestType.VisionTest:
                    {
                        pbTestTypeImage.Image = Resources.Vision_512;
                        this.Text = "Take Vision Test";
                        break;
                    }
                case clsTestsTypes.enTestType.WrittenTest:
                    {
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        this.Text = "Take Written Test";
                        break;
                    }

                case clsTestsTypes.enTestType.StreetTest:
                    {
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        this.Text = "Take Street Test ";
                        break;
                    }

            }
        }
        private void _LoadAppointmentData()
        {
            _TestAppointmentInfo = clsTestAppointment.Find(_TestAppointmentID);

            if (_TestAppointmentInfo == null)
            {
                MessageBox.Show("This Appointment is not exist !");
                this.Close();
                return;

            }
            int _LocalDrivingLicenseApplicationID;
            

            _LocalDrivingLicenseApplicationID = _TestAppointmentInfo.LocalDrivingLicenseApplicationID;
            clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = "
                    + _LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;
            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialPerTest(_LocalDrivingLicenseApplicationID, _TestAppointmentInfo.TestTypeID).ToString();
            lblDate.Text = clsFormat.GetDateString(_TestAppointmentInfo.AppointmentDate);
            lblFees.Text = _TestAppointmentInfo.PaidFees.ToString();

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadAppointmentData();

            if (_TestAppointmentInfo == null)
                return;

            _LoadTestType();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           if ( MessageBox.Show("Are you sure you want to save results ? \n It Can Not Be Changed", "Confrim",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (!rbPass.Checked && !rbFail.Checked)
            {
                MessageBox.Show("Please select the test result!",
                    "Missing Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            clsTests Test = new clsTests();
            Test.TestAppointmentID = _TestAppointmentID;
            Test.TestResult = (rbPass.Checked);
            Test.Notes = txtNotes.Text;
            Test.CreatedByUserID = clsGlobalInfo.CurrentUser.UserID;

            if (Test.Save())
            {
                MessageBox.Show("Test Result Saved Successfully !", "Saved successfully",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {

                MessageBox.Show("Failed to save !", "Failed ! ",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 

        }
    }
}
