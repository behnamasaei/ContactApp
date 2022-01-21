using ContactApp.Data;
using ApplicationContext = ContactApp.Data.ApplicationContext;

namespace ContactApp;
public partial class MainForm : Form
{
    //private List<CheckBox>? CheckBoxList { get; set; }

    public MainForm()
    {
        InitializeComponent();

    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        int tag = (int)(sender as LinkLabel).Tag;
        using (var context = new ApplicationContext())
        {
            ContactInfo contactInfo = new ContactInfo(tag);
            contactInfo.ShowDialog();
        }
    }

    public void LoadData()
    {
        flpMainContact.Controls.Clear();
        using (var context = new ApplicationContext())
        {
            var contacts = context.Contacts.ToList();
            foreach (var contact in contacts)
            {
                LinkLabel label = new LinkLabel();
                label.Text = $"{contact.Name} {contact.Family}  -  {contact.Phone}";
                label.Tag = contact.Id;

                // Label attribiut
                label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
                label.Margin = new Padding(5, 5, 5, 5);
                label.Padding = new Padding(5, 5, 5, 5);
                label.AutoSize = true;
                label.LinkColor = Color.Black;
                label.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
                label.Font = new Font("Arial", 10);

                // Check box
                CheckBox checkBox = new CheckBox();
                checkBox.Name = $"chb{contact.Id}";
                checkBox.Tag = contact.Id;
                checkBox.Margin = new Padding(0, 10, 0, 0);



                flpMainContact.Controls.Add(label);
                flpMainContact.Controls.Add(checkBox);

                flpMainContact.SetFlowBreak(checkBox, true);
            }

        }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        string search = tbSearch.Text;

        if (!string.IsNullOrWhiteSpace(search))
        {
            flpMainContact.Controls.Clear();
            using (var context = new ApplicationContext())
            {

                var dataContacts = context.Contacts.Where(
                    c => c.Name.Contains(search) ||
                    c.Family.Contains(search) ||
                    c.Phone.Contains(search) ||
                    c.Address.Contains(search) ||
                    c.Email.Contains(search) ||
                    c.Address.Contains(search)).ToList();


                foreach (var contact in dataContacts)
                {
                    LinkLabel label = new LinkLabel();
                    label.Text = $"{contact.Name} {contact.Family}  -  {contact.Phone}";
                    label.Tag = contact.Id;

                    // Label attribiut
                    label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
                    label.Margin = new Padding(5, 5, 5, 5);
                    label.Padding = new Padding(5, 5, 5, 5);
                    label.AutoSize = true;
                    label.LinkColor = Color.Black;
                    label.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
                    label.Font = new Font("Arial", 10);

                    // Check box
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = $"chb{contact.Id}";
                    checkBox.Tag = contact.Id;
                    checkBox.Margin = new Padding(0, 10, 0, 0);



                    flpMainContact.Controls.Add(label);
                    flpMainContact.Controls.Add(checkBox);

                    flpMainContact.SetFlowBreak(checkBox, true);
                }

            }
        }
        else
        {
            flpMainContact.Controls.Clear();
            LoadData();
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {

        DialogResult dialogResult = MessageBox.Show("Are you shure delete your contacts?", "Delete contacts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (dialogResult == DialogResult.Yes)
        {
            List<CheckBox> checksTrue = new List<CheckBox>();
            var checkboxes = GetAll(this, typeof(CheckBox)).ToList();

            foreach (var checkBox in checkboxes)
            {
                var ch = checkBox as CheckBox;
                if (ch.CheckState == CheckState.Checked)
                {
                    using (var context = new ApplicationContext())
                    {
                        var contact = context.Contacts.Single(c => c.Id == (int)ch.Tag);
                        context.Contacts.Remove((Data.ContactData)contact);
                        context.SaveChanges();
                    }
                }
            }
            MessageBox.Show("Contacts selected are deleted.");
            LoadData();

        }
        else if (dialogResult == DialogResult.No)
        {
            //do something else
        }



    }

    public IEnumerable<Control> GetAll(Control control, Type type)
    {
        var controls = control.Controls.Cast<Control>();

        return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                  .Concat(controls)
                                  .Where(c => c.GetType() == type);
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        NewContact newContact = new NewContact();
        newContact.ShowDialog();
    }

    private void btnAddFake_Click(object sender, EventArgs e)
    {
        //// add user
        using (var context = new ApplicationContext())
        {
            var contact = new ContactData
            {
                Name = Faker.Name.First(),
                Family = Faker.Name.Last(),
                Phone = Faker.Phone.Number(),
                Email = Faker.Internet.Email(),
                Address = $"{Faker.Address.Country()}, {Faker.Address.City()}",
                Description = $"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            };

            context.Contacts.Add(contact);
            context.SaveChanges();

            MessageBox.Show($"User {contact.Name} added.");
            LoadData();
        }
        
    }
}
