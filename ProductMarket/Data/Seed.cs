using Microsoft.AspNetCore.Identity;
using ProductMarket.Data.Enum;
using ProductMarket.Models;
using System.Diagnostics;

namespace ProductMarket.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            ProductName = "Nvidia Rtx3060Ti",
                            Image = "https://i.imgur.com/3Q5drEy.jpeg",
                            ProductDescription = "This is a graphics card for sale",
                            ProductCategory = ProductCategory.Electronics,
                            ProductPrice = 599.99,
                            Address = new Address()
                            {
                                Street = "123 Washington",
                                City = "Chicago",
                                State = "IL",
                                PostalCode = "60630"
                            }
                         },
                        new Product()
                        {
                            ProductName = "Harry Potter and the Philosopher's Stone",
                            Image = "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg",
                            ProductDescription = "This is first Harry Potter Book for sale",
                            ProductCategory = ProductCategory.Books,
                            ProductPrice = 19.99,
                            Address = new Address()
                            {
                                Street = "666 North Ave",
                                City = "Chicago",
                                State = "IL",
                                PostalCode = "60645"
                            }
                         },
                        new Product()
                        {
                            ProductName = "Patek Philippe Moon Celestial",
                            Image = "https://static.patek.com/images/articles/face_white/350/6102R_001_1.jpg",
                            ProductDescription = "This is a watch for sale",
                            ProductCategory = ProductCategory.Jewelry,
                            ProductPrice = 499999,
                            Address = new Address()
                            {
                                Street = "555 Montrose",
                                City = "Chicago",
                                State = "IL",
                                PostalCode = "60623"
                            }
                         },
                        new Product()
                        {
                            ProductName = "WindShield Wipers 21 inches",
                            Image = "https://m.media-amazon.com/images/I/710Mi6eXd9L.jpg",
                            ProductDescription = "This is a windhshield wipers for sale",
                            ProductCategory = ProductCategory.Automotive,
                            ProductPrice = 25.99,
                            Address = new Address()
                            {
                                Street = "2 Belmont Ave",
                                City = "Chicago",
                                State = "IL",
                                PostalCode = "60565"
                            }
                         }
                    });
                    context.SaveChanges();
                }
                
        
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin123412@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "555 North Ave",
                            City = "Chicago",
                            State = "IL",
                            PostalCode = "12345"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Stack5Has#Over");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "poach1231@yahoo.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "JohnDoe",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "262 Washington",
                            City = "Chicago",
                            State = "IL",
                            PostalCode = "60612"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Suplex^is12");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
                
            }

        }
    }
}

