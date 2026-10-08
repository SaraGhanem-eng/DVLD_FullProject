using System;
using System.Data;
using DVLD.DataAccess;

namespace DVLD.Business
{
    public class clsCountry
    {

        public int _CountryID { get; set; }
        public string _CountryName { get; set; }

        private clsCountry(int CountryID , string CountryName )
        {
            this._CountryID = CountryID;
            this._CountryName = CountryName;
        }

        static public DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }



        static public clsCountry FindByCountryID(int ID)
        {

            string CountryName = "";

            if (clsCountryData.FindCountryByID(ID, ref CountryName))
            {

                return new clsCountry(ID, CountryName);

            }
            else
            {
                return null;
            }

        }

        static public clsCountry FindByCountryName(string CountryName)
        {
            int ID = -1;

            if (clsCountryData.FindCountryByCountryName(CountryName, ref ID))
            {

                return new clsCountry(ID, CountryName);

            }
            else
            { 
                return null;
            }

        }



    }
}
