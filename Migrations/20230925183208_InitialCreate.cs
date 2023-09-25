using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HillarysHair.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stylists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stylists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StylistId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Stylists_StylistId",
                        column: x => x.StylistId,
                        principalTable: "Stylists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAppointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAppointments_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAppointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Email", "FirstName", "Lastname", "Phone" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John", "Doe", "555-123-4567" },
                    { 2, "alice.smith@example.com", "Alice", "Smith", "555-987-6543" },
                    { 3, "bob.johnson@example.com", "Bob", "Johnson", "555-555-5555" },
                    { 4, "eva.williams@example.com", "Eva", "Williams", "555-888-9999" },
                    { 5, "michael.brown@example.com", "Michael", "Brown", "555-777-3333" },
                    { 6, "sophia.martinez@example.com", "Sophia", "Martinez", "555-111-2222" },
                    { 7, "william.davis@example.com", "William", "Davis", "555-444-7777" },
                    { 8, "olivia.garcia@example.com", "Olivia", "Garcia", "555-666-8888" },
                    { 9, "james.miller@example.com", "James", "Miller", "555-222-5555" },
                    { 10, "charlotte.jones@example.com", "Charlotte", "Jones", "555-999-1111" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Cost", "Description" },
                values: new object[,]
                {
                    { 1, 30.00m, "Haircut" },
                    { 2, 25.00m, "Shampoo and Blowout" },
                    { 3, 60.00m, "Color Highlights" },
                    { 4, 20.00m, "Manicure" },
                    { 5, 30.00m, "Pedicure" },
                    { 6, 35.00m, "Deep Conditioning Treatment" },
                    { 7, 40.00m, "Root Touch-Up" },
                    { 8, 75.00m, "Balayage" },
                    { 9, 45.00m, "Updo Hairstyle" },
                    { 10, 15.00m, "Facial Waxing" },
                    { 11, 100.00m, "Hair Extensions" },
                    { 12, 55.00m, "Perms and Waves" },
                    { 13, 70.00m, "Brazilian Blowout" },
                    { 14, 150.00m, "Bridal Hair and Makeup" },
                    { 15, 20.00m, "Scalp Massage" }
                });

            migrationBuilder.InsertData(
                table: "Stylists",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "Lastname", "Phone" },
                values: new object[,]
                {
                    { 1, "emily.smith@example.com", "Emily", true, "Smith", "555-123-4567" },
                    { 2, "daniel.johnson@example.com", "Daniel", false, "Johnson", "555-987-6543" },
                    { 3, "grace.williams@example.com", "Grace", true, "Williams", "555-555-5555" },
                    { 4, "thomas.brown@example.com", "Thomas", true, "Brown", "555-888-9999" },
                    { 5, "natalie.garcia@example.com", "Natalie", false, "Garcia", "555-777-3333" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "CustomerId", "EndTime", "StartTime", "StylistId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, new DateTime(2023, 10, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 3, new DateTime(2023, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 4, 4, new DateTime(2023, 10, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 5, 5, new DateTime(2023, 10, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, 2, new DateTime(2023, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, 3, new DateTime(2023, 10, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, 4, new DateTime(2023, 9, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, 5, new DateTime(2023, 9, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 22, 10, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 10, 1, new DateTime(2023, 9, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "ServiceAppointments",
                columns: new[] { "Id", "AppointmentId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 2, 3 },
                    { 4, 3, 4 },
                    { 5, 3, 5 },
                    { 6, 3, 6 },
                    { 7, 4, 7 },
                    { 8, 4, 8 },
                    { 9, 4, 9 },
                    { 10, 4, 10 },
                    { 11, 5, 11 },
                    { 12, 6, 12 },
                    { 13, 6, 13 },
                    { 14, 7, 14 },
                    { 15, 7, 15 },
                    { 16, 7, 1 },
                    { 17, 7, 2 },
                    { 18, 7, 3 },
                    { 19, 8, 4 },
                    { 20, 8, 5 },
                    { 21, 9, 15 },
                    { 22, 10, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId",
                table: "Appointments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_StylistId",
                table: "Appointments",
                column: "StylistId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_AppointmentId",
                table: "ServiceAppointments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAppointments_ServiceId",
                table: "ServiceAppointments",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAppointments");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Stylists");
        }
    }
}
