using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Abstract;
using BLL.Implementations;
using BLL.UnitOfWork;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models;
using service;
using services.Mapper;

namespace RestApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

                public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();
            services.AddControllers ();

            services.AddDbContext<DartaDbContext> (
                confg => {
                    confg.UseSqlServer (Configuration.GetConnectionString ("AppDb"));
                }
            );

            services.Configure<JwtProperty> (Configuration.GetSection ("jwtProperty"));
            var jwt = Configuration.GetSection ("jwtProperty").Get<JwtProperty> ();

            services.AddIdentity<ApplicationUser, ApplicationRole> (p => {
                p.Password.RequireDigit = false;
                p.Password.RequiredLength = 7;
                p.Password.RequireNonAlphanumeric = false;
                p.Password.RequireUppercase = false;
                p.Password.RequireLowercase = false;

            }).AddEntityFrameworkStores<DartaDbContext> ();

            services.AddAuthentication (p => {
                    p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (p => {
                    var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (jwt.Key));
                    p.SaveToken = true;
                    p.TokenValidationParameters = new TokenValidationParameters () {
                        IssuerSigningKey = key,
                        ValidAudience = jwt.Audience,
                        ValidIssuer = jwt.Issuer,
                        // ValidateIssuer = true,
                        // ValidateAudience=true,
                        RequireExpirationTime = true

                    };
                });

            //cors services activated
            services.AddCors (p => {
                p.AddPolicy ("mainCors", builder =>
                    builder.SetIsOriginAllowed (p => true)
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ());
                //  .AllowCredentials ());
            });

            //dependency For Unit Of Work
            services.AddTransient<IUOW, UOW> ();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

           

          //  Dependency container for automapper
            services.AddAutoMapper (typeof (MapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();

            // app.UseExceptionHandler (error => {
            //     error.Run (async context => {
            //         context.Response.ContentType = "application/json";

            //         await context.Response.WriteAsync (new {
            //             code = context.Response.StatusCode.ToString (),
            //                 MessageReceivedContext = context.Response.StatusCode == 500 ? "insertnal server error" : "Bad request"
            //         }.ToString ());
            //     });
            // });

            app.UseRouting ();
            app.UseCors ("mainCors");
             app.UseAuthentication();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

            //seeding Deault User and ROle
            app.Seed (service);
        }
    }
}