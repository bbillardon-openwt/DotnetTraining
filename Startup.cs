using AppBackendTraining.Clients.Pms;
using AppBackendTraining.Entities;
using AppBackendTraining.Helpers;
using AppBackendTraining.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace AppBackendTraining
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
            services.AddDbContext<PersistenceContext>(opt => opt.UseInMemoryDatabase("NotifDB"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AppBackendTraining", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                      },
                        new List<string>()
                    }
                });
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPmsService, FakePmsService>();
            services.AddScoped<IAuthService, AuthenticationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppBackendTraining v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            ;

            PersistenceContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<PersistenceContext>();
            AddTestData(context);
        }


        private static void AddTestData(PersistenceContext context)
        {
            var testNotif1 = new NotificationEntity
            {
                Id = "jkasf-asfhasf-ahuiasf-asfjk",
                AccountId = 1L,
                Name = "test notif 1",
                Content = "Some content"
                
            };

            context.NotificationEntities.Add(testNotif1);


            var testNotif2 = new NotificationEntity
            {
                Id = "dfhfgjdkh-aagsf-aasfdAsdjh-adsda",
                AccountId = 1L,
                Name = "test notif 2",
                Content = "Some other content"

            };

            context.NotificationEntities.Add(testNotif2);

            context.SaveChanges();
        }
    }

}
