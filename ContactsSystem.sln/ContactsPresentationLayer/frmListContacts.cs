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

        private void _RefreshContactsLust()
        {
            dgvAllContacts.DataSource = clsContact.GetAllContact();
        }

        private void dgvAllContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactsLust();
        }
    }
}
