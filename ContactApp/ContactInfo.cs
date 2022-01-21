using ContactApp.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationContext = ContactApp.Data.ApplicationContext;

namespace ContactApp;

public partial class ContactInfo : Form
{
    private int _tag { get; set; }
    public ContactInfo(int tag)
    {
        InitializeComponent();

        _tag = tag;
        ReadOnlyControllers(true);
        LoadData();
    }

    public void ReadOnlyControllers(bool visible)
    {
        if (visible)
        {
            this.tbName.ReadOnly = true;
            this.tbFamily.ReadOnly = true;
            this.tbPhone.ReadOnly = true;
            this.tbEmail.ReadOnly = true;
            this.tbAddress.ReadOnly = true;
            this.tbDescription.ReadOnly = true;
            this.btnCancel.Visible = false;
            this.btnSave.Visible = false;
            this.btnEdit.Visible = true;
        }
        if (!visible)
        {
            this.tbName.ReadOnly = false;
            this.tbFamily.ReadOnly = false;
            this.tbPhone.ReadOnly = false;
            this.tbEmail.ReadOnly = false;
            this.tbAddress.ReadOnly = false;
            this.tbDescription.ReadOnly = false;
            this.btnCancel.Visible = true;
            this.btnSave.Visible = true;
            this.btnEdit.Visible = false;
        }
    }

    public void LoadData()
    {
        using (var context = new ApplicationContext())
        {
            var dataContact = context.Contacts.FirstOrDefault(c=>c.Id == _tag);
            
            tbName.Text = dataContact.Name;
            tbFamily.Text = dataContact.Family;
            tbPhone.Text = dataContact.Phone;
            tbEmail.Text = dataContact.Email;
            tbAddress.Text = dataContact.Address;
            tbDescription.Text = dataContact.Description;
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        ReadOnlyControllers(false);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        ReadOnlyControllers(true);
        using (var context = new ApplicationContext())
        {
            var dataContact = context.Contacts.FirstOrDefault(c => c.Id == _tag);

            tbName.Text = dataContact.Name;
            tbFamily.Text = dataContact.Family;
            tbPhone.Text = dataContact.Phone;
            tbEmail.Text = dataContact.Email;
            tbAddress.Text = dataContact.Address;
            tbDescription.Text = dataContact.Description;
        }
    }


    private void btnSave_Click(object sender, EventArgs e)
    {
        ReadOnlyControllers(true);

        try
        {
            using (var context = new ApplicationContext())
            {
                var dataContact = new ContactData
                {
                    Id = _tag,
                    Name = tbName.Text,
                    Family = tbFamily.Text,
                    Phone = tbPhone.Text,
                    Email = tbEmail.Text,
                    Address = tbAddress.Text,
                    Description = tbDescription.Text
                };

                context.Contacts.Update(dataContact);
                context.SaveChanges();

                MessageBox.Show("Saved !");
            }
        }
        catch (Exception)
        {
            MessageBox.Show("Error");
        }

       
    }
}

