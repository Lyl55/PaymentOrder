using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.Core.Factories;
using PaymentOrder.WebCore.IdentityServer;
using PaymentOrder.WebCore.Services.Contracts;
using PaymentOrder.WebCore.Services.Implementations;

namespace PaymentOrder.WebAdminPanel
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
            services.AddRazorPages();

            services.AddSingleton((t) =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                return DbFactory.Create(connectionString);
            });

            services.AddSingleton<IPasswordHasher<User>, CustomPasswordHasher>();
            services.AddIdentity<User, Role>();

            services.AddHttpContextAccessor();
            services.AddScoped<IBankService, BankService>();

            services.AddSingleton<IUserStore<User>, UserStore>();
            services.AddSingleton<IRoleStore<Role>, RoleStore>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
      
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
           
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Bank}/{action=Index}/{id?}");
            });
        }
    }
}
