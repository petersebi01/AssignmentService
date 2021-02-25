using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AssignmentService.Data;
using AssignmentService.Data.Contexts;
using AssignmentService.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace AssignmentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(s => 
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(settings =>
            {
                settings.RequireHttpsMetadata = false;
                settings.SaveToken = true;
                settings.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                };
            });
            
            services.AddAuthorization(polices =>
            {
                polices.AddPolicy("Employee", new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole("Employee").Build());
            });
            
            services.AddDbContext<AssignmentServiceDbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("Connection")));
            
            services.AddScoped<ITaskRepository, SqlTaskRepository>();
            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
            services.AddScoped<IAssignmentRepository, SqlAssignmentRepository>();
            services.AddScoped<IWorkRepository, SqlWorkRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}