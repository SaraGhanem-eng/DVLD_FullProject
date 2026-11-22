using DVLD.Business;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Application_Types
{
    public partial class frmManageApplicationTypes : Form
    {
        private DataTable _dtListTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void  _LoadApplicationTypes()
        {
            _dtListTypes = clsApplicationTypes.GetApplicationTypes();
            dgvApplicationTypes.DataSource = _dtListTypes;
            lblNumOfRecords.Text = dgvApplicationTypes.RowCount.ToString();


            dgvApplicationTypes.ReadOnly = true;

            dgvApplicationTypes.AllowUserToAddRows = false;
            dgvApplicationTypes.AllowUserToDeleteRows = false;

            if (dgvApplicationTypes.Rows.Count > 0 )
            {
                dgvApplicationTypes.Columns[0].Width = 110;
                dgvApplicationTypes.Columns[1].Width = 300;
                dgvApplicationTypes.Columns[2].Width = 100;
            }
        }
       
        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadApplicationTypes();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.CurrentRow != null)
            {
                int TypeID = Convert.ToInt32(dgvApplicationTypes.CurrentRow.Cells[0].Value);
                clsApplicationTypes Type = clsApplicationTypes.Find(TypeID);
                if (Type != null)
                {
                    frmEditApplicationType frm = new frmEditApplicationType(TypeID);
                    frm.ShowDialog();
                    _LoadApplicationTypes();
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
