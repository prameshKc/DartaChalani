using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BLL.Abstract;
using BLL.UnitOfWork;
using DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Models;

namespace service {
    public static class SeedingData {

        public static void Seed (this IApplicationBuilder app, IServiceProvider service) {

            var appUser = service.GetRequiredService<UserManager<ApplicationUser>> ();
            var appRole = service.GetRequiredService<RoleManager<ApplicationRole>> ();

            var prefix = service.GetRequiredService<IGenericRepository<Prefix>> ();
            var Subject = service.GetRequiredService<IGenericRepository<Subject>> ();
            var Chalan = service.GetRequiredService<IGenericRepository<Chalan>> ();
            var Dartas = service.GetRequiredService<IGenericRepository<Dartas>> ();
            var uow = service.GetRequiredService<IUOW> ();

            SeedRole (appRole).Wait ();
            SeedUser (appUser).GetAwaiter ().GetResult ();

            SeedPrefix (prefix).Wait ();
            SeedSubject (Subject).Wait ();
            SeedDarta (Dartas).Wait ();
            SeedChalan (Chalan).Wait ();
            uow.CompleteAsync ().Wait ();

        }

        #region seed DefaultUser & Roles
        public static async Task SeedUser (UserManager<ApplicationUser> user) {

            //add Default AdminUser
            if (await user.FindByNameAsync ("admin") == null) {

                var appUser = new ApplicationUser {
                UserName = "admin",
                firstName = "Super",
                lastName = "Admin",
                password = "admin@123",
                Email = "admin@gmail.com",
                Address = "Kathmandu",
                isActive = true,
                Gender = "male",
                activeDate = DateTime.Now
                };

                var result = await user.CreateAsync (appUser, appUser.password);
                if (result.Succeeded) {
                    await user.AddToRoleAsync (appUser, "Admin");
                }

            }
            //add default ManagerUser
            if (await user.FindByNameAsync ("Manager") == null) {
                var appUser = new ApplicationUser {
                UserName = "Manager",
                firstName = "Manager",
                lastName = "User",
                password = "Manager@123",
                Email = "Manager@gmail.com",
                Address = "Kathmandu",
                isActive = true,
                Gender = "male",
                activeDate = DateTime.Now

                };

                var result = await user.CreateAsync (appUser, appUser.password);
                if (result.Succeeded) {
                    await user.AddToRoleAsync (appUser, "Editor");
                }
            }

        }

        public static async Task SeedRole (RoleManager<ApplicationRole> role) {

            if (!await role.RoleExistsAsync ("Admin")) {
                var appRole = new ApplicationRole {
                    Name = "Admin"
                };
                await role.CreateAsync (appRole);
            }

            if (!await role.RoleExistsAsync ("Editor")) {
                var appRole = new ApplicationRole {
                    Name = "Editor"
                };
                await role.CreateAsync (appRole);
            }
        }

        #endregion

        #region  seed Default Prefix
        static async Task SeedPrefix (IGenericRepository<Prefix> prefix) {
            var purjiChalan = await prefix.FilterAsync (p => p.type == "purji");
            var purjiDarta = await prefix.FilterAsync (p => p.type == "purji-darta");
            var fieldChalan = await prefix.FilterAsync (p => p.type == "field-chalani");
            var fieldDarta = await prefix.FilterAsync (p => p.type == "field-darta");
            var halsabikChalan = await prefix.FilterAsync (p => p.type == "halsabik-chalan");
            var halsabikDarta = await prefix.FilterAsync (p => p.type == "halsabik-darta");

            if (!purjiChalan.Any ()) {
                var chalanPrefix = new Prefix {
                    prefix = "CH-",
                    startIndex = 0,
                    type = "purji"

                };

                await prefix.CreateAsync (chalanPrefix);
            }

            if (!purjiDarta.Any ()) {
                var chalanPrefix = new Prefix {
                    prefix = "CD-",
                    startIndex = 0,
                    type = "purji-darta"

                };

                await prefix.CreateAsync (chalanPrefix);
            }
            if (!fieldChalan.Any ()) {
                var chalanPrefix = new Prefix {
                    prefix = "FRC-",
                    startIndex = 0,
                    type = "field-chalani"

                };

                await prefix.CreateAsync (chalanPrefix);
            }
            if (!fieldDarta.Any ()) {
                var chalanPrefix = new Prefix {
                    prefix = "FRD-",
                    startIndex = 0,
                    type = "field-darta"

                };

                await prefix.CreateAsync (chalanPrefix);
            }
            if (!halsabikChalan.Any ()) {
                var chalanPrefix = new Prefix {
                    prefix = "HC-",
                    startIndex = 0,
                    type = "halsabik-chalani"

                };

                await prefix.CreateAsync (chalanPrefix);
            }
            if (!halsabikDarta.Any ()) {
                var chalanPrefix = new Prefix {
                    prefix = "HD-",
                    startIndex = 0,
                    type = "halsabik-darta"

                };

                await prefix.CreateAsync (chalanPrefix);
            }

        }
        #endregion

        static async Task SeedSubject (IGenericRepository<Subject> repo) {

            var defaultSub = await repo.FilterAsync (p => p.Name == "N/A");
            if (!defaultSub.Any ()) {
                var sub = new Subject {
                    Name = "N/A"
                };

                await repo.CreateAsync (sub);

            }
        }

        static async Task SeedDarta (IGenericRepository<Dartas> repo) {

            var defaultSub = await repo.FilterAsync (p => p.Name == "N/A");
            if (!defaultSub.Any ()) {
                var sub = new Dartas {
                    Name = "N/A"
                };

                await repo.CreateAsync (sub);

            }
        }

         static async Task SeedChalan (IGenericRepository<Chalan> repo) {

            var defaultSub = await repo.FilterAsync (p => p.Name == "N/A");
            if (!defaultSub.Any ()) {
                var sub = new Chalan {
                    Name = "N/A"
                };

                await repo.CreateAsync (sub);

            }
        }
    }
}