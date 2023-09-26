using Microsoft.EntityFrameworkCore;
using HillarysHair.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HillarysHairDbContext>(builder.Configuration["HillarysHairDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// endpoints here

// get all appointments
// make filterable by stylist ID and/or customer ID

app.MapGet("/api/appointments", (HillarysHairDbContext db, int? stylistId, int? customerId) => 
{

    var query = db.Appointments
        .Include(a => a.Stylist)
        .Include(a => a.Customer)
        .Include(a => a.ServiceAppointments)
        .ThenInclude(sa => sa.Service).ToList();

    if (stylistId.HasValue)
    {
        query = query.Where(a => a.StylistId == stylistId).ToList();
    }

    if (customerId.HasValue)
    {
        query = query.Where(a => a.CustomerId == customerId).ToList();
    }

    return query;

});

// ? get all completed appointments

app.MapGet("/api/appointments/completed", (HillarysHairDbContext db) =>
{
    return db.Appointments
        .Include(a => a.Stylist)
        .Include(a => a.Customer)
        .Include(a => a.ServiceAppointments)
        .ThenInclude(sa => sa.Service)
        .Where(a => DateTime.Today > a.EndTime).ToList();
});

// ? get all future appointments

app.MapGet("/api/appointments/future", (HillarysHairDbContext db) =>
{
    return db.Appointments
        .Include(a => a.Stylist)
        .Include(a => a.Customer)
        .Include(a => a.ServiceAppointments)
        .ThenInclude(sa => sa.Service)
        .Where(a => DateTime.Today < a.EndTime).ToList();
});

// ? access appointment by ID

app.MapGet("/api/appointments/{id}", (HillarysHairDbContext db, int id) =>
{
    try
    {
        Appointment foundAppointment = db.Appointments
            .Include(a => a.Stylist)
            .Include(a => a.Customer)
            .Include(a => a.ServiceAppointments)
            .ThenInclude(sa => sa.Service)
            .Single(a => a.Id == id);
        return Results.Ok(foundAppointment);
    }
    catch (InvalidOperationException)
    {
        return Results.NotFound();
    }
    catch (BadHttpRequestException)
    {
        return Results.BadRequest("please provide integers only");
        // how do I provide a response body? The above is not working
    }
});

// post new appointment

app.MapPost("/api/appointments", (HillarysHairDbContext db, Appointment newAppointment) =>
{
    db.Appointments.Add(newAppointment);
    db.SaveChanges();
    return Results.Created($"/api/appointments/{newAppointment.Id}", newAppointment);
});

// put - edit existing appointment

app.MapPut("/api/appointments/{id}", (HillarysHairDbContext db, Appointment editedAppointment, int id) =>
{
    Appointment foundAppointment = db.Appointments.SingleOrDefault(a => a.Id == id);
    if (foundAppointment == null)
    {
        return Results.NotFound();
    }
    else if (editedAppointment.Id != id)
    {
        return Results.BadRequest();
    }
    
    if (editedAppointment.StylistId == null)
    {
        foundAppointment.StylistId = foundAppointment.StylistId;
    }
    foundAppointment.StylistId = editedAppointment.StylistId;

    if (editedAppointment.CustomerId == null)
    {
        foundAppointment.CustomerId = foundAppointment.CustomerId;
    }
    foundAppointment.CustomerId = editedAppointment.CustomerId;

    if (editedAppointment.StartTime == null)
    {
        foundAppointment.StartTime = foundAppointment.StartTime;
    }
    foundAppointment.StartTime = editedAppointment.StartTime;

    if (editedAppointment.EndTime == null)
    {
        foundAppointment.EndTime = foundAppointment.EndTime;    
    }
    foundAppointment.EndTime = editedAppointment.EndTime;

    db.SaveChanges();
    return Results.NoContent();

});

// POST - ADD service associated with an appointment
// backend will receive a new ServiceAppointment object

app.MapPost("/api/serviceappointments", (HillarysHairDbContext db, ServiceAppointment newServiceAppointment) =>
{
    db.ServiceAppointments.Add(newServiceAppointment);
    db.SaveChanges();
    return Results.Created($"/api/serviceappointments/{newServiceAppointment.Id}", newServiceAppointment);
});

// DELETE - REMOVE service associated with an appointment
// backend will receive a ServiceAppointment object; this will be matched to a specific member of the collection and subsequently removed

app.MapDelete("/api/serviceappointments", (HillarysHairDbContext db, ServiceAppointment deletedServiceAppointment) =>
{
    ServiceAppointment foundSA = db.ServiceAppointments.SingleOrDefault(sa => sa.ServiceId == deletedServiceAppointment.ServiceId && sa.AppointmentId == deletedServiceAppointment.AppointmentId);
    if (foundSA == null)
    {
        return Results.NotFound();
    }
    db.ServiceAppointments.Remove(foundSA);
    db.SaveChanges();
    return Results.NoContent();
});

// delete (hard) future appointment
// if current or past: Results = Forbidden;

app.MapDelete("/api/appointments/{id}", (HillarysHairDbContext db, int id) =>
{
    Appointment foundAppointment = db.Appointments.SingleOrDefault(a => a.Id == id);

    if (foundAppointment == null)
    {
        return Results.NotFound();
    }
    else if (foundAppointment.StartTime <= DateTime.Today)
    {
        // if appointment's start time is present or in the past, prevent its deletion
        return Results.Forbid();
    }
    db.Appointments.Remove(foundAppointment);
    db.SaveChanges();
    return Results.NoContent();
});

// get all customers

app.MapGet("/api/customers", (HillarysHairDbContext db) =>
{
    return db.Customers.ToList();
});

// post new customer

app.MapPost("/api/customers", (HillarysHairDbContext db, Customer newCustomer) =>
{
    db.Customers.Add(newCustomer);
    db.SaveChanges();
    return Results.Created($"/api/customers/{newCustomer.Id}", newCustomer);
});

// get all employees

app.MapGet("/api/stylists", (HillarysHairDbContext db) =>
{
    return db.Stylists.ToList();
});

// post new employee

app.MapPost("/api/stylists", (HillarysHairDbContext db, Stylist newStylist) =>
{
    newStylist.IsActive = true;
    db.Stylists.Add(newStylist);
    db.SaveChanges();
    return Results.Created($"/api/customers/{newStylist.Id}", newStylist);
});

// delete (soft) employee

app.MapDelete("/api/stylists/{id}", (HillarysHairDbContext db, int id) =>
{
    Stylist foundStylist = db.Stylists.SingleOrDefault(s => s.Id == id);
    if (foundStylist == null)
    {
        return Results.NotFound();   
    }
    foundStylist.IsActive = false;
    db.SaveChanges();
    return Results.NoContent();
});


app.Run();
