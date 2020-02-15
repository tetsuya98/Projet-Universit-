using Microsoft.EntityFrameworkCore.Migrations;

namespace Universite.Data.Migrations
{
    public partial class AjoutTd3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Etudiant",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Etudiant",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormationID",
                table: "Etudiant",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enseignant",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: false),
                    Prenom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignant", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntituleDiplome = table.Column<string>(nullable: false),
                    AnneeDiplome = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(nullable: false),
                    Intitule = table.Column<string>(nullable: false),
                    FormationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UE_Formation_FormationID",
                        column: x => x.FormationID,
                        principalTable: "Formation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enseigne",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnseignantID = table.Column<int>(nullable: true),
                    UEID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseigne", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Enseigne_Enseignant_EnseignantID",
                        column: x => x.EnseignantID,
                        principalTable: "Enseignant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enseigne_UE_UEID",
                        column: x => x.UEID,
                        principalTable: "UE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valeur = table.Column<float>(nullable: false),
                    UEID = table.Column<int>(nullable: true),
                    EtudiantID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Note_Etudiant_EtudiantID",
                        column: x => x.EtudiantID,
                        principalTable: "Etudiant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Note_UE_UEID",
                        column: x => x.UEID,
                        principalTable: "UE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etudiant_FormationID",
                table: "Etudiant",
                column: "FormationID");

            migrationBuilder.CreateIndex(
                name: "IX_Enseigne_EnseignantID",
                table: "Enseigne",
                column: "EnseignantID");

            migrationBuilder.CreateIndex(
                name: "IX_Enseigne_UEID",
                table: "Enseigne",
                column: "UEID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_EtudiantID",
                table: "Note",
                column: "EtudiantID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UEID",
                table: "Note",
                column: "UEID");

            migrationBuilder.CreateIndex(
                name: "IX_UE_FormationID",
                table: "UE",
                column: "FormationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Etudiant_Formation_FormationID",
                table: "Etudiant",
                column: "FormationID",
                principalTable: "Formation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Etudiant_Formation_FormationID",
                table: "Etudiant");

            migrationBuilder.DropTable(
                name: "Enseigne");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Enseignant");

            migrationBuilder.DropTable(
                name: "UE");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropIndex(
                name: "IX_Etudiant_FormationID",
                table: "Etudiant");

            migrationBuilder.DropColumn(
                name: "FormationID",
                table: "Etudiant");

            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "Etudiant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Etudiant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
