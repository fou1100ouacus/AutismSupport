using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Localization;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Infrastructure;

using Microsoft.AspNetCore.Mvc.Routing;

using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Options;

using Core;

using Core.Filters;

using Core.MiddleWare;

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

builder.Services.AddSwaggerGen();





//dotnet ef migrations add InitialIdentity --context ApplicationDBContext --output-dir Data\Migrations

// builder.Services.AddDbContext<ApplicationDBContext>(option =>

// {

//     option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));

// });

///////
builder.Services.AddDbContext<ApplicationDBContext>(option =>
{
    // إذا كان التطبيق يعمل أونلاين على Railway
    if (builder.Environment.IsProduction())
    {
        option.UseSqlite("Data Source=TestDB.db");
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

        //    new CultureInfo("fr-FR"),

         //   new CultureInfo("ar-EG")

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

#pragma warning disable CS8604 // Possible null reference argument.

    return factory.GetUrlHelper(actionContext);

#pragma warning restore CS8604 // Possible null reference argument.

});

builder.Services.AddTransient<AuthFilter>();



//Serilog

// Log.Logger=new LoggerConfiguration()

//               .ReadFrom.Configuration(builder.Configuration).CreateLogger();

// builder.Services.AddSerilog();



var app = builder.Build();

// using (var scope = app.Services.CreateScope())

// {

//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

//     await RoleSeeder.SeedAsync(roleManager);

//     await UserSeeder.SeedAsync(userManager);

// }





// Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment())

// {

//     app.UseSwagger();

//     app.UseSwaggerUI();

// }

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Autism Project API V1");
    // السطر القادم اختياري: يجعل الـ Swagger يفتح مباشرة بمجرد دخول الرابط الرئيسي بدون كتابة /swagger
    c.RoutePrefix = string.Empty; 
});



#region Localization Middleware

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.

app.UseRequestLocalization(options.Value);

#pragma warning restore CS8602 // Dereference of a possibly null reference.

#endregion



app.UseMiddleware<ErrorHandlerMiddleware>();



app.UseHttpsRedirection();

app.UseCors(CORS);

app.UseStaticFiles();



app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();
// امسح أي كود قديم متبقي للـ Migration في آخر الملف، وحط ده مكانه بالظبط:
if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
        
        // الدالة دي بتمسح أي ملف داتابيز قديم متكش أونلاين عشان تبدأ على نظيف
        await dbContext.Database.EnsureDeletedAsync();
        
        // إنشاء الداتابيز فوراً بناءً على الـ Models بدون المرور بالـ Migrations القديمة
        await dbContext.Database.EnsureCreatedAsync(); 
    }
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");