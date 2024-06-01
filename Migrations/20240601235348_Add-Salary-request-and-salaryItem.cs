using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayrollManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryrequestandsalaryItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryRequests_Employees_EmployeeId",
                table: "SalaryRequests");

            migrationBuilder.DropIndex(
                name: "IX_SalaryRequests_EmployeeId",
                table: "SalaryRequests");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalaryRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SalaryRequests");

            migrationBuilder.RenameColumn(
                name: "ApprovedByManager",
                table: "SalaryRequests",
                newName: "IsApprovedByManager");

            migrationBuilder.RenameColumn(
                name: "ApprovedByAccountant",
                table: "SalaryRequests",
                newName: "IsApprovedByAccountant");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerDocumentPath",
                table: "SalaryRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountantDocumentPath",
                table: "SalaryRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SalaryRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryRequestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryRequestId = table.Column<int>(type: "int", nullable: false),
                    SalaryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryRequestItems_SalaryLists_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "SalaryLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryRequestItems_SalaryRequests_SalaryRequestId",
                        column: x => x.SalaryRequestId,
                        principalTable: "SalaryRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryRequestItems_SalaryId",
                table: "SalaryRequestItems",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryRequestItems_SalaryRequestId",
                table: "SalaryRequestItems",
                column: "SalaryRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryRequestItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SalaryRequests");

            migrationBuilder.RenameColumn(
                name: "IsApprovedByManager",
                table: "SalaryRequests",
                newName: "ApprovedByManager");

            migrationBuilder.RenameColumn(
                name: "IsApprovedByAccountant",
                table: "SalaryRequests",
                newName: "ApprovedByAccountant");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerDocumentPath",
                table: "SalaryRequests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountantDocumentPath",
                table: "SalaryRequests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SalaryRequests",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "SalaryRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryRequests_EmployeeId",
                table: "SalaryRequests",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryRequests_Employees_EmployeeId",
                table: "SalaryRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
