using DVLD.Applications.Local_Driving_License;
using DVLD.Business;
using DVLD.Classes;
using DVLD.Properties;
using DVLD.Tests.Schedual_Tests;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace DVLD.Tests.Schedual_Tests
{
    public partial class frmTestAppointment : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public clsTestAppointment.enScheduleTestMode _AppointmentMode;
        private DataTable _dtTestAppointments;
        private clsTestsTypes.enTestType _TestType;
        public frmTestAppointment(int LocalDrivingLicenseApplicationID , clsTestsTypes.enTestType TestType)
        {
            InitializeComponent();
            if (LocalDrivingLicenseApplicationID == -1)
                return;

            _TestType = TestType;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void _LoadMood()
        {
            switch(_TestType)
            {
                case clsTestsTypes.enTestType.VisionTest:
                    {
                        PBTestType.Image = Resources.Vision_512;
                        lblTestType.Text = "Vision Test";
                        this.Text = "Vision Test Appointments";
                        break;
                    }
                case clsTestsTypes.enTestType.WrittenTest:
                    {
                        PBTestType.Image = Resources.Written_Test_512;
                        lblTestType.Text = "Written Test";
                        this.Text = "Written Test Appointments";
                        break;
                    }

                case clsTestsTypes.enTestType.StreetTest:
                    {
                        PBTestType.Image = Resources.driving_test_512;
                        lblTestType.Text = "Street Test";
                        this.Text = "Street Test Appointments";
                        break;
                    }

            }
        }

        private void _LoadTestAppointmentList()
        {
            _dtTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, _TestType);
            dgvLicenseTestAppointments.DataSource = _dtTestAppointments;
            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 200;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }

        }
       
        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadMood();

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication != null)
            {
                ctrlLocalApplicationInfo1.LoadLocalDrivingApplicationInfo(_LocalDrivingLicenseApplicationID);
            }
            else
            {
                MessageBox.Show("Local Application is not exist !");
                this.Close();
            }
            _LoadTestAppointmentList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTests.HasPassedTest(_LocalDrivingLicenseApplicationID ,(int)_TestType))
            {
                MessageBox.Show("This Test is Passed Successfully , You Can't Make Another Appointment ", "Completed Test !",
                MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (clsTestAppointment.DoesHaveAvailableAppointment(_LocalDrivingLicenseApplicationID ,(int)_TestType))
            {
                MessageBox.Show("There is Already An Availabel Appointment , Can not Book Another One " , "Exist Available Appointment" , 
                MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }

            clsTests Test = clsTests.FindLastTestByLocalDrivingLicenseApplicationAndTestTypeID(_LocalDrivingLicenseApplicationID, _TestType);


            if (Test == null)
            {
                _AppointmentMode = clsTestAppointment.enScheduleTestMode.TakeTest;

            }
            else 
            {
                _AppointmentMode = clsTestAppointment.enScheduleTestMode.RetakeTest;
            }

            frmSchedualeTest frm = new frmSchedualeTest(_LocalDrivingLicenseApplicationID , _TestType , _AppointmentMode , -1);
            frm.ShowDialog();
            _LoadTestAppointmentList();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            clsTestAppointment Appointment = clsTestAppointment.Find(TestAppointmentID);

            if (Appointment == null)
            {
                MessageBox.Show("Appointment not found!");
                return;
            }

            if (Appointment.LocalDrivingLicenseApplicationID != _LocalDrivingLicenseApplicationID ||
               Appointment.TestTypeID != _TestType)
            {
                MessageBox.Show("Invalid Appointment!");
                return;
            }

            if (Appointment.RetakeTestApplicationID == -1)
                _AppointmentMode = clsTestAppointment.enScheduleTestMode.TakeTest;
            else
                _AppointmentMode = clsTestAppointment.enScheduleTestMode.RetakeTest;

            frmSchedualeTest frm = new frmSchedualeTest(
                _LocalDrivingLicenseApplicationID,
                _TestType,
                _AppointmentMode,
                TestAppointmentID);

            frm.ShowDialog();

            _LoadTestAppointmentList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;

            clsTestAppointment Appointment = clsTestAppointment.Find(TestAppointmentID);

            if (Appointment == null)
            {
                MessageBox.Show("Appointment not found!");
                return;
            }

            if (Appointment.IsLocked || Appointment.AppointmentDate < DateTime.Today)
            {
                MessageBox.Show("This Appointment is locked ,You Can not Take It's Test ", "Can Not Take Test !",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Appointment.LocalDrivingLicenseApplicationID != _LocalDrivingLicenseApplicationID ||
               Appointment.TestTypeID != _TestType)
            {
                MessageBox.Show("Invalid Appointment!");
                return;
            }


            frmTakeTest frm = new frmTakeTest(TestAppointmentID);
            frm.ShowDialog();

            _LoadTestAppointmentList();
        }
    }
}
