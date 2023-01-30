using DependencyInjection.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("mysettings.json",
                       optional: false, //file is not optional
                       reloadOnChange: true);
builder.Services.Configure<MyJson>(builder.Configuration);

//Dependency injection services

IWebHostEnvironment env = builder.Environment;

builder.Services.AddTransient<IRepository>(provider =>
{
    if (env.IsDevelopment())
    {
        var x = provider.GetService<Repository>();
        return x;
    }
    else
    {
        return new ProductionRepository();
    }
});
// builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddTransient<ProductSum>();
builder.Services.AddTransient<Repository>();
builder.Services.AddTransient<IStorage, Storage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
