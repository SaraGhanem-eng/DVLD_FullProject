using DVLD.DataAccess;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {

        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }

        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }

        }

        public clsLocalDrivingLicenseApplication()
        : base()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
        }

        private clsLocalDrivingLicenseApplication( int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
        int ApplicationTypeID, int CreatedByUserID, enApplicationStatus ApplicationStatus, double PaidFees, DateTime ApplicationDate,
        DateTime LastStatusDate, int LicenseClassID)
        : base( ApplicationID, ApplicantPersonID, ApplicationTypeID, CreatedByUserID, 
              ApplicationStatus, PaidFees, ApplicationDate, LastStatusDate)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(LicenseClassID);
        }

        static public clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID )
        {
            int ApplicationID = -1;
            int LicenceClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.FindLocalApplicationByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenceClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.Find(ApplicationID);
                if (Application == null)
                    return null;

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID , Application.ApplicationID , 
                    Application.ApplicantPersonID , Application.ApplicationTypeID , Application.CreatedByUserID , Application.ApplicationStatus,
                    Application.PaidFees , Application.ApplicationDate , Application.LastStatusDate , LicenceClassID);
            }
            return null;
        }

        static public clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            int LicenceClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.FindLocalApplicationByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenceClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.Find(ApplicationID);
                if (Application == null)
                    return null;

                return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID, Application.ApplicationTypeID, Application.CreatedByUserID, Application.ApplicationStatus,
                    Application.PaidFees, Application.ApplicationDate, Application.LastStatusDate, LicenceClassID);
            }
            return null;
        }
        
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddLocalApplication
                (this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalApplication
                (this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);

        }

        static public int TotalTrialPerTest(int LocalDrivingLicenseID , clsTestsTypes.enTestType TestTypeID )
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseID, (int)TestTypeID);
        }
        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            enMode CurrentMode = Mode;

            if (!base.SaveBase())
                return false;


            //After we save the main application now we save the sub application.
            switch (CurrentMode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }

            return false;
        }

        public bool Delete()
         {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.DeleteBase();
            return IsBaseApplicationDeleted;

        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationsList();
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int LicenseClassID)
        {
           return clsLocalDrivingLicenseApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, LicenseClassID);
        }

        public bool DoesPassTestType(clsTestsTypes.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
    }
}
