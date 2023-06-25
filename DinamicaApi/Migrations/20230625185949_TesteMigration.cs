using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DinamicaApi.Migrations
{
    /// <inheritdoc />
    public partial class TesteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TextosEnviados",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeParticipante = table.Column<string>(type: "TEXT", nullable: true),
                    Texto = table.Column<string>(type: "TEXT", nullable: true),
                    IdSala = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextosEnviados", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextosEnviados");
        }
    }
}
