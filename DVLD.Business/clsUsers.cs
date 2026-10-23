using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Business
{
    public class clsUsers
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
        public int PersonID { set; get; }

        enum enMode { AddNew , Update};
        private enMode _Mode;

        private clsUsers ( int UserID, int PersonID , string UserName , string Password , bool IsActive  )
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this._Mode = enMode.Update;
        }

        public clsUsers ()
        {

            this.PersonID = -1;
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true ;

            this._Mode = enMode.AddNew;
        }

        public static clsUsers Find (int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false ;

            if (clsUsersData.FindUserByID(UserID, ref PersonID, ref UserName, ref Password ,ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }

        }

        public static clsUsers Find (string UserName , string Password )
        {
            int PersonID = -1;
            int UserID = -1;
            bool IsActive = false;

            if (clsUsersData.FindUserByUserNameAndPassword(UserName, Password , ref UserID , ref PersonID, ref IsActive))
            {
                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersData.AddUser(PersonID, UserName, Password, IsActive);

            if (this.UserID != -1)
                return true;
            else
                return false;
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetUsersList();
        }

        public static bool IsUserExistByUserID(int UserID)
        {
            return clsUsersData.IsUserExistByID(UserID);
        }

        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUsersData.IsUserExistByUserName(UserName);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUsersData.IsUserExistByPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID , string Password)
        {
            return clsUsersData.ChangePassword(UserID, Password);
        }

        public static bool IsUserNameExistExceptUserID(string UserName , int UserID )
        {
            return clsUsersData.IsUserNameExistExceptUserID(UserName, UserID);
        }
        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    {
                        if (!IsUserNameExistExceptUserID(this.UserName , this.UserID) && clsPerson.IsPersonExist(this.PersonID) && !IsUserExistByPersonID(this.PersonID))
                        {
                            if (_AddNewUser())
                            {
                                _Mode = enMode.Update;
                                return true;
                            }

                        }
                        return false;
                    }
                case enMode.Update:
                    {
                        if (!IsUserNameExistExceptUserID(this.UserName , this.UserID))
                        {
                            return _UpdateUser();
                        }
                        return false;
                    }



            }
            return false;

        }

    }
}
