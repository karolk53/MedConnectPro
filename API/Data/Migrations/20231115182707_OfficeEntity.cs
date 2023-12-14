using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class OfficeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Shedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Doctors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shedules_OfficeId",
                table: "Shedules",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_OfficeId",
                table: "Doctors",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_AddressId",
                table: "Offices",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Offices_OfficeId",
                table: "Doctors",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shedules_Offices_OfficeId",
                table: "Shedules",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Offices_OfficeId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Shedules_Offices_OfficeId",
                table: "Shedules");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Shedules_OfficeId",
                table: "Shedules");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_OfficeId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Shedules");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Doctors");
        }
    }
}
