using DVLD.Business;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Tests.Schedual_Tests
{
    public partial class frmSchedualeTest : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;
        private int _LocalDrivingLicenseApplicationID;

        private int _AppointmentID;
        private clsTestAppointment _Appointment;
        private clsTestAppointment.enScheduleTestMode _AppointmentMode;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        private clsTestsTypes.enTestType _TestTypeID;

      
        public frmSchedualeTest(int LocalDrivingLicenseApplicationID , clsTestsTypes.enTestType TestTypeID , clsTestAppointment.enScheduleTestMode Mode , int AppointmentID )
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _AppointmentMode = Mode;
            _AppointmentID = AppointmentID;

            if (_AppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private bool _LoadLocalApplicationInfo()
        {
            //  Load Local Application
            _LocalDrivingLicenseApplicationInfo =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(
                    _LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null)
            {
                MessageBox.Show("This Application Is Not Exist !");
                this.Close();
                return false;
            }

            //  Default Controls State
            btnSave.Enabled = true;
            dtpTestDate.Enabled = true;
            lblLocked.Visible = false;

            //  Handle Appointment Date
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        dtpTestDate.MinDate = DateTime.Today;
                        dtpTestDate.Value = DateTime.Today.AddDays(1);
                        break;
                    }

                case enMode.Update:
                    {
                        _Appointment = clsTestAppointment.Find(_AppointmentID);

                        if (_Appointment == null)
                        {
                            MessageBox.Show("This Appointment Is Not Exist !");
                            this.Close();
                            return false;
                        }
                        if (_Appointment.LocalDrivingLicenseApplicationID != _LocalDrivingLicenseApplicationID || _Appointment.TestTypeID != _TestTypeID)
                        {
                            MessageBox.Show("Invalid Appointment Context!");
                            this.Close();
                            return false;
                        }

                        bool IsLocked =
                            _Appointment.IsLocked ||
                            _Appointment.AppointmentDate < DateTime.Now;

                        if (IsLocked)
                        {
                            dtpTestDate.Value = _Appointment.AppointmentDate;
                            btnSave.Enabled = false;
                            dtpTestDate.Enabled = false;
                            lblLocked.Visible = true;
                        }
                        else
                        {
                            dtpTestDate.MinDate = DateTime.Today;
                            dtpTestDate.Value = _Appointment.AppointmentDate;
                        }

                        break;
                    }
            }

            //  Display Local Application Information
            lblLocalDrivingLicenseAppID.Text =
                _LocalDrivingLicenseApplicationInfo.LocalDrivingLicenseApplicationID.ToString();

            lblFullName.Text =
                _LocalDrivingLicenseApplicationInfo.ApplicantFullName;

            clsLicenseClass LicenseClass =
                clsLicenseClass.Find(_LocalDrivingLicenseApplicationInfo.LicenseClassID);

            lblDrivingClass.Text = LicenseClass.ClassName;

            clsTestsTypes Test = clsTestsTypes.Find(_TestTypeID);

            lblFees.Text = Test.Fees.ToString();

            lblTrial.Text = clsLocalDrivingLicenseApplication.TotalTrialPerTest(_LocalDrivingLicenseApplicationID, _TestTypeID).ToString();

            return true;
        }

        private void _LoadRetakeApplicationInfo()
        {
            // Load Test Fees
            clsTestsTypes Test = clsTestsTypes.Find(_TestTypeID);

            // Handle Appointment Mode
            switch (_AppointmentMode)
            {
                case clsTestAppointment.enScheduleTestMode.TakeTest:
                    {
                        lblTotalFees.Text = Test.Fees.ToString();
                        lblRetakeAppFees.Text = 0.ToString();
                        lblRetakeTestAppID.Text = "N/A";

                        break;
                    }

                case clsTestAppointment.enScheduleTestMode.RetakeTest:
                    {
                        clsApplicationTypes ApplicationType =
                            clsApplicationTypes.Find(
                                (int)clsApplicationTypes.enApplicationType.RetakeTest);

                        double TotalFees = Test.Fees + ApplicationType.Fees;

                        lblRetakeAppFees.Text = ApplicationType.Fees.ToString();
                        lblTotalFees.Text = TotalFees.ToString();

                        // Display Retake Application ID
                        if (_Mode == enMode.AddNew)
                            lblRetakeTestAppID.Text = "N/A";
                        else
                            lblRetakeTestAppID.Text =
                                _Appointment.RetakeTestApplicationID.ToString();

                        break;
                    }
            }
        }

        private void _LoadTestMode()
        {
            switch (_AppointmentMode)
            {
                case clsTestAppointment.enScheduleTestMode.TakeTest:
                    {
                        lblTitle.Text = "Schedule Test";
                        gbRetakeTestInfo.Enabled = false;
                        break;
                    }
                case clsTestAppointment.enScheduleTestMode.RetakeTest:
                    {
                        lblTitle.Text = "Schedule Retake Test";
                        gbRetakeTestInfo.Enabled = true;
                        break;
                    }
            }

        }

        private void _LoadTestType()
        {
            switch (_TestTypeID)
            {
                case clsTestsTypes.enTestType.VisionTest:
                    {
                        PBTestType.Image = Resources.Vision_512;
                        this.Text = "Schedula Vision Test";
                        break;
                    }
                case clsTestsTypes.enTestType.WrittenTest:
                    {
                        PBTestType.Image = Resources.Written_Test_512;
                        this.Text = "Schedule Written Test";
                        break;
                    }

                case clsTestsTypes.enTestType.StreetTest:
                    {
                        PBTestType.Image = Resources.driving_test_512;
                        this.Text = "Schedule Street Test ";
                        break;
                    }

            }
        }

        private bool _ArePrerequisiteTestsPassed()
        {
            switch (_TestTypeID)
            {
                case clsTestsTypes.enTestType.VisionTest:
                    return true;

                case clsTestsTypes.enTestType.WrittenTest:
                    return clsTests.HasPassedTest(
                        _LocalDrivingLicenseApplicationID,
                        (int)clsTestsTypes.enTestType.VisionTest);

                case clsTestsTypes.enTestType.StreetTest:
                    return clsTests.HasPassedTest(
                        _LocalDrivingLicenseApplicationID,
                        (int)clsTestsTypes.enTestType.WrittenTest);

                default:
                    return false;
            }
        }
        private void frmSchedualeTest_Load(object sender, EventArgs e)
        {
            _LoadTestMode();
            _LoadTestType();
            if (!_LoadLocalApplicationInfo())
                return; 
            _LoadRetakeApplicationInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dtpTestDate.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Appointment Date Must Be In The Future!",
                    "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (_Mode)
            {
                
                case enMode.AddNew:
                {
                        // Revalidate Costraints befor set new appointment 

                        if (!_ArePrerequisiteTestsPassed())
                        {
                            MessageBox.Show(
                                "You Must Pass The Previous Test First!",
                                "Prerequisite Test Not Passed",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            this.Close();
                            return;
                        }

                        if (clsTestAppointment.DoesHaveAvailableAppointment(_LocalDrivingLicenseApplicationID,(int)_TestTypeID))
                        {
                            MessageBox.Show("There is Already Available Appointment sat on the system ",
                            "Can Not Add Another Appointment !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                            return;
                        }

                        if (clsTests.HasPassedTest(_LocalDrivingLicenseApplicationID,(int)_TestTypeID))
                        {
                            MessageBox.Show("This Test is Passed Successfully , You Can't Make Another Appointment ", "Completed Test !",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        clsTestAppointment Appointment = new clsTestAppointment();
                        Appointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
                        Appointment.TestTypeID = _TestTypeID;
                        Appointment.AppointmentDate = dtpTestDate.Value;
                        Appointment.PaidFees =(float)clsTestsTypes.Find(_TestTypeID).Fees;
                        Appointment.CreatedByUserID = clsGlobalInfo.CurrentUser.UserID;
                        Appointment.IsLocked = false;
                        Appointment.RetakeTestApplicationID = -1;

                 if (_AppointmentMode == clsTestAppointment.enScheduleTestMode.TakeTest)
                 {
                   if (Appointment.Save())
                   {
                     MessageBox.Show("Appointment Saved Successfully !",
                     " Date Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     _Mode = enMode.Update;
                     this.Close();
                     return;
                   }
                   else
                   {
                     MessageBox.Show("Failed To save Appointment !",
                     " Saved Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                   }
                 }
                 else if(_AppointmentMode == clsTestAppointment.enScheduleTestMode.RetakeTest)
                 {
                            clsApplication RetakeApplication = new clsApplication();
                            RetakeApplication.ApplicantPersonID = _LocalDrivingLicenseApplicationInfo.ApplicantPersonID;
                            RetakeApplication.ApplicationTypeID = (int)clsApplicationTypes.enApplicationType.RetakeTest;
                            RetakeApplication.CreatedByUserID =clsGlobalInfo.CurrentUser.UserID;RetakeApplication.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                            RetakeApplication.PaidFees = clsApplicationTypes.Find( (int)clsApplicationTypes.enApplicationType.RetakeTest).Fees;
                            RetakeApplication.ApplicationDate = DateTime.Now;
                            RetakeApplication.LastStatusDate = DateTime.Now;


                            if (!RetakeApplication.SaveBase())
                            {
                                MessageBox.Show("Failed To Create Retake Application.");
                                return;
                            }

                            Appointment.RetakeTestApplicationID = RetakeApplication.ApplicationID;
                            if (Appointment.Save())
                            {
                                MessageBox.Show("Appointment Saved Successfully !",
                                " Date Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Failed To save Appointment !",
                                " Saved Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                   break;
                }
                case enMode.Update:
                {
                      

                        if (_Appointment == null || _Appointment.IsLocked ||
                        _Appointment.AppointmentDate < DateTime.Now)
                        {
                            MessageBox.Show("This Appointment Cannot Be Modified!");
                            return;
                        }
                        _Appointment.AppointmentDate = dtpTestDate.Value;

                        if (_Appointment.Save())
                        {
                            MessageBox.Show("Appointment Saved Successfully !",
                            " Date Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Failed To save Appointment !",
                            " Saved Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        
                }
            }
        }
    }
}
