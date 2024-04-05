using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesWebProyectoFinal.Migrations
{
    public partial class CreartablasSAR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "ConfigSAR",
                columns: table => new
                {
                    RTN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(120)", nullable: false),
                    DireccionFacturacion = table.Column<string>(type: "varchar(600)", nullable: false),
                    TelefonoFacturacion = table.Column<string>(type: "char(9)", nullable: false),
                    CorreoFacturacion = table.Column<string>(type: "varchar(255)", nullable: false),
                    PuntoEmisionOnline = table.Column<string>(type: "char(3)", nullable: false),
                    CodigoEstablecimientoOnline = table.Column<string>(type: "char(3)", nullable: false),
                    CAIVigente = table.Column<string>(type: "char(37)", nullable: false),
                    CorrelativoFacturas = table.Column<int>(type: "int", nullable: false),
                    RangoDesde = table.Column<int>(type: "int", nullable: false),
                    RangoHasta = table.Column<int>(type: "int", nullable: false),
                    FechaLimiteEmision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioCreacionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioModificacionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigSAR", x => x.RTN);
                    table.ForeignKey(
                        name: "FK_ConfigSAR_Usuario_UsuarioCreacionId",
                        column: x => x.UsuarioCreacionId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfigSAR_Usuario_UsuarioModificacionId",
                        column: x => x.UsuarioModificacionId,
                        principalTable: "Usuario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoricoCAIS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConfiguracionRTN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CAI = table.Column<string>(type: "char(37)", nullable: false),
                    RangoDesde = table.Column<int>(type: "int", nullable: false),
                    RangoHasta = table.Column<int>(type: "int", nullable: false),
                    FechaLimiteEmision = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoCAIS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricoCAIS_ConfigSAR_ConfiguracionRTN",
                        column: x => x.ConfiguracionRTN,
                        principalTable: "ConfigSAR",
                        principalColumn: "RTN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigSAR_UsuarioCreacionId",
                table: "ConfigSAR",
                column: "UsuarioCreacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigSAR_UsuarioModificacionId",
                table: "ConfigSAR",
                column: "UsuarioModificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoCAIS_ConfiguracionRTN",
                table: "HistoricoCAIS",
                column: "ConfiguracionRTN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoCAIS");

            migrationBuilder.DropTable(
                name: "ConfigSAR");

        }
    }
}
