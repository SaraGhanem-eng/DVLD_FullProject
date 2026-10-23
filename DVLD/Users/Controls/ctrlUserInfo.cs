using DVLD.Business;
using DVLD.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class ctrlUserInfo : UserControl
    {

        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
        }
       
        public void LoadUserInfo(int UserID)
        {
            clsUsers User = clsUsers.Find(UserID);
            if (User != null)
            {
                _UserID = User.UserID;
                clsPerson Person = clsPerson.Find(User.PersonID);

                if (Person != null)
                {
                    ctrlPersonCard1.LoadPersonInfo(Person);
                }

                lblUserID.Text = User.UserID.ToString();
                lblUserName.Text = User.UserName.ToString();
                lblIsActive.Text = User.IsActive.ToString();


            }

        }

    }
}
