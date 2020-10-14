using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commands.Command;
using Commands.User;
using Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSwag;
using Queries.Contact;
using Queries.User;
using Repositories;

namespace WebApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("panel",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
            services.AddControllers();

            // Register the Swagger services
            services.AddSwaggerDocument(c =>
            {

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey
                });
            });

            //Configure JWT Token Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["BearerTokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserCommand, UserCommand>();
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<IContactQuery, ContactQuery>();
            services.AddScoped<IContactCommand, ContactCommand>();

            services.AddScoped<IContactManagerContext, ContactManagerContext>();
            services.AddDbContext<ContactManagerContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ContactManager"));
            });
            services.AddScoped<ContactManagerContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
