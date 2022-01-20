using ApplicationContext = ContactApp.Data.ApplicationContext;

namespace ContactApp;
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        using (var context = new ApplicationContext())
        {
            var contacts = context.Contacts.ToList();
            foreach (var contact in contacts)
            {
                LinkLabel label = new LinkLabel();
                label.Text = contact.Name;
                label.Tag = contact.Id;

                // Label attribiut
                label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkClicked);
                label.Margin = new Padding(5, 5, 5, 5);
                label.Padding = new Padding(5, 5, 5, 5);
                label.AutoSize = true;
                label.BorderStyle = BorderStyle.FixedSingle;
                label.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
                label.Font = new Font("Arial", 13);

                flpMainContact.Controls.Add(label);
                flpMainContact.SetFlowBreak(label, true);
            }
            
        }
    }

    private void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        
    }

}
