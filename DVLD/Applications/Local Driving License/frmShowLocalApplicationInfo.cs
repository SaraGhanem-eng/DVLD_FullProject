using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmShowLocalApplicationInfo : Form
    {
        private int _LocalDrivingLicenseID = -1;
        public frmShowLocalApplicationInfo(int LocalDrivingLicenseApplicationID )
        {
            InitializeComponent();
            if (LocalDrivingLicenseApplicationID != -1)
                _LocalDrivingLicenseID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLocalApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlLocalApplicationInfo1.LoadLocalDrivingApplicationInfo(_LocalDrivingLicenseID);
        }
    }
}
