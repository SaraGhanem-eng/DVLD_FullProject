using DVLD.Business;
using DVLD.GLobal_Classes;
using DVLD.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddEditLocalDrivingLicenseApplication : Form
    {
        enum enMode { AddNew , Update };
        enMode _Mode;

        private int _LocalDrivingLicenseApplicationID = -1;

        private int _ApplicantPersonID = -1;

        private clsLocalDrivingLicenseApplication _LocalApplciation;
       
        public frmAddEditLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            ctrlPersonCardWithFilter1.OnPersonSelected +=
            CtrlPersonCardWithFilter1_OnPersonSelected;

        }

        private void CtrlPersonCardWithFilter1_OnPersonSelected(int SelectedPersonID)
        {
            if (SelectedPersonID == -1)
            {
                btnNext.Enabled = false;
                btnSave.Enabled = false;
                tabPage2.Enabled = false;
                return;
            }

            _ApplicantPersonID = SelectedPersonID;
            if (_Mode == enMode.AddNew)
            {
                btnNext.Enabled = true;
                btnSave.Enabled = true;
                tabPage2.Enabled = true;
            }
        }
        public frmAddEditLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            if (LocalDrivingLicenseApplicationID != -1)
            {
                _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;

            }

            _Mode = enMode.Update;
            
        }

        private void _FillLicenseClassesInComoboBox()
        {
            DataTable dtLicenseClasses = clsLicenseClass.GetLicenseClassesList();

            cbLicenseClass.DataSource = dtLicenseClasses;
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";
        }

        private void _ResetData()
        {
        
            lblLocalDrivingLicebseApplicationID.Text = "[????]";
            cbLicenseClass.SelectedValue = 3;
            lblFees.Text = clsApplicationTypes.Find((int)clsApplicationTypes.enApplicationType.NewDrivingLicense).Fees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedByUser.Text = clsGlobalInfo.CurrentUser.UserName;
        }
        
        private void _LoadData()
        {
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalApplciation.ApplicantPersonID);

            lblLocalDrivingLicebseApplicationID.Text = _LocalApplciation.LocalDrivingLicenseApplicationID.ToString(); ;
            cbLicenseClass.SelectedValue = _LocalApplciation.LicenseClassID;
            lblFees.Text = _LocalApplciation.PaidFees.ToString();
            lblApplicationDate.Text = _LocalApplciation.ApplicationDate.ToShortDateString();
            lblCreatedByUser.Text = clsUsers.Find(_LocalApplciation.CreatedByUserID).UserName.ToString();
        }

        private void _Load()
        {
            _FillLicenseClassesInComoboBox();

            switch(_Mode)
            {
                case enMode.AddNew:
                    {

                        ctrlPersonCardWithFilter1.FilterEnabled = true;
                        btnSave.Enabled = false;
                        btnNext.Enabled = false;
                        this.Text = "New Local Driving License Application";
                        lblAddUpdate.Text = "New Local Driving License Application";
                        tabPage2.Enabled = false;

                        _LocalApplciation = new clsLocalDrivingLicenseApplication();

                        _ResetData();
                        break;
                    }
                case enMode.Update:
                    {

                        this.Text = "Update Local Driving License Application";
                        lblAddUpdate.Text = "Update Local Driving License Application";
                        _LocalApplciation = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

                        if (_LocalApplciation == null)
                        {
                            MessageBox.Show("This Local Application is Not Exist ");
                            this.Close();
                            return;
                        }

                        ctrlPersonCardWithFilter1.FilterEnabled = false;
                        btnSave.Enabled = true;
                        btnNext.Enabled = true;


                        _LoadData();
                        break;
                    }

            }
        }
      
        private void frmAddEditLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _LocalApplciation.LicenseClassID = (int)cbLicenseClass.SelectedValue;

            if (_Mode == enMode.AddNew)
            {
                _LocalApplciation.ApplicantPersonID = _ApplicantPersonID;
                _LocalApplciation.ApplicationTypeID = (int)clsApplicationTypes.enApplicationType.NewDrivingLicense;

                _LocalApplciation.CreatedByUserID = clsGlobalInfo.CurrentUser.UserID;
                _LocalApplciation.PaidFees = Convert.ToDouble(lblFees.Text);
            }

            if (clsLicense.IsLicenseExistByPersonID(_LocalApplciation.ApplicantPersonID, _LocalApplciation.LicenseClassID))
            {
                MessageBox.Show("Person Has Already a license with the same Applied driving class , Choose another License Class ", "Not Allowed ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int ExistApplicationID = clsLocalDrivingLicenseApplication.GetActiveApplicationIDForLicenseClass (_LocalApplciation.ApplicantPersonID, Convert.ToInt32(cbLicenseClass.SelectedValue));

                if (ExistApplicationID != -1 && (_Mode == enMode.AddNew || ExistApplicationID != _LocalApplciation.ApplicationID))
                {
                    MessageBox.Show("choose another License class, the seleced person already have active application for the selected class with ID = " + ExistApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // verification for if the person have active license for the same licenseClass .



            if (_LocalApplciation.Save())
            {
                MessageBox.Show("Data Saved Successfully !","Success", MessageBoxButtons.OK,MessageBoxIcon.Information);
               
                lblLocalDrivingLicebseApplicationID.Text = _LocalApplciation.LocalDrivingLicenseApplicationID.ToString();
                this.Text = "Update Local Driving License Application";
                lblAddUpdate.Text = "Update Local Driving License Application";
                _Mode = enMode.Update;
                ctrlPersonCardWithFilter1.FilterEnabled = false;

            }
            else
            {
                MessageBox.Show("Failed to Save Data ! " ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.SetFilterFocus();
        }
    }
}
