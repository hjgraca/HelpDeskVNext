using System;
using System.Linq;
using System.Threading.Tasks;
using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace HelpDeskVNext.Data.Models
{
    public static class SampleData
    {
        public static async Task InitializeIdentityDatabaseAsync(IServiceProvider serviceProvider)
        {
            CreateData(serviceProvider);
            await CreateAdminUser(serviceProvider);
        }

        private static void CreateData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (!context.Estados.Any())
            {
                context.Estados.AddRange(
                    new Estado { Designacao = "Novo" },
                    new Estado { Designacao = "Em progresso" },
                    new Estado { Designacao = "Concluido" }
                    );
            }

            if (!context.Prioridades.Any())
            {
                context.Prioridades.AddRange(
                    new Prioridade { Designacao = "Baixa" },
                    new Prioridade { Designacao = "Media" },
                    new Prioridade { Designacao = "Alta" }
                    );
            }

            if (!context.Departamentos.Any())
            {
                context.Departamentos.AddRange(
                    new Departamento { Nome = "N/A" },
                    new Departamento { Nome = "Sistemas" },
                    new Departamento { Nome = "Vendas" },
                    new Departamento { Nome = "Marketing" }
                    );
            }
        }

        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<IOptions<IdentityDbContextOptions>>().Value;
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string adminRole = options.AdministratorRole.Trim();

            if (!string.IsNullOrEmpty(options.Roles))
            {
                foreach (var role in options.Roles.Split(','))
                {
                    if (!await roleManager.RoleExistsAsync(role.Trim()))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role.Trim()));
                    }
                }
            }

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            var user = await userManager.FindByNameAsync(options.DefaultAdminUserName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = options.DefaultAdminUserName,
                    Email = options.DefaultAdminUserName,
                    Nome = "Administrador",
                    DepartamentoId = 2
                };
                await userManager.CreateAsync(user, options.DefaultAdminPassword);
                await userManager.AddToRoleAsync(user, adminRole);
            }
        }
    }
}
