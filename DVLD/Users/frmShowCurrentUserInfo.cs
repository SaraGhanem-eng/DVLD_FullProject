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

namespace DVLD.Users
{
    public partial class frmShowCurrentUserInfo : Form
    {
        public frmShowCurrentUserInfo()
        {
            InitializeComponent();
        }

        private void frmShowCurrentUserInfo_Load(object sender, EventArgs e)
        {
            int CurrentUserID = clsGlobalInfo.CurrentUser.UserID;

            if (CurrentUserID != -1)
                ctrlUserInfo1.LoadUserInfo(CurrentUserID);
            else
                return;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
