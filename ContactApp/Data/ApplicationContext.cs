using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ContactApp.Data;

public class ApplicationContext : DbContext
{
    public DbSet<ContactData> Contacts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=C:\Users\behna\Desktop\ContactDB.db;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactData>().ToTable("Contacts");
    }
}

public class ContactData
{
    [Key, Required]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Family { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
}