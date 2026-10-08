using DVLD.DataAccess;
using System;
using System.Data;
using System.Net;


namespace DVLD.Business
{

    public class clsPerson
    {

        public enum enMode { AddNew , Update};

        public int _PersonID { set; get; }
        public string _NationalNo { set; get; }
        public string _FirstName { set; get; }
        public string _SecondName { set; get; }
        public string _ThirdName { set; get; }
        public string _LastName { set; get; }
        public DateTime _DateOfBirth { set; get; }
        public string _Address { set; get; }
        public int _Gender { set; get; }
        public string _Phone { set; get; }
        public string _Email { set; get; }
        public int _NationalityCountryID { set; get; }
        public string _ImagePath { set; get; }
        public enMode _Mode { set; get; }


        private clsPerson(int PersonID , string NationalNo, string FirstName,
                                           string SecondName, string ThirdName, string LastName,
                                           DateTime DateOfBirth, int Gender, string Address, string Phone,
                                           string Email, int NationalityCountryID, string ImagePath)
        {
            this._PersonID = PersonID;
            this._NationalNo = NationalNo;
            this._FirstName = FirstName;
            this._SecondName = SecondName;
            this._ThirdName = ThirdName;
            this._LastName = LastName;
            this._DateOfBirth = DateOfBirth;
            this._Address = Address;
            this._Gender = Gender;
            this._Phone = Phone;
            this._Email = Email;
            this._ImagePath = ImagePath;
            this._NationalityCountryID = NationalityCountryID;

            this._Mode = enMode.Update;
        }

        public clsPerson()
        {
            this._NationalNo = "";
            this._FirstName = "";
            this._SecondName = "";
            this._ThirdName = "";
            this._LastName = "";
            this._DateOfBirth = DateTime.Now;
            this._Address = "";
            this._Gender = 1;
            this._Phone = "";
            this._Email = "";
            this._NationalityCountryID = 51;
            this._ImagePath = "";

            this._Mode = enMode.AddNew;
        }

        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            int Gender = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = 51;
            string ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPersonData.FindPersonByID(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName
                ,ref LastName,ref DateOfBirth, ref Gender,ref Address, ref Phone , ref Email, ref NationalityCountryID,ref ImagePath))
            {
                return new clsPerson(PersonID , NationalNo , FirstName , SecondName , ThirdName , LastName,
                    DateOfBirth , Gender , Address , Phone , Email , NationalityCountryID , ImagePath);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewPerson()
        {
            this._PersonID = clsPersonData.AddPerson(this._NationalNo, this._FirstName, this._SecondName, this._ThirdName
                , this._LastName, this._DateOfBirth, this._Gender, this._Address, this._Phone, this._Email,
                 this._NationalityCountryID, this._ImagePath);

            if (this._PersonID != -1)
                return true;
            else
                return false;
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this._PersonID, this._NationalNo, this._FirstName, this._SecondName, this._ThirdName
                , this._LastName, this._DateOfBirth, this._Gender, this._Address, this._Phone, this._Email,
                 this._NationalityCountryID, this._ImagePath);
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetPeopleList();
        }

        public static DataTable FilterBy(string ColumnName , string SearchString)
        {
            return clsPersonData.FilterBy(ColumnName, SearchString);
        }

        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExistByID(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExistByNationalNo(NationalNo);
        }

        public static bool IsPersonExistExceptPersonID(string NationalNo, int PersonID)
        {
            return clsPersonData.IsPersonExistByNationalNoExceptPersonID(
                NationalNo, PersonID);
        }
        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.AddNew :
                {
                   if(_AddNewPerson())
                   {
                     _Mode = enMode.Update;
                     return true;
                   }
                   else
                   {
                     return false;
                   }
                }
                case enMode.Update:
                   return _UpdatePerson();
                    

            }
            return false;

        }
    
    
    }
}
