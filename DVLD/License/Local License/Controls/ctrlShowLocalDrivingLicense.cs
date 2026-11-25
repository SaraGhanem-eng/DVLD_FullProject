using DVLD.Classes;
using DVLD.Properties;
using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD.Controls;
using DVLD.GLobal_Classes;

namespace DVLD.License.Local_License.Controls
{
    public partial class ctrlShowLocalDrivingLicense : UserControl
    {
        public ctrlShowLocalDrivingLicense()
        {
            InitializeComponent();
        }
        private clsLicense _License;
        private int _LicenseID;

        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        { get { return _License; } }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersonInfo._Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string RelativePath = _License.DriverInfo.PersonInfo._ImagePath;

            if (!string.IsNullOrEmpty(RelativePath))
            {
                string ImagePath = Path.Combine(clsUtil.ImagesFolder, RelativePath);

                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: " + ImagePath,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            // lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassIfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersonInfo._NationalNo;
            lblGendor.Text = _License.DriverInfo.PersonInfo._Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.GetDateString(_License.DriverInfo.PersonInfo._DateOfBirth);

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text = clsFormat.GetDateString(_License.IssueDate);
            lblExpirationDate.Text = clsFormat.GetDateString(_License.ExpirationDate);
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();



        }
    }
}
