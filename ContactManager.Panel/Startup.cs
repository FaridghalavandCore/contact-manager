using System;
using Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactManager.Panel
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
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy("panel",builder=>builder
                .SetIsOriginAllowed(_ =>true)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            //Configure JWT Token Authentication
            //services.AddAuthentication(auth =>
            //{
            //    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(token =>
            //{
            //    token.RequireHttpsMetadata = false;
            //    token.SaveToken = true;
            //    token.TokenValidationParameters = new TokenValidationParameters
            //    {

            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["BearerTokens:Key"])),
            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        RequireExpirationTime = true,
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});



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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            //Add JWToken to all incoming HTTP Request Header
            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseCors("panel");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
