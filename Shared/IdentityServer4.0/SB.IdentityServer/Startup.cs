using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServerWithAspNetIdentity.Models;
using IdentityServerWithAspNetIdentity.Repositories;
using QuickstartIdentityServer;
using IdentityServer4.Services;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using SB.IdentityNPocoStores;
using SB.IdentityNPocoStores.Core;
using Microsoft.AspNetCore.Http;
using System;
using IdentityServer4;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.SpaServices.AngularCli;

namespace IdentityServerWithAspNetIdentitySqlite
{
    public class ConfirmEmailDataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public ConfirmEmailDataProtectorTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<ConfirmEmailDataProtectionTokenProviderOptions> options) : base(dataProtectionProvider, options)
        {
        }
    }

    public class ConfirmEmailDataProtectionTokenProviderOptions : DataProtectionTokenProviderOptions { }

    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private const string EmailConfirmationTokenProviderName = "ConfirmEmail";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                //  builder.AddUserSecrets();

            }

            _environment = env;

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "FoodAppserver.pfx"), "", X509KeyStorageFlags.MachineKeySet);


            services.AddMvc();

            services.AddTransient<IProfileService, IdentityWithAdditionalClaimsProfileService>();

            services.AddTransient<ViewRenderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Read email settings
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            services.Configure<SMSoptions>(Configuration.GetSection("SMSTwilio"));
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<IMessageService, FileMessageService>();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // Cookie settings
                //options.Cookies.ApplicationCookie.CookieName = "YouAppCookieName";
                //options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                //options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                //options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";
                //options.Cookies.ApplicationCookie.AccessDeniedPath = "/Account/AccessDenied";
                //options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                //options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                //options.Cookies.ApplicationCookie.AuthenticationScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Confirm Email Token Provider 
                options.Tokens.EmailConfirmationTokenProvider = EmailConfirmationTokenProviderName;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultPhoneProvider;


            });

            services.Configure<ConfirmEmailDataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(7);
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddNPocoStores<ApplicationUser, ApplicationRole>(Configuration["Data:DefaultConnection:ConnectionString"], Configuration["Data:DefaultConnection:ProviderName"])
            .AddIdentityServer()
            .AddDefaultTokenProviders()
            .AddTokenProvider<ConfirmEmailDataProtectorTokenProvider<ApplicationUser>>(EmailConfirmationTokenProviderName);
            
            // .AddIdentityServer();
            //.AddTokenProvider<ConfirmEmailDataProtectorTokenProvider<ApplicationUser>>(EmailConfirmationTokenProviderName);

            //services.AddAuthentication(x =>
            //   {
            //       x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //       x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //   })

            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SBSecretKey")),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //})

            services.AddAuthentication("MyCookieAuthenticationScheme")
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Account/Forbidden/";
                    options.LoginPath = "/Account/Login/";
                })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            }).AddMicrosoftAccount(msOptions=>
            {
                msOptions.ClientId= Configuration["Authentication:Microsoft:ClientId"];
                msOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            })
            ;


            services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("dataEventRecordsAdmin", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", "dataEventRecords.admin");
                });
                options.AddPolicy("admin", policyAdmin =>
                {
                    policyAdmin.RequireClaim("role", "admin");
                });
                options.AddPolicy("dataEventRecordsManager", policyManager =>
                {
                    policyManager.RequireClaim("role", "dataEventRecords.manager");
                });
                options.AddPolicy("manager", policyManager =>
                {
                    policyManager.RequireClaim("role", "manager");
                });
                options.AddPolicy("dataEventRecordsUser", policyUser =>
                {
                    policyUser.RequireClaim("role", "dataEventRecords.user");
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //Configure Cors
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseStaticFiles();

            app.UseIdentityServer();
            //app.UseAuthentication();


            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //   // AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
            //    AutomaticAuthenticate = false,
            //    AutomaticChallenge = false
            //});

            //var CookieScheme = app.ApplicationServices.GetRequiredService<IOptions<IdentityOptions>>().Value.Cookies.ExternalCookieAuthenticationScheme;

            //app.UseFacebookAuthentication(new FacebookOptions()
            //{
            //    SignInScheme = CookieScheme, //IdentityServerConstants.ExternalCookieAuthenticationScheme,
            //    AppId = Configuration["Authentication:Facebook:AppId"],
            //    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            //});

            //app.UseTwitterAuthentication(new TwitterOptions()
            //{
            //    SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
            //    ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"],
            //    ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"]
            //});

            //app.UseGoogleAuthentication(new GoogleOptions
            //{
            //    AuthenticationScheme = "Google",
            //    DisplayName = "Google",
            //    SignInScheme = CookieScheme,

            //    ClientId = Configuration["Authentication:Google:ClientId"],
            //    ClientSecret = Configuration["Authentication:Google:ClientSecret"]
            //});

            //app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions()
            //{
            //    AuthenticationScheme = "Microsoft",
            //    DisplayName = "Microsoft",
            //    SignInScheme = CookieScheme,

            //    ClientId = Configuration["Authentication:Microsoft:ClientId"],
            //    ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"]
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // await CreateRoles(serviceProvider);

           
        }


        private async Task CreateRoles(IServiceProvider serviceProvider)
        {

            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            UserManager<ApplicationUser> UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //IUserStore<ApplicationUser> UserStore = serviceProvider.GetRequiredService<IUserStore<ApplicationUser>>();

            string[] roleNames = { "Admin", "Manager", "Member" };

IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new ApplicationRole(roleName, "TimeSheet"));
                }
            }

            //creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("UserSettings")["UserEmail"],
                Email = Configuration.GetSection("UserSettings")["UserEmail"]
            };

            string UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                await UserManager.UpdateAsync(poweruser);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
