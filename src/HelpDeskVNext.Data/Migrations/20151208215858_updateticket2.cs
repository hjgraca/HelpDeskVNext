using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace HelpDeskVNext.Data.Migrations
{
    public partial class updateticket2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Avaria_AvariaId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Departamento_DepartamentoId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Estado_EstadoId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Prioridade_PrioridadeId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_ApplicationUser_UtilizadorId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "UtilizadorId", table: "Ticket");
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUtilizadorId",
                table: "Ticket",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "TecnicoId",
                table: "Ticket",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Avaria_AvariaId",
                table: "Ticket",
                column: "AvariaId",
                principalTable: "Avaria",
                principalColumn: "AvariaId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_ApplicationUser_CreatedByUtilizadorId",
                table: "Ticket",
                column: "CreatedByUtilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Departamento_DepartamentoId",
                table: "Ticket",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Estado_EstadoId",
                table: "Ticket",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "EstadoId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Prioridade_PrioridadeId",
                table: "Ticket",
                column: "PrioridadeId",
                principalTable: "Prioridade",
                principalColumn: "PrioridadeId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_ApplicationUser_TecnicoId",
                table: "Ticket",
                column: "TecnicoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Avaria_AvariaId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_ApplicationUser_CreatedByUtilizadorId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Departamento_DepartamentoId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Estado_EstadoId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_Prioridade_PrioridadeId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_Ticket_ApplicationUser_TecnicoId", table: "Ticket");
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId", table: "AspNetRoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId", table: "AspNetUserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId", table: "AspNetUserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_IdentityRole_RoleId", table: "AspNetUserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<string>_ApplicationUser_UserId", table: "AspNetUserRoles");
            migrationBuilder.DropColumn(name: "CreatedByUtilizadorId", table: "Ticket");
            migrationBuilder.DropColumn(name: "TecnicoId", table: "Ticket");
            migrationBuilder.AddColumn<string>(
                name: "UtilizadorId",
                table: "Ticket",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Avaria_AvariaId",
                table: "Ticket",
                column: "AvariaId",
                principalTable: "Avaria",
                principalColumn: "AvariaId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Departamento_DepartamentoId",
                table: "Ticket",
                column: "DepartamentoId",
                principalTable: "Departamento",
                principalColumn: "DepartamentoId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Estado_EstadoId",
                table: "Ticket",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "EstadoId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Prioridade_PrioridadeId",
                table: "Ticket",
                column: "PrioridadeId",
                principalTable: "Prioridade",
                principalColumn: "PrioridadeId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_ApplicationUser_UtilizadorId",
                table: "Ticket",
                column: "UtilizadorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<string>_IdentityRole_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<string>_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<string>_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_IdentityRole_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<string>_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
