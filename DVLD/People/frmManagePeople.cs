using DVLD.Business;
using DVLD.Classes;
using System;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;
namespace DVLD
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAdd_EditPersonInfo frm = new frmAdd_EditPersonInfo(-1);
            frm.ShowDialog();
            LoadPeople();
        }

        private void LoadPeople()
        {
            dgvPeople.DataSource = clsPerson.GetAllPeople();
            lblNumOfRecords.Text = dgvPeople.RowCount.ToString();
        }
       
        private string GetSelectedColumn(int SelectedIndex)
        {
            switch (SelectedIndex)
            {
                case 1:
                    return "PersonID";
                case 2:
                    return "NationalNo";
                case 3:
                    return "FirstName";
                case 4:
                    return "SecondName";
                case 5:
                    return "ThirdName";
                case 6:
                    return "LastName";
                case 7:
                    return "Nationality";
                case 8:
                    return "Gender";
                case 9:
                    return "Phone";
                case 10:
                    return "Email";
                default:
                    return "None";
            }
        }
       
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            LoadPeople();
            cbFilterBy.SelectedIndex = 0;
            
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Clear();

            if (cbFilterBy.SelectedIndex == 0)
                txtFilterBy.Enabled = false;
            else
                txtFilterBy.Enabled = true;
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0 || txtFilterBy.Text == "")
            {
                LoadPeople();
            
            }
            else 
            {
                dgvPeople.DataSource = clsPerson.FilterBy(GetSelectedColumn(cbFilterBy.SelectedIndex), txtFilterBy.Text);
            }
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            switch (cbFilterBy.SelectedIndex)
            {
                case 1:
                    e.Handled = !clsValidation.IsNumber(e.KeyChar);
                    break;
                case 2:
                    e.Handled = !clsValidation.IsNumberOrLetter(e.KeyChar);
                    break;
                case 3:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 4:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 5:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 6:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 7:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 8:
                    e.Handled = !clsValidation.IsLetter(e.KeyChar);
                    break;
                case 9:
                    e.Handled = !clsValidation.IsNumber(e.KeyChar);
                    break;
                case 10:
                    e.Handled = !clsValidation.IsNumberOrLetterOrSymbol(e.KeyChar);
                    break;
                default:
                    e.Handled = !clsValidation.IsNumberOrLetter(e.KeyChar);
                    break;

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdd_EditPersonInfo frm = new frmAdd_EditPersonInfo(-1);
            frm.ShowDialog();
            LoadPeople();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvPeople.CurrentRow != null)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);
                frmAdd_EditPersonInfo frm = new frmAdd_EditPersonInfo(PersonID);
                frm.ShowDialog();
                LoadPeople();
            }

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow != null )
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);
                clsPerson _Person = clsPerson.Find(PersonID);
                if (_Person != null)
                {
                    frmPresentPersonInfo frm = new frmPresentPersonInfo(_Person);
                    frm.ShowDialog();
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow != null)
            {
                int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);
                if (MessageBox.Show("Are you sure that you want to delete Person With ID [ " + dgvPeople.CurrentRow.Cells[0].Value
                 + " ]", "Confirm Delete Message ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (clsPerson.DeletePerson(PersonID))
                    {
                        MessageBox.Show("Person Deleted Successfully ! " , "Deleted Successfuly ", MessageBoxButtons.OK , MessageBoxIcon.Information) ;
                        LoadPeople();
                    }
                    else
                    {
                        MessageBox.Show("Person Was Not Deleted Because it has data linked to it ! ", "Failed ! " , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
