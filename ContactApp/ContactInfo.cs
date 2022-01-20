using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactApp
{
    public partial class ContactInfo : Form
    {
        public ContactInfo()
        {
            InitializeComponent();
            this.tbName.ReadOnly = true;
            this.tbFamily.ReadOnly = true;
            this.tbPhone.ReadOnly = true;
            this.tbEmail.ReadOnly = true;
            this.tbAddress.ReadOnly = true;
            this.tbDescription.ReadOnly = true;
            this.btnCancel.Visible = false;
            this.btnSave.Visible = false;
        }


    }
}
