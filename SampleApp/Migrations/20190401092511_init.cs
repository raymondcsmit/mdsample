using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleApp.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "Basic",
                columns: table => new
                {
                    BasicID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PasNumber = table.Column<string>(nullable: true),
                    Forenames = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    SexCode = table.Column<string>(nullable: true),
                    HomeTelephoneNumber = table.Column<string>(nullable: true),
                    FkPatientID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basic", x => x.BasicID);
                    table.ForeignKey(
                        name: "FK_Basic_Patient_FkPatientID",
                        column: x => x.FkPatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GpDetail",
                columns: table => new
                {
                    GpDetailID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GpCode = table.Column<string>(nullable: true),
                    GpSurname = table.Column<string>(nullable: true),
                    GpInitials = table.Column<string>(nullable: true),
                    GpPhone = table.Column<string>(nullable: true),
                    FkPatientID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpDetail", x => x.GpDetailID);
                    table.ForeignKey(
                        name: "FK_GpDetail_Patient_FkPatientID",
                        column: x => x.FkPatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    NextOfKinID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NokName = table.Column<string>(nullable: true),
                    NokRelationshipCode = table.Column<string>(nullable: true),
                    NokAddressLine1 = table.Column<string>(nullable: true),
                    NokAddressLine2 = table.Column<string>(nullable: true),
                    NokAddressLine3 = table.Column<string>(nullable: true),
                    NokAddressLine4 = table.Column<string>(nullable: true),
                    NokPostcode = table.Column<string>(nullable: true),
                    FkPatientID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.NextOfKinID);
                    table.ForeignKey(
                        name: "FK_NextOfKin_Patient_FkPatientID",
                        column: x => x.FkPatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Basic_FkPatientID",
                table: "Basic",
                column: "FkPatientID",
                unique: true,
                filter: "[FkPatientID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GpDetail_FkPatientID",
                table: "GpDetail",
                column: "FkPatientID");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_FkPatientID",
                table: "NextOfKin",
                column: "FkPatientID",
                unique: true,
                filter: "[FkPatientID] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basic");

            migrationBuilder.DropTable(
                name: "GpDetail");

            migrationBuilder.DropTable(
                name: "NextOfKin");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
