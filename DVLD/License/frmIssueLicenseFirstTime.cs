using DVLD.Business;
using DVLD.GLobal_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.License
{
    public partial class frmIssueLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplicationInfo;
        

        public frmIssueLicenseFirstTime(int LocalDrivingLicenseApplicationID )
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;    
        }

        private void frmIssueLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplicationInfo == null )
            {
                MessageBox.Show("Application Is Not Exist ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            ctrlLocalApplicationInfo1.LoadLocalDrivingApplicationInfo(_LocalDrivingLicenseApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplicationInfo.IssueLicenseForFirstTime(clsGlobalInfo.CurrentUser.UserID, txtNotes.Text);
           if (LicenseID != -1 )
            { 
                MessageBox.Show("License Issued Successfully With ID : " + LicenseID , "Saved Successfully ! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
           else
            {
                MessageBox.Show("Couldn't Issue License Successfully ! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
