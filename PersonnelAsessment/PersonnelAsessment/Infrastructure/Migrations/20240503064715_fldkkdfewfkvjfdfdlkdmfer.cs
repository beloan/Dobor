using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fldkkdfewfkvjfdfdlkdmfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "private");

            migrationBuilder.CreateTable(
                name: "Admins",
                schema: "private",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    INN = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organisations_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FIO = table.Column<string>(type: "text", nullable: true),
                    Education = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leads_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    ImageName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    OrganisationId = table.Column<int>(type: "integer", nullable: false),
                    LeadId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forms_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

          
            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    OrganisationsId = table.Column<int>(type: "integer", nullable: false),
                    LeadsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => new { x.OrganisationsId, x.LeadsId });
                    table.ForeignKey(
                        name: "FK_Workers_Organisations_OrganisationsId",
                        column: x => x.OrganisationsId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Leads_LeadsId",
                        column: x => x.LeadsId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    FIO = table.Column<string>(type: "text", nullable: true),
                    FormId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assigments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: true),
                    FormId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assigments_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jobLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FormId = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: true),
                    IndexNum = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobLists_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AssigmentId = table.Column<int>(type: "integer", nullable: false),
                    WorkerId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_Assigments_AssigmentId",
                        column: x => x.AssigmentId,
                        principalTable: "Assigments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "private",
                table: "Admins",
                columns: new[] { "Id", "Email", "Password", "Salt" },
                values: new object[] { 1, "admin1@ymail.com", "wVHAesBpD3fGGREvMkCqyyHABbgjeU22v2OdcQ8M+Jo=", new byte[] { 14, 219, 106, 150, 6, 252, 1, 234, 149, 170, 167, 133, 176, 226, 192, 211 } });


            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActivated", "Password", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, "andrej.04@mail.ru", true, "eKPL44bLuWH5mJJbVH2vbTcXdwDjm8Fb4CIHB0NZ8S0=", "organisation", new byte[] { 58, 94, 103, 112, 230, 149, 61, 51, 48, 95, 116, 77, 14, 106, 8, 70 } },
                    { 2, "andrej@example.ru", true, "ABvKZwAsbcFGPDmhT1elWoFPIQUPp+MZO9Vt1eiukkI=", "student", new byte[] { 53, 104, 17, 236, 74, 82, 90, 207, 169, 201, 221, 58, 24, 250, 69, 58 } },
                    { 3, "andrej@example.ru", true, "mMTQiS0BjDcIwRpa7Z9QxBheAz/KOKG832xMCYqdyt4=", "teacher", new byte[] { 174, 156, 101, 39, 72, 119, 148, 98, 42, 83, 2, 59, 168, 117, 76, 155 } }
                });

            migrationBuilder.InsertData(
                table: "Organisations",
                columns: new[] { "Id", "Address", "INN", "Name" },
                values: new object[] { 1, null, null, null });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "Education", "FIO" },
                values: new object[] { 3, null, "Александр Македонский" });

            migrationBuilder.InsertData(
                table: "Forms",
                columns: new[] { "Id", "LeadId", "Number", "OrganisationId" },
                values: new object[] { 1, 3, 1, 1 });

            migrationBuilder.InsertData(
                table: "Assigments",
                columns: new[] { "Id", "Date", "FormId", "Topic" },
                values: new object[] { 1, new DateOnly(2024, 4, 10), 1, "Устройство сердца" });

            migrationBuilder.InsertData(
                table: "JobLists",
                columns: new[] { "Id", "Day", "FormId", "IndexNum", },
                values: new object[] { 1, "Monday", 1, 1,});

            migrationBuilder.InsertData(
                table: "Workers",
                columns: new[] { "Id", "FIO", "FormId" },
                values: new object[] { 2, "Зубенко Михаил Петрович", 1 });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "AssigmentId", "WorkerId", "Value" },
                values: new object[] { 1, 1, 2, "5" });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_Email",
                schema: "private",
                table: "Admins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_LeadId",
                table: "Forms",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_OrganisationId",
                table: "Forms",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AssigmentId",
                table: "Grades",
                column: "AssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_WorkerId",
                table: "Grades",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_FormId",
                table: "Lessons",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLists_FormId",
                table: "JobLists",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_FormId",
                table: "Workers",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workers_LeadsId",
                table: "Workers",
                column: "LeadsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins",
                schema: "private");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "JobListss");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "WorkProgramms");

            migrationBuilder.DropTable(
                name: "Assigments");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
