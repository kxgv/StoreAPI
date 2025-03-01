using System;
using Microsoft.EntityFrameworkCore.Migrations;
using BCrypt.Net;

#nullable disable

namespace StoreAPI.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UsersAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
    name: "AspNetUsers",
    columns: table => new
    {
        Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
        UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
        EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
        PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
        SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
        ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
        PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: true),
        TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: true),
        LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
        LockoutEnabled = table.Column<bool>(type: "bit", nullable: true),
        AccessFailedCount = table.Column<int>(type: "int", nullable: true),
        IsAdmin = table.Column<bool>(type: "bit", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
    });

            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!");

            migrationBuilder.Sql($@"
            INSERT INTO Users 
                (Username, PasswordHash, Email, Role, LastLogin)
            VALUES
                (
                'admin-kx', 
                '{passwordHash}', 
                'admin@kx.com', 
                'Admin', 
                GETUTCDATE())"
            );

            migrationBuilder.Sql($@"
                INSERT INTO Products (Name, Guid, Color, Price, Size, [Description], IsFeatured, ImageURL)
                VALUES('Producto de Prueba 5', NEWID(), 'Amarillo', 20.22, 'M', 'Este es un producto de prueba para la base de datos.', 1, 'https://i.postimg.cc/DZ7zcb1K/ropa-3.jpg');

                INSERT INTO Products (Name, Guid, Color, Price, Size, [Description], IsFeatured, ImageURL)
                VALUES('Producto de Prueba 6', NEWID(), 'Amarillo', 20.22, 'M', 'Este es un producto de prueba para la base de datos.', 1, 'https://i.postimg.cc/QxzHrCZp/ropa-9.jpg');

                INSERT INTO Products (Name, Guid, Color, Price, Size, [Description], IsFeatured, ImageURL)
                VALUES('Producto de Prueba 7', NEWID(), 'Amarillo', 20.22, 'M', 'Este es un producto de prueba para la base de datos.', 1, 'https://i.postimg.cc/YS69gxG3/ropa-5.jpg');

                INSERT INTO Products (Name, Guid, Color, Price, Size, [Description], IsFeatured, ImageURL)
                VALUES('Producto de Prueba 8', NEWID(), 'Amarillo', 20.22, 'M', 'Este es un producto de prueba para la base de datos.', 1, 'https://i.postimg.cc/WzWtzbFc/ropa-8.jpg');

                INSERT INTO Products (Name, Guid, Color, Price, Size, [Description], IsFeatured, ImageURL)
                VALUES('Producto de Prueba 9', NEWID(), 'Amarillo', 20.22, 'M', 'Este es un producto de prueba para la base de datos.', 1, 'https://i.postimg.cc/WzWtzbFc/ropa-8.jpg');

                INSERT INTO Products (Name, Guid, Color, Price, Size, [Description], IsFeatured, ImageURL)
                VALUES('Producto de Prueba 10', NEWID(), 'Amarillo', 20.22, 'M', 'Este es un producto de prueba para la base de datos.', 1, 'https://i.postimg.cc/YS69gxG3/ropa-5.jpg');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}   
