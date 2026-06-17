// using Microsoft.AspNetCore.Identity;

// using Microsoft.AspNetCore.Localization;

// using Microsoft.AspNetCore.Mvc;

// using Microsoft.AspNetCore.Mvc.Infrastructure;
// using System.Reflection;
// using Microsoft.AspNetCore.Mvc.Routing;

// using Microsoft.EntityFrameworkCore;

// using Microsoft.Extensions.Options;

// using Core;

// using Core.Filters;

// using Core.MiddleWare;

// using Api;

// using Data.Entities.Identity;

// using Infrastructure;

// using Infrastructure.Context;

// using Infrastructure.Seeder;

// using Service;

// using Serilog;

// using System.Globalization;



// var builder = WebApplication.CreateBuilder(args);



// // Add services to the container.



// builder.Services.AddControllers();



// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// builder.Services.AddEndpointsApiExplorer();



// // builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerGen(options =>
// {
//     // Add custom schema filter to include enum value descriptions
//     options.SchemaFilter<EnumSchemaFilter>();

//     // 1. قراءة ملف الـ XML الخاص بمشروع الـ Api نفسه
//     var apiXmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
//     var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
//     if (File.Exists(apiXmlPath))
//     {
//         options.IncludeXmlComments(apiXmlPath);
//     }

//     // 2. قراءة ملف الـ XML الخاص بمشروع الـ Core (طريقة ديناميكية مضمونة)
//     // نستخدم اسم الكلاس QuestionAnswerDto ليعرف الـ .NET مكان الملف بالضبط
//     var coreAssembly = typeof(Core.Features.AbilitiesTracker.QuestionAnswerDto).Assembly;
//     var coreXmlFile = $"{coreAssembly.GetName().Name}.xml";
//     var coreXmlPath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);

//     if (File.Exists(coreXmlPath))
//     {
//         options.IncludeXmlComments(coreXmlPath);
//     }

//     // 3. قراءة ملف الـ XML الخاص بمشروع الـ Data (للEnums والـ DTOs)
//     var dataAssembly = typeof(Data.Enums.StudentOrderingEnum).Assembly;
//     var dataXmlFile = $"{dataAssembly.GetName().Name}.xml";
//     var dataXmlPath = Path.Combine(AppContext.BaseDirectory, dataXmlFile);

//     if (File.Exists(dataXmlPath))
//     {
//         options.IncludeXmlComments(dataXmlPath);
//     }
// });



// ///////
// builder.Services.AddDbContext<ApplicationDBContext>(option =>
// {
//     // اجعله كدة مؤقتاً للتجربة محلياً ورؤية الملف
//     // إذا كان التطبيق يعمل 
//     //أونلاين على Railway
//     if (builder.Environment.IsProduction())
//     {
//         option.UseSqlite("Data Source=TestDB.db");
//     }
//     else
//     {
//         // محلياً يستمر السيرفر في استخدام SQL Server الخاص بجهازك
//         option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
//     }
// });
// // اجعله كدة مؤقتاً للتجربة محلياً ورؤية الملف
// #region Dependency injections

// builder.Services.AddInfrastructureDependencies()

//                  .AddServiceDependencies()

//                  .AddCoreDependencies()

//                  .AddServiceRegisteration(builder.Configuration);

// #endregion



// #region Localization

// builder.Services.AddControllersWithViews();

// builder.Services.AddLocalization(opt =>

// {

//     opt.ResourcesPath = "";

// });



// builder.Services.Configure<RequestLocalizationOptions>(options =>

// {

//     List<CultureInfo> supportedCultures = new List<CultureInfo>

//     {

//             new CultureInfo("en-US"),

//             new CultureInfo("de-DE"),

//         //    new CultureInfo("fr-FR"),

//          //   new CultureInfo("ar-EG")

//     };



//     options.DefaultRequestCulture = new RequestCulture("en-US");

//     options.SupportedCultures = supportedCultures;

//     options.SupportedUICultures = supportedCultures;

// });



// #endregion



// #region AllowCORS

// var CORS = "_cors";

// builder.Services.AddCors(options =>

// {

//     options.AddPolicy(name: CORS,

//                       policy =>

//                       {

//                           policy.AllowAnyHeader();

//                           policy.AllowAnyMethod();

//                           policy.AllowAnyOrigin();

//                       });

// });



// #endregion



// builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// builder.Services.AddTransient<IUrlHelper>(x =>

// {

//     var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;

//     var factory = x.GetRequiredService<IUrlHelperFactory>();

// #pragma warning disable CS8604 // Possible null reference argument.

//     return factory.GetUrlHelper(actionContext);

// #pragma warning restore CS8604 // Possible null reference argument.

// });

// builder.Services.AddTransient<AuthFilter>();



// //Serilog

// // Log.Logger=new LoggerConfiguration()

// //               .ReadFrom.Configuration(builder.Configuration).CreateLogger();

// // builder.Services.AddSerilog();



// var app = builder.Build();

// // using (var scope = app.Services.CreateScope())

// // {

// //     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

// //     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

// //     await RoleSeeder.SeedAsync(roleManager);

// //     await UserSeeder.SeedAsync(userManager);

// // }





// // Configure the HTTP request pipeline.

// // if (app.Environment.IsDevelopment())

// // {

// //     app.UseSwagger();

// //     app.UseSwaggerUI();

// // }

// app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autism Project API V1");
//     // السطر القادم اختياري: يجعل الـ Swagger يفتح مباشرة بمجرد دخول الرابط الرئيسي بدون كتابة /swagger
//     c.RoutePrefix = string.Empty; 
// });



// #region Localization Middleware

// var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

// #pragma warning disable CS8602 // Dereference of a possibly null reference.

// app.UseRequestLocalization(options.Value);

// #pragma warning restore CS8602 // Dereference of a possibly null reference.

// #endregion



// app.UseMiddleware<ErrorHandlerMiddleware>();



// app.UseHttpsRedirection();

// app.UseCors(CORS);

// app.UseStaticFiles();



// app.UseAuthentication();

// app.UseAuthorization();


// app.MapControllers();


// // في نهاية ملف Program.cs قبل app.Run()
// if (app.Environment.IsProduction())
// {
//     using (var scope = app.Services.CreateScope())
//     {
//         var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        
//         // 1. إنشاء الداتابيز أونلاين في حالة عدم وجودها
//         await dbContext.Database.EnsureCreatedAsync(); 
        
//         // 2. 👇 مناداة دالة الفرش التلقائي للبيانات
//       await Infrastructure.Seeder.ApplicationDataSeeder.SeedDataAsync(dbContext);
//       await Infrastructure.Seeder.ApplicationDataSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
//       await Infrastructure.Seeder.ApplicationDataSeeder.SeedFullCommunityDataAsync(dbContext);
//     }
// }



// var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
// app.Run($"http://0.0.0.0:{port}");







using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Core;
using Core.Filters;
using Core.MiddleWare;
using Api;
using Data.Entities.Identity;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Seeder;
using Service;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Add custom schema filter to include enum value descriptions
    options.SchemaFilter<EnumSchemaFilter>();

    // 1. قراءة ملف الـ XML الخاص بمشروع الـ Api نفسه
    var apiXmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFile);
    if (File.Exists(apiXmlPath))
    {
        options.IncludeXmlComments(apiXmlPath);
    }

    // 2. قراءة ملف الـ XML الخاص بمشروع الـ Core (طريقة ديناميكية مضمونة)
    var coreAssembly = typeof(Core.Features.AbilitiesTracker.QuestionAnswerDto).Assembly;
    var coreXmlFile = $"{coreAssembly.GetName().Name}.xml";
    var coreXmlPath = Path.Combine(AppContext.BaseDirectory, coreXmlFile);

    if (File.Exists(coreXmlPath))
    {
        options.IncludeXmlComments(coreXmlPath);
    }

    // 3. قراءة ملف الـ XML الخاص بمشروع الـ Data (للEnums والـ DTOs)
    var dataAssembly = typeof(Data.Enums.StudentOrderingEnum).Assembly;
    var dataXmlFile = $"{dataAssembly.GetName().Name}.xml";
    var dataXmlPath = Path.Combine(AppContext.BaseDirectory, dataXmlFile);

    if (File.Exists(dataXmlPath))
    {
        options.IncludeXmlComments(dataXmlPath);
    }
});

/////// [تعديل] إعدادات قاعدة البيانات لتدعم SQL Server في التطوير والإنتاج
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    if (builder.Environment.IsProduction())
    {
        // عند التشغيل أونلاين على Railway، يتم قراءة رابط SQL Server من المتغيرات
        option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
    }
    else
    {
        // محلياً يستمر السيرفر في استخدام SQL Server الخاص بجهازك
        option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
    }
});

#region Dependency injections
builder.Services.AddInfrastructureDependencies()
                 .AddServiceDependencies()
                 .AddCoreDependencies()
                 .AddServiceRegisteration(builder.Configuration);
#endregion

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
            new CultureInfo("en-US"),
            new CultureInfo("de-DE"),
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion

#region AllowCORS
var CORS = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});
#endregion

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
#pragma warning disable CS8604 
    return factory.GetUrlHelper(actionContext);
#pragma warning restore CS8604 
});
builder.Services.AddTransient<AuthFilter>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autism Project API V1");
    c.RoutePrefix = string.Empty; 
});

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
#pragma warning disable CS8602 
app.UseRequestLocalization(options.Value);
#pragma warning restore CS8602 
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseCors(CORS);
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// في نهاية ملف Program.cs قبل app.Run()
if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        
        // 1. إنشاء جداول SQL Server أونلاين في حالة عدم وجودها
        await dbContext.Database.EnsureCreatedAsync(); 
        
        // 2. مناداة دالة الفرش التلقائي للبيانات
        await Infrastructure.Seeder.ApplicationDataSeeder.SeedDataAsync(dbContext);
        await Infrastructure.Seeder.ApplicationDataSeeder.SeedAsync(scope.ServiceProvider.GetRequiredService<UserManager<User>>());
        await Infrastructure.Seeder.ApplicationDataSeeder.SeedFullCommunityDataAsync(dbContext);
    }
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
