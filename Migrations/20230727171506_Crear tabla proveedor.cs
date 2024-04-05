using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesWebProyectoFinal.Migrations
{
    public partial class Creartablaproveedor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proveedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(120)", nullable: false),
                    Telefono = table.Column<string>(type: "varchar(9)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Celular = table.Column<string>(type: "varchar(9)", nullable: false),
                    Direccion = table.Column<string>(type: "varchar(600)", nullable: false),
                    UsuarioCreadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsuarioModificadorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Eliminado = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proveedor_Usuario_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proveedor_Usuario_UsuarioModificadorId",
                        column: x => x.UsuarioModificadorId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_UsuarioCreadorId",
                table: "Proveedor",
                column: "UsuarioCreadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedor_UsuarioModificadorId",
                table: "Proveedor",
                column: "UsuarioModificadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proveedor");
        }
    }
}
