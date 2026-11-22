using System;
using System.Data;
using System.Runtime.CompilerServices;
using DVLD.DataAccess;


namespace DVLD.Business
{
    public class clsApplicationTypes
    {

        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enApplicationType ApplicationTypeID { set; get; }
        public string ApplicationTypeTitle { set; get; }
        public double Fees { set; get; }

        public clsApplicationTypes()
        {
            ApplicationTypeID = enApplicationType.NewDrivingLicense;
            ApplicationTypeTitle = "";
            Fees = 0;
        }

        private clsApplicationTypes(int ApplicationTypeID , string ApplicationTypeTitle , double Fees )
        {
            this.ApplicationTypeID =(enApplicationType) ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.Fees = Fees;

        }

        public static clsApplicationTypes Find(int ApplicationTypeID)
        {

            string ApplicationTypeTilte = "";
            double Fees = 0;


            if (clsApplicationTypesData.FindApplicationTypeByID(ApplicationTypeID, ref ApplicationTypeTilte , ref Fees ))
            {
                return new clsApplicationTypes(ApplicationTypeID , ApplicationTypeTilte , Fees);
            }
            else
            {
                return null;
            }

        }

        public  bool UpdateApplicationType()
        {
            return (clsApplicationTypesData.UpdateApplicationType((int)this.ApplicationTypeID, this.ApplicationTypeTitle, this.Fees)); 
        }

        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }


    }
}
