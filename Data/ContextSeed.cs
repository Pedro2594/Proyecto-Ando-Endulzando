using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace DesWebProyectoFinal.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cliente.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            var usuarioDefecto = new Usuario
            {
                UserName = "superadmin@test.com",
                Email = "superadmin@test.com",
                PrimerNombre = "Super",
                PrimerApellido = "Admin",
                DNI = "9999999999999",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FotoPerfil = null
            };
            if(userManager.Users.All(u=> u.Id != usuarioDefecto.Id))
            {
                var user = await userManager.FindByEmailAsync(usuarioDefecto.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(usuarioDefecto, "123Pa$$word.");
                    await userManager.AddToRoleAsync(usuarioDefecto, Roles.Cliente.ToString());                    
                    await userManager.AddToRoleAsync(usuarioDefecto, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(usuarioDefecto, Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
