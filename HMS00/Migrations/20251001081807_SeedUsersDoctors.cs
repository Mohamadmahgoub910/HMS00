using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HMS00.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersDoctors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "ImageUrl", "Name", "Specialization" },
                values: new object[,]
                {
                    { 1, "/images/doctors/doc1.jpg", "Dr. Ahmed Ali", "Cardiology" },
                    { 2, "/images/doctors/doc2.jpg", "Dr. Sara Mahmoud", "Dermatology" },
                    { 3, "/images/doctors/doc3.jpg", "Dr. Omar Hossam", "Radiology" },
                    { 4, "/images/doctors/doc4.jpg", "Dr. Mona Adel", "Pediatrics" },
                    { 5, "/images/doctors/doc5.jpg", "Dr. Hany Nabil", "Orthopedics" },
                    { 6, "/images/doctors/doc6.jpg", "Dr. Salma Reda", "Neurology" },
                    { 7, "/images/doctors/doc1.jpg", "Dr. Yasser Tarek", "ENT" },
                    { 8, "/images/doctors/doc2.jpg", "Dr. Laila Mostafa", "Gynecology" },
                    { 9, "/images/doctors/doc3.jpg", "Dr. Mohamed Salah", "General Surgery" },
                    { 10, "/images/doctors/doc4.jpg", "Dr. Eman Hossam", "Ophthalmology" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "ahmed.hassan@example.com", "Ahmed Hassan", "0101111111" },
                    { 2, "sara.mohamed@example.com", "Sara Mohamed", "0102222222" },
                    { 3, "omar.fathy@example.com", "Omar Fathy", "0103333333" },
                    { 4, "nour.samir@example.com", "Nour Samir", "0104444444" },
                    { 5, "youssef.adel@example.com", "Youssef Adel", "0105555555" },
                    { 6, "mona.ali@example.com", "Mona Ali", "0106666666" },
                    { 7, "karim.tarek@example.com", "Karim Tarek", "0107777777" },
                    { 8, "heba.salah@example.com", "Heba Salah", "0108888888" },
                    { 9, "mahmoud.reda@example.com", "Mahmoud Reda", "0109999999" },
                    { 10, "rana.mostafa@example.com", "Rana Mostafa", "0110000000" },
                    { 11, "tamer.hany@example.com", "Tamer Hany", "0111111111" },
                    { 12, "laila.nabil@example.com", "Laila Nabil", "0112222222" },
                    { 13, "fady.magdy@example.com", "Fady Magdy", "0113333333" },
                    { 14, "nesma.adel@example.com", "Nesma Adel", "0114444444" },
                    { 15, "hossam.ezz@example.com", "Hossam Ezz", "0115555555" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate_AppointmentTime",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDate", "AppointmentTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
