using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Build.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared;
using System.Globalization;
using System.Reflection;
using DataAccess;
using Vexed.Models;
using Vexed.Repositories;
using Vexed.Repositories.Abstractions;
using Vexed.Services;
using Vexed.Services.Abstractions;
using Abstractions.Repositories;
using Abstractions.Services;
using AppLogic;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("VexedDbContextConnection") ?? throw new InvalidOperationException("Connection string 'VexedDbContextConnection' not found.");

builder.Services.AddDbContext<VexedDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<VexedDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

#region Services and Repositories
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddScoped<IContactInfoRepository, ContactInfoRepository>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();

builder.Services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
builder.Services.AddScoped<IEmergencyContactService, EmergencyContactService>();

builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();

builder.Services.AddScoped<IQualificationRepository, QualificationRepository>();
builder.Services.AddScoped<IQualificationService, QualificationService>();

builder.Services.AddScoped<ISalaryRepository, SalaryRepository>();
builder.Services.AddScoped<ISalaryService, SalaryService>();

builder.Services.AddScoped<ITimeCardRepository, TimeCardRepository>();
builder.Services.AddScoped<ITimeCardService, TimeCardService>();

builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IToDoService, ToDoService>();

builder.Services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
builder.Services.AddScoped<IUserDetailsService, UserDetailsService>();

builder.Services.AddScoped<IUserEmploymentRepository, UserEmploymentRepository>();
builder.Services.AddScoped<IUserEmploymentService, UserEmploymentService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPdfService, PdfService>();
#endregion

#region Language setup
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-GB"),
            new CultureInfo("ro-RO"),
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "en-GB", uiCulture: "en-GB");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;

        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });

#endregion

// Logger
string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Shared", "logs");
Logger logger = new Logger(logFilePath);

// Register the logger with the dependency injection container
builder.Services.AddSingleton(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//Language setup
var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
