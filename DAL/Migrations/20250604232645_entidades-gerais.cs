using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class entidadesgerais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePhoto",
                table: "user",
                type: "bytea",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChipNumber",
                table: "pet",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ApproximateBirthDate",
                table: "pet",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Acquisition",
                table: "pet",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePhoto",
                table: "pet",
                type: "bytea",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "breed",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DocumentNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IssuingAgency = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_pet_PetId",
                        column: x => x.PetId,
                        principalTable: "pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "medical_event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    EventDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ProfessionalName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_event_pet_PetId",
                        column: x => x.PetId,
                        principalTable: "pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pet_photo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "bytea", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pet_photo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pet_photo_pet_PetId",
                        column: x => x.PetId,
                        principalTable: "pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document_attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileData = table.Column<byte[]>(type: "bytea", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_attachment_document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diagnosis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Condition = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    recommended_treatment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DiagnosisDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_diagnosis_medical_event_MedicalEventId",
                        column: x => x.MedicalEventId,
                        principalTable: "medical_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExamType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Result = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExamDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exam_medical_event_MedicalEventId",
                        column: x => x.MedicalEventId,
                        principalTable: "medical_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medical_attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FileName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FileData = table.Column<byte[]>(type: "bytea", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medical_attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medical_attachment_medical_event_MedicalEventId",
                        column: x => x.MedicalEventId,
                        principalTable: "medical_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medication",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Dosage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Frequency = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_medication_medical_event_MedicalEventId",
                        column: x => x.MedicalEventId,
                        principalTable: "medical_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vaccine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Batch = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NextDose = table.Column<DateOnly>(type: "date", nullable: true),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vaccine_medical_event_MedicalEventId",
                        column: x => x.MedicalEventId,
                        principalTable: "medical_event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_diagnosis_MedicalEventId",
                table: "diagnosis",
                column: "MedicalEventId");

            migrationBuilder.CreateIndex(
                name: "IX_document_PetId",
                table: "document",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_document_attachment_DocumentId",
                table: "document_attachment",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_exam_MedicalEventId",
                table: "exam",
                column: "MedicalEventId");

            migrationBuilder.CreateIndex(
                name: "IX_medical_attachment_MedicalEventId",
                table: "medical_attachment",
                column: "MedicalEventId");

            migrationBuilder.CreateIndex(
                name: "IX_medical_event_PetId",
                table: "medical_event",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_medication_MedicalEventId",
                table: "medication",
                column: "MedicalEventId");

            migrationBuilder.CreateIndex(
                name: "IX_pet_photo_PetId",
                table: "pet_photo",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_vaccine_MedicalEventId",
                table: "vaccine",
                column: "MedicalEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diagnosis");

            migrationBuilder.DropTable(
                name: "document_attachment");

            migrationBuilder.DropTable(
                name: "exam");

            migrationBuilder.DropTable(
                name: "medical_attachment");

            migrationBuilder.DropTable(
                name: "medication");

            migrationBuilder.DropTable(
                name: "pet_photo");

            migrationBuilder.DropTable(
                name: "vaccine");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.DropTable(
                name: "medical_event");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "pet");

            migrationBuilder.AlterColumn<string>(
                name: "ChipNumber",
                table: "pet",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproximateBirthDate",
                table: "pet",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Acquisition",
                table: "pet",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "breed",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
