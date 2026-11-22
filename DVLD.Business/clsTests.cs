using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Business
{
    public class clsTests
    {


        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }

        public clsTests()

        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        private clsTests(int TestID, int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {

            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);

            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestsData.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static clsTests Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestsData.GetTestInfoByID(TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTests(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }

        public static clsTests FindLastTestByLocalDrivingLicenseApplicationAndTestTypeID(int LocalDrivingLicenseID , clsTestsTypes.enTestType TestTypeID )
        {
            int TestAppointmentID = -1; int TestID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestsData.GetLastTestByLocalDrivingLicenseApplicationIDAndTestTypeID(LocalDrivingLicenseID , (int)TestTypeID ,ref TestID ,
               ref TestAppointmentID ,ref TestResult ,ref Notes , ref CreatedByUserID))
                return new clsTests(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;
        }
        public static DataTable GetAllTests()
        {
            return clsTestsData.GetAllTests();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public int GetPassedTestCount()
        {
            return clsTestsData.GetPassedTestCount(TestAppointmentInfo.LocalDrivingLicenseApplicationID);
        }
        
        static public int GetPassedTestCount(int LocalDrivinLicenseApplicationID )
        {
            return clsTestsData.GetPassedTestCount(LocalDrivinLicenseApplicationID);
        }
        public bool DidPassAllTests()
        {
            return (clsTestsData.GetPassedTestCount(TestAppointmentInfo.LocalDrivingLicenseApplicationID) == 3);
        }
   
        static public bool HasPassedTest(int LocalDrivingLicenseID, int TestTypeID)
        {
            return clsTestsData.HasPassedTest(TestTypeID, LocalDrivingLicenseID);
        }
    }
}
