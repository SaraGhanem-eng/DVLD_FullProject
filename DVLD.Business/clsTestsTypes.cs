using System;
using System.Data;
using System.Runtime.CompilerServices;
using DVLD.DataAccess;

namespace DVLD.Business
{
    public class clsTestsTypes
    {

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        public clsTestsTypes.enTestType ID { set; get; }
        public string TestTypeTitle { set; get; }
        public string TestTypeDescription { set; get; }
        public double Fees { set; get; }

        public clsTestsTypes()
        {

            ID = clsTestsTypes.enTestType.VisionTest;
            TestTypeTitle = "";
            TestTypeDescription = "";
            Fees = 0;
        }

        private clsTestsTypes(clsTestsTypes.enTestType ID, string TestTypeTitle, string TestTypeDescription ,double Fees)
        {
            this.ID = ID ;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.Fees = Fees;

        }

        public static clsTestsTypes Find( clsTestsTypes.enTestType TestTypeID)
        {

            string TestTypeTilte = "";
            string TestTypeDescription = "";
            double Fees = 0;


            if (clsTestsTypesData.FindTestTypeByID((int)TestTypeID ,ref TestTypeTilte , ref TestTypeDescription, ref Fees))
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
            return (clsTestsTypesData.UpdateTestType((int)this.ID, this.TestTypeTitle, this.TestTypeDescription ,this.Fees));
        }

        public static DataTable GetTestTypes()
        {
            return clsTestsTypesData.GetTestsTypes();
        }


    }
}
