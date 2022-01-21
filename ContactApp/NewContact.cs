using ContactApp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationContext = ContactApp.Data.ApplicationContext;

namespace ContactApp;

public partial class NewContact : Form
{
    public NewContact()
    {
        InitializeComponent();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        var contact = new ContactData
        {
            Name = tbName.Text,
            Family = tbFamily.Text,
            Address = tbAddress.Text,
            Description = tbDescription.Text,
            Email = tbEmail.Text,
            Phone = tbPhone.Text
        };
        try
        {
            using (var context = new ApplicationContext())
            {
                context.Add(contact);
                context.SaveChanges();
            }
            MessageBox.Show($"Contacts : {contact.Name} is save.");
            this.Close();
        }
        catch (Exception)
        {
            MessageBox.Show("Error!");
        }
    }
}

