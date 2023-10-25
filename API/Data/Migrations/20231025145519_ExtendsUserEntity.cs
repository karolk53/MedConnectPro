using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendsUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Patients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Patients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Patients",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Patients",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Patients",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Patients",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Patients",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Patients",
                newName: "firstName");
        }
    }
}
