using System;
using DVLD.DataAccess;
using System.Data;


namespace DVLD.Business
{
    public class clsApplication
    {

        public enum enMode { AddNew, Update };
        public enum enApplicationStatus { New =1 , Cancelled = 2 ,Completed = 3  }

        public int ApplicationID { set; get; }
        public int ApplicantPersonID { set; get; }
        public int ApplicationTypeID { set; get; }
        public clsApplicationTypes ApplicationTypeInfo;
        public int CreatedByUserID { set; get; }
        public clsUsers CreatedByUserInfo;
        public enApplicationStatus ApplicationStatus { set; get; }
        public double PaidFees { set; get; }
        public DateTime ApplicationDate { set; get; }
        public DateTime LastStatusDate { set; get; }

        public enMode Mode { set; get; }

        public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }

        protected clsApplication(int ApplicationID , int ApplicantPersonID , int ApplicationTypeID , int CreatedByUserID ,
                enApplicationStatus ApplicationStatus , double PaidFees , DateTime ApplicationDate , DateTime LastStatusDate)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.Find(ApplicationTypeID);
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUsers.Find(CreatedByUserID);
            this.ApplicationStatus =ApplicationStatus;
            this.PaidFees = PaidFees;
            this.ApplicationDate = ApplicationDate;
            this.LastStatusDate = LastStatusDate;

            this.Mode = enMode.Update;
        }

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationTypeID = -1;
            this.CreatedByUserID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.PaidFees = 0;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;

            this.Mode = enMode.AddNew;
        }

        public static clsApplication Find(int ApplicationID)
        {

            int ApplicantPersonID = -1;
            int ApplicationTypeID = -1;
            int CreatedByUserID = -1;
            int ApplicationStatus =(int)enApplicationStatus.New;
            double PaidFees = 0;
            DateTime ApplicationDate = DateTime.Now;
            DateTime LastStatusDate = DateTime.Now;


            if (clsApplicationData.FindApplicationByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate,
                ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))
            {
                return new clsApplication( ApplicationID ,ApplicantPersonID,ApplicationTypeID , CreatedByUserID
                    ,(enApplicationStatus)ApplicationStatus ,PaidFees , ApplicationDate , LastStatusDate );
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddApplicaton(this.ApplicantPersonID , this.ApplicationDate , 
                this.ApplicationTypeID ,(int)this.ApplicationStatus , this.LastStatusDate , this.PaidFees , this.CreatedByUserID);

            if (this.ApplicationID != -1)
                return true;
            else
                return false;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(this.ApplicationID , this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (int)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool DeleteBase()
        {
            return clsApplicationData.DeleteApplication(this.ApplicationID);
        }

        public static bool IsApplicationExistByID(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExistByID(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
          return clsApplicationData.GetActiveApplicationID(PersonID, ApplicationTypeID);
        }
     
        public bool SaveBase()
        {
            switch (this.Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    return _UpdateApplication();


            }
            return false;

        }

        public bool Cancel()
        {
            return clsApplicationData.ChangeApplicationStatus(ApplicationID, (int)enApplicationStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return clsApplicationData.ChangeApplicationStatus(ApplicationID, (int)enApplicationStatus.Completed);
        }
    }
}
