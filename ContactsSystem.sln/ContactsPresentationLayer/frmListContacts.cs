using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ContactsBusinessLayer;

namespace ContactsPresentationLayer
{
    public partial class frmListContacts : Form
    {
        public frmListContacts()
        {
            InitializeComponent();
        }

        void _RefreshContactsList()
        {
            dgvGetAllContacts.DataSource = clsContact.GetAllContact();
        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvGetAllContacts.CurrentRow.Cells[0].Value;
            frmAddEditContact frm = new frmAddEditContact(ID);
            frm.ShowDialog();

            _RefreshContactsList();

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddEditContact frm = new frmAddEditContact(-1);
            frm.ShowDialog();

            _RefreshContactsList();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ContactID = (int)dgvGetAllContacts.CurrentRow.Cells[0].Value;

            if(MessageBox.Show("Are you sure you want to delete Contact [" + ContactID + "]", "Confirm Delete", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if(clsContact.DeleteContact(ContactID))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete Contact");
                }

                _RefreshContactsList();

            }
        }
    }
}
