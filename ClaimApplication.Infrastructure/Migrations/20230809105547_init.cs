using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClaimApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppealPredmets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppealPredmets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppealTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppealTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfBusiness = table.Column<string>(type: "text", nullable: true),
                    FullNameOfManager = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<int>(type: "integer", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    PostIndex = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    MobileNumberFirst = table.Column<string>(type: "text", nullable: true),
                    MobileNumberSecond = table.Column<string>(type: "text", nullable: true),
                    Fax = table.Column<string>(type: "text", nullable: true),
                    MobileNumberExtra = table.Column<string>(type: "text", nullable: true),
                    EmailFirst = table.Column<string>(type: "text", nullable: true),
                    EmailSecond = table.Column<string>(type: "text", nullable: true),
                    WebSite = table.Column<string>(type: "text", nullable: true),
                    SkypeProfile = table.Column<string>(type: "text", nullable: true),
                    FaceBookProfile = table.Column<string>(type: "text", nullable: true),
                    TelegramProfile = table.Column<string>(type: "text", nullable: true),
                    ExtraProfile = table.Column<string>(type: "text", nullable: true),
                    BussinessRegesteredDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "text", nullable: true),
                    HasCopyOfRegistrationCerteficate = table.Column<bool>(type: "boolean", nullable: true),
                    Inn = table.Column<string>(type: "text", nullable: true),
                    OKED = table.Column<string>(type: "text", nullable: true),
                    MainActivityType = table.Column<int>(type: "integer", nullable: true),
                    BussinessCategory = table.Column<int>(type: "integer", nullable: true),
                    NumberOfEmployees = table.Column<int>(type: "integer", nullable: true),
                    NameOfBank = table.Column<string>(type: "text", nullable: true),
                    CodeOfBank = table.Column<string>(type: "text", nullable: true),
                    BankAccount = table.Column<string>(type: "text", nullable: true),
                    AnnualTurnoverOfEnterprise = table.Column<decimal>(type: "numeric", nullable: true),
                    AnnualPaidTax = table.Column<decimal>(type: "numeric", nullable: true),
                    AnnualExportAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    AnnualImportAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    AnnualProductionAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    BrandName = table.Column<string>(type: "text", nullable: true),
                    BithDateOfManager = table.Column<string>(type: "text", nullable: true),
                    SeriesOfPassport = table.Column<string>(type: "text", nullable: true),
                    NumberOfPassport = table.Column<string>(type: "text", nullable: true),
                    PassportGivenFrom = table.Column<string>(type: "text", nullable: true),
                    Nationality = table.Column<string>(type: "text", nullable: true),
                    ForeignLanguage = table.Column<int>(type: "integer", nullable: true),
                    EducationDegree = table.Column<string>(type: "text", nullable: true),
                    ExtraInformation = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfResponsiblePeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfResponsiblePeople", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Inn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameOfBussiness = table.Column<string>(type: "text", nullable: false),
                    AppealNumber = table.Column<int>(type: "integer", nullable: false),
                    AppealDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    MembershipAgreementNumber = table.Column<string>(type: "text", nullable: false),
                    MembershipAgreementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CertificateNumber = table.Column<string>(type: "text", nullable: false),
                    CertificateGivenDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PreviousAppeal = table.Column<string>(type: "text", nullable: true),
                    AppealText = table.Column<string>(type: "text", nullable: false),
                    TotalClaimAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    MainDebt = table.Column<decimal>(type: "numeric", nullable: true),
                    CalculatedLateCharges = table.Column<decimal>(type: "numeric", nullable: true),
                    AmountOfFine = table.Column<decimal>(type: "numeric", nullable: true),
                    Percentage = table.Column<decimal>(type: "numeric", nullable: true),
                    AppealPredmetId = table.Column<int>(type: "integer", nullable: false),
                    AppealTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AppealPredmets_AppealPredmetId",
                        column: x => x.AppealPredmetId,
                        principalTable: "AppealPredmets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_AppealTypes_AppealTypeId",
                        column: x => x.AppealTypeId,
                        principalTable: "AppealTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponsiblePeople",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrdinalNumber = table.Column<string>(type: "text", nullable: false),
                    Inn = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<int>(type: "integer", nullable: false),
                    TypeOfResponsiblePersonId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsiblePeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResponsiblePeople_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponsiblePeople_TypeOfResponsiblePeople_TypeOfResponsible~",
                        column: x => x.TypeOfResponsiblePersonId,
                        principalTable: "TypeOfResponsiblePeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppealPredmetId",
                table: "Applications",
                column: "AppealPredmetId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppealTypeId",
                table: "Applications",
                column: "AppealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePeople_ApplicationId",
                table: "ResponsiblePeople",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResponsiblePeople_TypeOfResponsiblePersonId",
                table: "ResponsiblePeople",
                column: "TypeOfResponsiblePersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipApplications");

            migrationBuilder.DropTable(
                name: "ResponsiblePeople");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "TypeOfResponsiblePeople");

            migrationBuilder.DropTable(
                name: "AppealPredmets");

            migrationBuilder.DropTable(
                name: "AppealTypes");
        }
    }
}
