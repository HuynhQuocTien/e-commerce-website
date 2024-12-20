﻿using e_commerce_website.Data;
using e_commerce_website.Helper;
using e_commerce_website.Hubs;
using e_commerce_website.Services.Interfaces;
using e_commerce_website.Models;
using e_commerce_website.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace e_commerce_website
{
     public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            services.AddCors(
                options => options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyMethod().AllowAnyHeader()
                            .WithOrigins("http://localhost:3000", "http://localhost:8000")
                            .AllowCredentials();
                    }));
            //
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<ShopDbContext>()
                .AddDefaultTokenProviders();
            //
            services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(10);
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
            });


            //
            var emailConfig = Configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
            //
            services.AddTransient<IManageProductService, ManageProductService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IManageCategoryService, ManageCategoryService>();
            services.AddTransient<IManageProviderService, ManageProviderService>();
            services.AddTransient<IEvaluationService, EvaluationService>();
            services.AddTransient<IManageOrderService, ManageOrderService>();
            services.AddTransient<IReplyService, ReplyService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IManageEvaluationService, ManageEvaluationService>();
            services.AddTransient<IManageUserService, ManageUserService>();
            services.AddTransient<IChatService, ChatService>();
            //
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Swagger Online Shop", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Enter 'Bearer {your token}'",
                        Name = "Authorization",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            //
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Authentication failed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("Token validated for: " + context.Principal.Identity.Name);
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var accessToken2 = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken2))
                            {
                                context.Token = accessToken2;
                                // Console.WriteLine("Received token 2: " + accessToken2);

                            } else if (!string.IsNullOrEmpty(accessToken) || (path.StartsWithSegments("/chatHub")))
                            {
                                context.Token = accessToken;
                            }
                            Console.WriteLine("Received token: " + context.Token);
                            return Task.CompletedTask;
                        }


                    };
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // ValidateIssuer = true,
                        // ValidIssuer = issuer,
                        // ValidateAudience = true,
                        // ValidAudience = issuer,
                        // ValidateLifetime = true,
                        // ValidateIssuerSigningKey = true,
                        // ClockSkew = System.TimeSpan.Zero,
                        // IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                        ValidateIssuer = true,
                        ValidIssuer = issuer,  // Ensure this is correct
                        ValidateAudience = true,
                        ValidAudience = issuer,  // Ensure this is correct
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                    };

                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            //app.UseCors(options => options.WithOrigins("https://localhost:3000", "http://localhost:8000").AllowAnyHeader().AllowAnyMethod());
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/MyImages"
            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
                RequestPath = "/MyImages"
            });
            
            app.UseAuthentication();  // Ensure this is before UseAuthorization()
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Online Shop V1");
            });

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatHub");
            });
            
        }
    }
}