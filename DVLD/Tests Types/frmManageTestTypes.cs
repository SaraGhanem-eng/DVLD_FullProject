using DVLD.Application_Types;
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

namespace DVLD.Tests_Types
{
    public partial class frmManageTestTypes : Form
    {
        private DataTable _dtListTypes;
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _LoadTestTypes()
        {
            _dtListTypes = clsTestsTypes.GetTestTypes();
            dgvTestTypes.DataSource = _dtListTypes;
            lblNumOfRecords.Text = dgvTestTypes.RowCount.ToString();


            dgvTestTypes.ReadOnly = true;

            dgvTestTypes.AllowUserToAddRows = false;
            dgvTestTypes.AllowUserToDeleteRows = false;

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].Width = 90;
                dgvTestTypes.Columns[1].Width = 120;
                dgvTestTypes.Columns[2].Width = 300;
                dgvTestTypes.Columns[3].Width = 90;
            }
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _LoadTestTypes();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.CurrentRow != null)
            {
                int TypeID = Convert.ToInt32(dgvTestTypes.CurrentRow.Cells[0].Value);
                clsTestsTypes Type = clsTestsTypes.Find(TypeID);
                if (Type != null)
                {
                    frmEditTestType frm = new frmEditTestType(TypeID);
                    frm.ShowDialog();
                    _LoadTestTypes();
                }
            }
        }
    }
}
