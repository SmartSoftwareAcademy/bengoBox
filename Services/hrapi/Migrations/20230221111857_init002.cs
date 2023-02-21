using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hrapi.Migrations
{
    public partial class init002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryDetails_PaymentData_PaymentDataId",
                table: "SalaryDetails");

            migrationBuilder.DropIndex(
                name: "IX_SalaryDetails_PaymentDataId",
                table: "SalaryDetails");

            migrationBuilder.RenameColumn(
                name: "PaymentDataId",
                table: "SalaryDetails",
                newName: "SalaryProcessingOptionId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "PaymentData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContractsId",
                table: "HRDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContractStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ContractDuration = table.Column<int>(type: "int", nullable: true),
                    MonthlySalary = table.Column<double>(type: "double", nullable: false),
                    Notes = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDetails_SalaryProcessingOptionId",
                table: "SalaryDetails",
                column: "SalaryProcessingOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentData_EmployeeId",
                table: "PaymentData",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRDetails_ContractsId",
                table: "HRDetails",
                column: "ContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeId",
                table: "Contracts",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HRDetails_Contracts_ContractsId",
                table: "HRDetails",
                column: "ContractsId",
                principalTable: "Contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentData_Employees_EmployeeId",
                table: "PaymentData",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryDetails_PaymentData_SalaryProcessingOptionId",
                table: "SalaryDetails",
                column: "SalaryProcessingOptionId",
                principalTable: "PaymentData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HRDetails_Contracts_ContractsId",
                table: "HRDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentData_Employees_EmployeeId",
                table: "PaymentData");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryDetails_PaymentData_SalaryProcessingOptionId",
                table: "SalaryDetails");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_SalaryDetails_SalaryProcessingOptionId",
                table: "SalaryDetails");

            migrationBuilder.DropIndex(
                name: "IX_PaymentData_EmployeeId",
                table: "PaymentData");

            migrationBuilder.DropIndex(
                name: "IX_HRDetails_ContractsId",
                table: "HRDetails");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "PaymentData");

            migrationBuilder.DropColumn(
                name: "ContractsId",
                table: "HRDetails");

            migrationBuilder.RenameColumn(
                name: "SalaryProcessingOptionId",
                table: "SalaryDetails",
                newName: "PaymentDataId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryDetails_PaymentDataId",
                table: "SalaryDetails",
                column: "PaymentDataId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryDetails_PaymentData_PaymentDataId",
                table: "SalaryDetails",
                column: "PaymentDataId",
                principalTable: "PaymentData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
