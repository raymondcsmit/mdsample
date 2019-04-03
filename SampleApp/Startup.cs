using FluentValidation;
using FluentValidation.AspNetCore;
using Madyan.Data;
using Madyan.Data.Context;
using Madyan.Data.Exception;
using Madyan.Data.Validation;
using Madyan.Repo.Abstract;
using SampleApp.Filters;
using SampleApp.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SampleApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string AllowSPACORSAccess = "AllowSPACORSAccess";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc()
            .AddFluentValidation(fvc =>
                {
                    fvc.RegisterValidatorsFromAssemblyContaining<BasicValidator>();
                    fvc.ImplicitlyValidateChildProperties = true;
                });
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSPACORSAccess,
                builder =>
                {
                    //builder.WithOrigins().AllowAnyOrigin()
                    //                    .AllowAnyHeader()
                    //                    .AllowAnyMethod();
                    builder.WithOrigins("http://localhost:5400")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
            services.AddScoped<ModelValidationFilter>();
            services.AddTransient<IValidator<Basic>, BasicValidator>();
            services.AddTransient<IValidator<NextOfKin>, NextOfKinValidator>();
            services.AddTransient<IValidator<GpDetail>, GpDetailValidator>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => {
                
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;
            services.AddDbContext<DDBDBContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("AppConnection"),
                //b => b.MigrationsAssembly(typeof(Startup).AssemblyQualifiedName)));


            b => b.MigrationsAssembly("SampleApp")));
            //typeof(ClassNameOfStep1).AssemblyQualifiedName
            services.AddDbContext<ExceptionDBContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("ExceptionDBConnection"),
              //b => b.MigrationsAssembly(typeof(Startup).AssemblyQualifiedName)));
            b => b.MigrationsAssembly("SampleApp")));

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddScoped<IDDBDBContext, DDBDBContext>();
            services.AddScoped<IExceptionDBContext, ExceptionDBContext>();
            #region Injecting Repositories
            registerRepositories(services, typeof(DDBDBContext));
            registerRepositories(services, typeof(ExceptionDBContext));
            #endregion Injecting Repositories 
            
            services.AddTransient<AppExceptionMiddleware>();

            
        }

        private void registerRepositories(IServiceCollection services, Type Contexttype)
        {
            var assemblerEntity = Contexttype.GetTypeInfo().Assembly;
            foreach (TypeInfo ti in assemblerEntity.DefinedTypes.Where(type =>
             typeof(IEntityBase).IsAssignableFrom(type.AsType())))
            {
                Type t = ti.AsType();
                if (!t.Equals(typeof(IEntityBase)))
                {
                    var parInterface = (typeof(IEntityBaseRepository<>).MakeGenericType(t));
                    var parClass = (typeof(EntityBaseRepository<,>).MakeGenericType(Contexttype, t));
                    services.AddTransient(parInterface, parClass);
                }

            }
        }





        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var contextDB = serviceScope.ServiceProvider.GetRequiredService<DDBDBContext>();
                contextDB.Database.Migrate();
                var contextException = serviceScope.ServiceProvider.GetRequiredService<ExceptionDBContext>();
                contextException.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseAppExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseCors(AllowSPACORSAccess);
            //app.UseStaticFiles();
            // app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }
    }
}
