using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using RickAndMorty.Model.Entity;

#nullable disable

namespace RickAndMorty.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likeds",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReferenceId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLiked = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ChangedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    IsLogin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CheatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ChangedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserId", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "Index_Type_And_ReferenceId",
                table: "Likeds",
                columns: new[] { "Type", "ReferenceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_Type_And_ReferenceId_And_IsLiked",
                table: "Likeds",
                columns: new[] { "Type", "ReferenceId", "IsLiked" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true);
            
            var dateTime = DateTimeOffset.UtcNow;
            migrationBuilder.InsertData("Users", new[]
            {
                nameof(User.CheatedAt),
                nameof(User.ChangedAt),
                nameof(User.Login),
                nameof(User.Password),
                nameof(User.IsLogin)
            }, new object?[]
            {
                dateTime,
                dateTime,
                "admin",
                "12345678",
                false
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likeds");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
