using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesWebProyectoFinal.Migrations
{
    public partial class Agregarcamposreferenciadepagoyordenafactura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrdenId",
                table: "Facturas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ReferenciaPago",
                table: "Facturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_OrdenId",
                table: "Facturas",
                column: "OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturas_Ordenes_OrdenId",
                table: "Facturas",
                column: "OrdenId",
                principalTable: "Ordenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturas_Ordenes_OrdenId",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_OrdenId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "OrdenId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "ReferenciaPago",
                table: "Facturas");
        }
    }
}
