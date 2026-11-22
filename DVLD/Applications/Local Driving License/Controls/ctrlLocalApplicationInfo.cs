using DVLD.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.Local_Driving_License.Controls
{
    public partial class ctrlLocalApplicationInfo : UserControl
    {
        private int _LocalDrivingLicenseApplicationID = -1 ;
        public int LocalDrivingLicenseApplicationID
        {
            get { return _LocalDrivingLicenseApplicationID; }
        }
        
        private clsLocalDrivingLicenseApplication _LocalApplication; 
     
        public clsLocalDrivingLicenseApplication LocalApplciation
        {
            get { return _LocalApplication; }
        }
        
        public ctrlLocalApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadLocalDrivingApplicationInfo (int LocalDrivingLicenseApplicationID)
        {
            _LocalApplication = clsLocalDrivingLicenseApplication.
                   FindByLocalDrivingLicenseApplicationID(LocalDrivingLicenseApplicationID);
            if(_LocalApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Local Application with L.D.LApplicationID = " + LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LocalDrivingLicenseApplicationID = _LocalApplication.LocalDrivingLicenseApplicationID;
            _LoadInfo();
        }

        public void LoadLocalDrivingApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalApplication = clsLocalDrivingLicenseApplication.
                   FindByApplicationID(ApplicationID);
            if (_LocalApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Local Application with L.D.LApplicationID = " + ApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LocalDrivingLicenseApplicationID = _LocalApplication.LocalDrivingLicenseApplicationID;
            _LoadInfo();
        }
      
        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            ctrlApplicationInfo1.ResetApplicationInfo();
            lblLocalDrivingLicenseApplicationID.Text = "[????]";
            lblAppliedFor.Text = "[????]";


        }
      
        private void _LoadInfo()
        {
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = clsLicenseClass.Find(_LocalApplication.LicenseClassID).ClassName;
            // load Passed tests after implementing it !!! 
            ctrlApplicationInfo1.LoadApplicationInfo(_LocalApplication.ApplicationID);
        }
    }
}
