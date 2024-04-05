using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesWebProyectoFinal.Migrations
{
    public partial class Creartablacategorias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(60)", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(300)", nullable: false),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioCreadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsuarioModificadorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Usuario_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Categorias_Usuario_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_UsuarioCreadorId",
                table: "Categorias",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_UsuarioModificadorId",
                table: "Categorias",
                column: "UsuarioModificadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
