using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using e_commerce_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce_website.Enums;

namespace e_commerce_website.Extension
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    id= 1,
                    generalityName="Quần áo",
                    name = "Áo sơ mi",
                    status = ActionStatus.Display
                },
                new Category
                {
                    id=2,
                    generalityName = "Quần áo",
                    name = "Quần tây",
                    status = ActionStatus.Display
                },
                new Category
                {
                    id = 3,
                    generalityName = "Quần áo",
                    name = "Áo thun",
                    status = ActionStatus.Display
                },
                new Category
                {
                    id = 4,
                    generalityName = "Quần áo",
                    name = "Quần kaki",
                    status = ActionStatus.Display
                }
            );
            //Provider
            modelBuilder.Entity<Provider>().HasData( new Provider { id = 1, name = "Việt Tiến", status= ActionStatus.Display},
                new Provider { id = 2, name = "Cty May Sông Hồng", status =ActionStatus.Display },
                new Provider { id = 3, name = "Cty May Nhà Bè", status = ActionStatus.Display },
                new Provider { id = 4, name = "Cty Giditex", status = ActionStatus.Display },
                new Provider { id = 5, name = "Cty Vinatex", status =ActionStatus.Display }
                );
            //Product
            modelBuilder.Entity<Product>().HasData( 
                new Product
                {
                    id = 1,
                    name = "Áo sơ mi",
                    importPrice = 100000,
                    price = 150000,
                    rating = 5,
                    description = "mô tả sản phẩm 1",
                    status = ActionStatus.Display,
                    color = Color.blue,
                    size = Size.L,
                    categoryId = 1,
                    providerId = 1
                },
             new Product
             {
                 id = 2,
                 name = "Áo sơ mi tay ngắn",
                 importPrice = 80000,
                 price = 120000,
                 rating = 5,
                
                 description = "mô tả sản phẩm 2",
                 status = ActionStatus.Display,
                 color = Color.red,
                 size = Size.X,
                 categoryId = 1,
                 providerId = 2
             },
             new Product
             {
                 id = 3,
                 name = "Quần tây",
                 importPrice = 200000,
                 price = 250000,
                 rating = 5,
                 
                 description = "mô tả sản phẩm 3",
                 status = ActionStatus.Display,
                 color = Color.blue,
                 size = Size.L,
                 categoryId = 2,
                 providerId = 3
             },
             new Product
             {
                 id = 4,
                 name = "Áo thun",
                 importPrice = 50000,
                 price = 75000,
                 rating = 5,

                 description = "mô tả sản phẩm 4",
                 status = ActionStatus.Display,
                 color = Color.black,
                 size = Size.L,
                 categoryId = 3,
                 providerId = 4
             },
             new Product
             {
                 id = 5,
                 name = "Quần kaki",
                 importPrice = 180000,
                 price = 220000,
                 rating = 5,

                 description = "mô tả sản phẩm 5",
                 status = ActionStatus.Display,
                 color = Color.gray,
                 size = Size.L,
                 categoryId = 4,
                 providerId = 5
             }
                );
            // user - role
            var adminId = new Guid("4557893F-1F56-4B6F-BB3B-CAEFD62C8C49");
            var roleId = new Guid("078269D8-1A12-4592-B92E-7FF1A876A5F2");
            
            modelBuilder.Entity<AppRole>().HasData(new AppRole {
                    Id= roleId,
                    Name="Admin",
                    NormalizedName="Admin",
                    Description="Administrator role",
                },
                new AppRole
                {
                    Id = new Guid("6D9186BA-2CD6-4B6C-B729-4E605DE1019F"),
                    Name = "User",
                    NormalizedName = "User",
                    Description = "User role",
                }
            );

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                displayname = "Admin",
                Email = "huynhquoctien01062003@gmail.com",
                NormalizedEmail = "HUYNHQUOCTIEN01062003@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "1234567890"),
                
                status = ActionStatus.Display,
                SecurityStamp = string.Empty,
                birthDay = new DateTime(1998, 02, 02),
                address = "29 Street No. 8, Linh Tay Ward, Thu Duc City"
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });


        }
    }
}
