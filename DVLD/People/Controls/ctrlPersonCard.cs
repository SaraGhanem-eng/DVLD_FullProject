using System;
using DVLD.Business;
using System.Windows.Forms;
using DVLD.Classes;
using System.IO;

namespace DVLD.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        private clsPerson _Person;
        private string _ImagesFolder = @"C:\DVLD-People-Images";
        public void LoadPersonInfo(clsPerson Person)
        {
            _Person = Person;

            lblPersonID.Text = _Person._PersonID.ToString();

            lblName.Text = _Person._FirstName.ToString() + " " +
            _Person._SecondName + " " + _Person._ThirdName+ " " + _Person._LastName ;

            lblNationalNo.Text = _Person._NationalNo;
            lblGender.Text = clsFormat.GetGenderName(_Person._Gender);
            lblAddress.Text = _Person._Address;
            lblDateOfBirth.Text = clsFormat.GetDateString(_Person._DateOfBirth);
            lblPhone.Text = _Person._Phone;
            lblEmail.Text = string.IsNullOrEmpty(_Person._Email)
                ? "N/A"
                : _Person._Email;
            clsCountry country = clsCountry.FindByCountryID(_Person._NationalityCountryID);
            lblCountry.Text = country._CountryName;

            if (_Person._ImagePath == "")
            {
                if (_Person._Gender == 0)
                {
                   
                    PBPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Male 512.png");
                }
                else if (_Person._Gender == 1)
                {
                    PBPerson.Load("C:\\Users\\sara\\Downloads\\Icons\\Icons\\Female 512.png");
                }
            }
            else
            {
                string FullImagePath = Path.Combine(_ImagesFolder, _Person._ImagePath);
                PBPerson.ImageLocation = FullImagePath;
               // PBPerson.Load(FullImagePath);
     
            }
        }

        private void llEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAdd_EditPersonInfo frm = new frmAdd_EditPersonInfo(_Person._PersonID);
            
            frm.ShowDialog();

            //reload the object from data base 
            _Person = clsPerson.Find(_Person._PersonID);
            LoadPersonInfo(_Person);
   
        }
    }
}
