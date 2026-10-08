using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Business;
namespace DVLD
{
    public partial class frmPresentPersonInfo : Form
    {
        clsPerson _Person;
        public frmPresentPersonInfo(clsPerson Person)
        {
            InitializeComponent();
            _Person = Person;

            if (_Person != null )
            {
                ctrlPersonCard1.LoadPersonInfo(_Person);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
