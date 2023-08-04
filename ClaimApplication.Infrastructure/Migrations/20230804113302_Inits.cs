using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppealPredmets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifyBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppealTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfResponsiblePeople",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Inn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NameOfBussiness = table.Column<string>(type: "text", nullable: false),
                    AppealNumber = table.Column<int>(type: "integer", nullable: false),
                    AppealDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MembershipAgreementNumber = table.Column<string>(type: "text", nullable: false),
                    MembershipAgreementDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CertificateNumber = table.Column<string>(type: "text", nullable: false),
                    CertificateGivenDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreviousAppeal = table.Column<string>(type: "text", nullable: true),
                    AppealText = table.Column<string>(type: "text", nullable: false),
                    TotalClaimAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    MainDebt = table.Column<decimal>(type: "numeric", nullable: true),
                    CalculatedLateCharges = table.Column<decimal>(type: "numeric", nullable: true),
                    AmountOfFine = table.Column<decimal>(type: "numeric", nullable: true),
                    Percentage = table.Column<decimal>(type: "numeric", nullable: true),
                    AppealPredmetId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppealTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdinalNumber = table.Column<string>(type: "text", nullable: false),
                    Inn = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypeOfResponsiblePersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
