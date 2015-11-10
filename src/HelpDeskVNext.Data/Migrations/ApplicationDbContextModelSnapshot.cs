using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using HelpDeskVNext.Data.Models;

namespace HelpDeskVNext.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964");

            modelBuilder.Entity("HelpDeskVNext.Data.Entitidades.Avaria", b =>
                {
                    b.Property<int>("AvariaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Designacao");

                    b.HasKey("AvariaId");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Entitidades.Departamento", b =>
                {
                    b.Property<int>("DepartamentoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("DepartamentoId");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Entitidades.Estado", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Designacao");

                    b.HasKey("EstadoId");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Entitidades.Prioridade", b =>
                {
                    b.Property<int>("PrioridadeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Designacao");

                    b.HasKey("PrioridadeId");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Entitidades.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AvariaId");

                    b.Property<DateTime?>("DataConclusao");

                    b.Property<DateTime>("DataInsercao");

                    b.Property<int>("DepartamentoId");

                    b.Property<string>("Descricao");

                    b.Property<int>("EstadoId");

                    b.Property<int>("PrioridadeId");

                    b.Property<string>("TecnicoId");

                    b.Property<string>("Titulo");

                    b.Property<string>("Utilizador");

                    b.HasKey("TicketId");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("DepartamentoDepartamentoId");

                    b.Property<string>("Email")
                        .Annotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Nome");

                    b.Property<string>("NormalizedEmail")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .Annotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.Index("NormalizedEmail")
                        .Annotation("Relational:Name", "EmailIndex");

                    b.Index("NormalizedUserName")
                        .Annotation("Relational:Name", "UserNameIndex");

                    b.Annotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .Annotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.Index("NormalizedName")
                        .Annotation("Relational:Name", "RoleNameIndex");

                    b.Annotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.Annotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.Annotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.Annotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.Annotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Entitidades.Ticket", b =>
                {
                    b.HasOne("HelpDeskVNext.Data.Entitidades.Avaria")
                        .WithMany()
                        .ForeignKey("AvariaId");

                    b.HasOne("HelpDeskVNext.Data.Entitidades.Departamento")
                        .WithMany()
                        .ForeignKey("DepartamentoId");

                    b.HasOne("HelpDeskVNext.Data.Entitidades.Estado")
                        .WithMany()
                        .ForeignKey("EstadoId");

                    b.HasOne("HelpDeskVNext.Data.Entitidades.Prioridade")
                        .WithMany()
                        .ForeignKey("PrioridadeId");

                    b.HasOne("HelpDeskVNext.Data.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("TecnicoId");
                });

            modelBuilder.Entity("HelpDeskVNext.Data.Models.ApplicationUser", b =>
                {
                    b.HasOne("HelpDeskVNext.Data.Entitidades.Departamento")
                        .WithMany()
                        .ForeignKey("DepartamentoDepartamentoId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .ForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HelpDeskVNext.Data.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HelpDeskVNext.Data.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .ForeignKey("RoleId");

                    b.HasOne("HelpDeskVNext.Data.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });
        }
    }
}
