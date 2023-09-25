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

    // var results = query.ToList();


    return query;

});

// ? get all completed appointments

// ? get all future appointments

// ? access appointment by ID

// post new appointment

// put existing appointment

// delete (hard) future appointment

// get all customers

// post new customer

// get all employees

// post new employee

// delete (soft) employee


app.Run();
