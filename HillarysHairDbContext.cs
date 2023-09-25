using Microsoft.EntityFrameworkCore;
using HillarysHair.Models;

public class HillarysHairDbContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
    public DbSet<Stylist> Stylists { get; set; }

    public HillarysHairDbContext(DbContextOptions<HillarysHairDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(new Customer[]
        {
            new Customer { Id = 1, FirstName = "John", Lastname = "Doe", Email = "johndoe@example.com", Phone = "555-123-4567" },
            new Customer { Id = 2, FirstName = "Alice", Lastname = "Smith", Email = "alice.smith@example.com", Phone = "555-987-6543" },
            new Customer { Id = 3, FirstName = "Bob", Lastname = "Johnson", Email = "bob.johnson@example.com", Phone = "555-555-5555" },
            new Customer { Id = 4, FirstName = "Eva", Lastname = "Williams", Email = "eva.williams@example.com", Phone = "555-888-9999" },
            new Customer { Id = 5, FirstName = "Michael", Lastname = "Brown", Email = "michael.brown@example.com", Phone = "555-777-3333" },
            new Customer { Id = 6, FirstName = "Sophia", Lastname = "Martinez", Email = "sophia.martinez@example.com", Phone = "555-111-2222" },
            new Customer { Id = 7, FirstName = "William", Lastname = "Davis", Email = "william.davis@example.com", Phone = "555-444-7777" },
            new Customer { Id = 8, FirstName = "Olivia", Lastname = "Garcia", Email = "olivia.garcia@example.com", Phone = "555-666-8888" },
            new Customer { Id = 9, FirstName = "James", Lastname = "Miller", Email = "james.miller@example.com", Phone = "555-222-5555" },
            new Customer { Id = 10, FirstName = "Charlotte", Lastname = "Jones", Email = "charlotte.jones@example.com", Phone = "555-999-1111" }
        });
        
        modelBuilder.Entity<Stylist>().HasData(new Stylist[]
        {
            new Stylist { Id = 1, FirstName = "Emily", Lastname = "Smith", IsActive = true, Email = "emily.smith@example.com", Phone = "555-123-4567" },
            new Stylist { Id = 2, FirstName = "Daniel", Lastname = "Johnson", IsActive = false, Email = "daniel.johnson@example.com", Phone = "555-987-6543" },
            new Stylist { Id = 3, FirstName = "Grace", Lastname = "Williams", IsActive = true, Email = "grace.williams@example.com", Phone = "555-555-5555" },
            new Stylist { Id = 4, FirstName = "Thomas", Lastname = "Brown", IsActive = true, Email = "thomas.brown@example.com", Phone = "555-888-9999" },
            new Stylist { Id = 5, FirstName = "Natalie", Lastname = "Garcia", IsActive = false, Email = "natalie.garcia@example.com", Phone = "555-777-3333" }
        });

        modelBuilder.Entity<Service>().HasData(new Service[]
        {
            new Service { Id = 1, Description = "Haircut", Cost = 30.00M },
            new Service { Id = 2, Description = "Shampoo and Blowout", Cost = 25.00M },
            new Service { Id = 3, Description = "Color Highlights", Cost = 60.00M },
            new Service { Id = 4, Description = "Manicure", Cost = 20.00M },
            new Service { Id = 5, Description = "Pedicure", Cost = 30.00M },
            new Service { Id = 6, Description = "Deep Conditioning Treatment", Cost = 35.00M },
            new Service { Id = 7, Description = "Root Touch-Up", Cost = 40.00M },
            new Service { Id = 8, Description = "Balayage", Cost = 75.00M },
            new Service { Id = 9, Description = "Updo Hairstyle", Cost = 45.00M },
            new Service { Id = 10, Description = "Facial Waxing", Cost = 15.00M },
            new Service { Id = 11, Description = "Hair Extensions", Cost = 100.00M },
            new Service { Id = 12, Description = "Perms and Waves", Cost = 55.00M },
            new Service { Id = 13, Description = "Brazilian Blowout", Cost = 70.00M },
            new Service { Id = 14, Description = "Bridal Hair and Makeup", Cost = 150.00M },
            new Service { Id = 15, Description = "Scalp Massage", Cost = 20.00M }

        });

        modelBuilder.Entity<Appointment>().HasData(new Appointment[]
        {
            // Future Appointments
            new Appointment { Id = 1, StylistId = 1, CustomerId = 1, StartTime = new DateTime(2023, 10, 1, 9, 0, 0), EndTime = new DateTime(2023, 10, 1, 10, 0, 0) },
            new Appointment { Id = 2, StylistId = 2, CustomerId = 2, StartTime = new DateTime(2023, 10, 1, 10, 0, 0), EndTime = new DateTime(2023, 10, 1, 11, 0, 0) },
            new Appointment { Id = 3, StylistId = 3, CustomerId = 3, StartTime = new DateTime(2023, 10, 1, 11, 0, 0), EndTime = new DateTime(2023, 10, 1, 12, 0, 0) },
            new Appointment { Id = 4, StylistId = 4, CustomerId = 4, StartTime = new DateTime(2023, 10, 1, 12, 0, 0), EndTime = new DateTime(2023, 10, 1, 13, 0, 0) },
            new Appointment { Id = 5, StylistId = 5, CustomerId = 5, StartTime = new DateTime(2023, 10, 1, 13, 0, 0), EndTime = new DateTime(2023, 10, 1, 14, 0, 0) },
            new Appointment { Id = 6, StylistId = 1, CustomerId = 2, StartTime = new DateTime(2023, 10, 1, 14, 0, 0), EndTime = new DateTime(2023, 10, 1, 15, 0, 0) },
            new Appointment { Id = 7, StylistId = 2, CustomerId = 3, StartTime = new DateTime(2023, 10, 1, 15, 0, 0), EndTime = new DateTime(2023, 10, 1, 16, 0, 0) },
            // Past Appointments
            new Appointment { Id = 8, StylistId = 3, CustomerId = 4, StartTime = new DateTime(2023, 9, 22, 9, 0, 0), EndTime = new DateTime(2023, 9, 22, 10, 0, 0) },
            new Appointment { Id = 9, StylistId = 4, CustomerId = 5, StartTime = new DateTime(2023, 9, 22, 10, 0, 0), EndTime = new DateTime(2023, 9, 22, 11, 0, 0) },
            new Appointment { Id = 10, StylistId = 5, CustomerId = 1, StartTime = new DateTime(2023, 9, 22, 11, 0, 0), EndTime = new DateTime(2023, 9, 22, 12, 0, 0) }

        });

        modelBuilder.Entity<ServiceAppointment>().HasData(new ServiceAppointment[]
        {
            new ServiceAppointment { Id = 1, ServiceId = 1, AppointmentId = 1 },
            new ServiceAppointment { Id = 2, ServiceId = 2, AppointmentId = 2 },
            new ServiceAppointment { Id = 3, ServiceId = 3, AppointmentId = 2 },
            new ServiceAppointment { Id = 4, ServiceId = 4, AppointmentId = 3 },
            new ServiceAppointment { Id = 5, ServiceId = 5, AppointmentId = 3 },
            new ServiceAppointment { Id = 6, ServiceId = 6, AppointmentId = 3 },
            new ServiceAppointment { Id = 7, ServiceId = 7, AppointmentId = 4 },
            new ServiceAppointment { Id = 8, ServiceId = 8, AppointmentId = 4 },
            new ServiceAppointment { Id = 9, ServiceId = 9, AppointmentId = 4 },
            new ServiceAppointment { Id = 10, ServiceId = 10, AppointmentId = 4 },
            new ServiceAppointment { Id = 11, ServiceId = 11, AppointmentId = 5 },
            new ServiceAppointment { Id = 12, ServiceId = 12, AppointmentId = 6 },
            new ServiceAppointment { Id = 13, ServiceId = 13, AppointmentId = 6 },
            new ServiceAppointment { Id = 14, ServiceId = 14, AppointmentId = 7 },
            new ServiceAppointment { Id = 15, ServiceId = 15, AppointmentId = 7 },
            new ServiceAppointment { Id = 16, ServiceId = 1, AppointmentId = 7 },
            new ServiceAppointment { Id = 17, ServiceId = 2, AppointmentId = 7 },
            new ServiceAppointment { Id = 18, ServiceId = 3, AppointmentId = 7 },
            new ServiceAppointment { Id = 19, ServiceId = 4, AppointmentId = 8 },
            new ServiceAppointment { Id = 20, ServiceId = 5, AppointmentId = 8 },
            new ServiceAppointment { Id = 21, ServiceId = 15, AppointmentId = 9 },
            new ServiceAppointment { Id = 22, ServiceId = 14, AppointmentId = 10 }

        });
    }
}