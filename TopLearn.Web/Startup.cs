using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TopLearn.core.Convertor;
using TopLearn.core.Services.Classes;
using TopLearn.core.Services.Intefaces;
using TopLearn.Datalayar.Context;

namespace TopLearn.Web
{

    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // For send Large File

            //services.Configure<FormOptions>(options =>
            //{
            //    options.MultipartBodyLengthLimit = 600000;
            //});

            services.AddRazorPages();
                            
            #region DataBase Context

            services.AddDbContext<TopLearnContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TopLearnConnection"));
            });

            #endregion

            #region Autentication

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
            });

            #endregion

            #region IoC

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IViewRenderService, RenderViewToString>();
            services.AddTransient<IPermisionService, PermisionService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IOrderService, OrderService>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (contex, next) =>
            {
                if (contex.Request.Path.Value.ToString().ToLower().StartsWith("/course/coursefileonline"))
                {
                    string callingUrl = contex.Request.Headers["Referer"].ToString();

                    if (callingUrl !="" && (callingUrl.StartsWith("https://localhost:44338/") || callingUrl.StartsWith("http://localhost:44338/")))
                    {
                      await  next.Invoke();
                    }
                    else
                    {
                        contex.Response.Redirect("/Login");
                    }
                }
                else
                {
                    await next.Invoke();
                }

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseStaticFiles();


            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(
                    name: "areas",
                 pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
             );

                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
