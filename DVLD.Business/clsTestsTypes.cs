using System;
using System.Data;
using System.Runtime.CompilerServices;
using DVLD.DataAccess;

namespace DVLD.Business
{
    public class clsTestsTypes
    {

        public int TestTypeID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public double Fees { set; get; }

        public clsTestsTypes()
        {
            TestTypeID = -1;
            TestTypeTitle = "";
            TestTypeDescription = "";
            Fees = 0;
        }

        private clsTestsTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription ,double Fees)
        {
            this.TestTypeID = TestTypeID ;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.Fees = Fees;

        }

        public static clsTestsTypes Find(int TestTypeID)
        {

            string TestTypeTilte = "";
            string TestTypeDescription = "";
            double Fees = 0;


            if (clsTestsTypesData.FindTestTypeByID(TestTypeID ,ref TestTypeTilte , ref TestTypeDescription, ref Fees))
            {
                return new clsTestsTypes(TestTypeID ,TestTypeTilte , TestTypeDescription , Fees );
            }
            else
            {
                return null;
            }

        }

        public bool UpdateTestType()
        {
            return (clsTestsTypesData.UpdateTestType(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription ,this.Fees));
        }

        public static DataTable GetTestTypes()
        {
            return clsTestsTypesData.GetTestsTypes();
        }


    }
}
