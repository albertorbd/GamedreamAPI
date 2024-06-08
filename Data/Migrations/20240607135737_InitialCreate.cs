using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamedreamAPI.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Money = table.Column<double>(type: "float", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videogames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valoration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VideogameId = table.Column<int>(type: "int", nullable: true),
                    Concept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Operations_Videogames_VideogameId",
                        column: x => x.VideogameId,
                        principalTable: "Videogames",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "DNI", "Email", "Lastname", "Money", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "345678", "jesusito@hotmail.es", "Lopez", 60.0, "Jesus", "12345", "user" },
                    { 2, new DateTime(1996, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "2532135", "albertoriveiro@hotmail.es", "Riveiro del Bano", 50.0, "Alberto", "1234567", "admin" },
                    { 3, new DateTime(1980, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "234567", "carlitos@hotmail.es", "diaz", 0.0, "carlos", "carlitos12", "user" },
                    { 4, new DateTime(1995, 5, 31, 12, 41, 4, 0, DateTimeKind.Unspecified), "2312313123", "marcos@hotmail.es", "M", 0.0, "Marcos", "caballo14", "user" },
                    { 5, new DateTime(2002, 5, 31, 12, 49, 44, 0, DateTimeKind.Unspecified), "21312321313", "mariobes@hotmail.com", "Bes", 0.0, "Mario", "caballo1213123", "user" }
                });

            migrationBuilder.InsertData(
                table: "Videogames",
                columns: new[] { "Id", "Description", "Developer", "Genre", "Name", "Platform", "Price", "RegisterDate", "Valoration" },
                values: new object[,]
                {
                    { 1, "Eres un Arisen que tiene que recuperar su corazón", "Capcom", "Aventura y Rol", "Dragons dogma 2", "PC PS5 Y XBOX", 70.0, new DateTime(2024, 5, 3, 13, 18, 45, 0, DateTimeKind.Unspecified), 10 },
                    { 2, "Juego de rol muy famoso", "Balve", "Rol", "Persona 5", "PC y PS4", 60.0, new DateTime(2024, 5, 3, 13, 28, 28, 0, DateTimeKind.Unspecified), 10 },
                    { 3, "Subir niveles y matar monstruos", "Square Enix", "Aventuras y Rol", "Final Fantasy XV", "PS5", 50.0, new DateTime(2024, 5, 5, 23, 54, 31, 0, DateTimeKind.Unspecified), 9 },
                    { 4, "Construye tu imperio", "Three Kingdom", "Estrategia", "Manor lords", "PC", 40.0, new DateTime(2024, 5, 6, 0, 8, 2, 0, DateTimeKind.Unspecified), 9 },
                    { 5, "Juego de acción con buena historia", "ASASSA", "Acción", "Max Payne 4", "PC", 40.0, new DateTime(2024, 6, 1, 2, 29, 36, 0, DateTimeKind.Unspecified), 9 },
                    { 6, "adsadad", "sasasasas", "adasdad", "asdadasda", "assaasdas", 30.0, new DateTime(2024, 6, 2, 22, 41, 43, 0, DateTimeKind.Unspecified), 9 }
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Id", "Amount", "Concept", "Date", "Method", "Quantity", "UserId", "VideogameId" },
                values: new object[,]
                {
                    { 1, 100.0, "Ingreso", new DateTime(2024, 5, 6, 0, 9, 2, 0, DateTimeKind.Unspecified), "PayPal", 1.0, 2, null },
                    { 2, 0.0, "Comprar Manor lords", new DateTime(2024, 5, 6, 0, 9, 12, 0, DateTimeKind.Unspecified), null, 1.0, 2, 4 },
                    { 3, 100.0, "Ingreso", new DateTime(2024, 5, 3, 20, 44, 36, 0, DateTimeKind.Unspecified), "Tarjeta de crédito", 1.0, 3, null },
                    { 4, 30.0, "Retirada", new DateTime(2024, 5, 3, 20, 44, 52, 0, DateTimeKind.Unspecified), "PayPal", 1.0, 3, null },
                    { 5, 50.0, "Comprar Persona 5", new DateTime(2024, 5, 5, 23, 33, 6, 0, DateTimeKind.Unspecified), "Tarjeta de crédito", 1.0, 3, null },
                    { 6, 50.0, "Ingreso", new DateTime(2024, 5, 5, 23, 39, 18, 0, DateTimeKind.Unspecified), "Tarjeta de crédito", 1.0, 3, null },
                    { 7, 70.0, "Dragons dogma 2", new DateTime(2024, 5, 5, 23, 39, 36, 0, DateTimeKind.Unspecified), null, 1.0, 3, null },
                    { 8, 100.0, "Ingreso", new DateTime(2024, 5, 5, 23, 52, 22, 0, DateTimeKind.Unspecified), "PayPal", 1.0, 3, null },
                    { 9, 0.0, "Comprar Final Fantasy XV", new DateTime(2024, 5, 5, 23, 55, 20, 0, DateTimeKind.Unspecified), null, 0.0, 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_UserId",
                table: "Operations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Operations_VideogameId",
                table: "Operations",
                column: "VideogameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Videogames");
        }
    }
}
