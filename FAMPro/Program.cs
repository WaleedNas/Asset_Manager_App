using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FAMPro.Data;
using FAMPro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FAMProContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FAMProContext") ?? throw new InvalidOperationException("Connection string 'FAMProContext' not found.")));

builder.Services.AddAuthentication().AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});



builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes("MyCookieAuth")
        .Build();
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAdmin",
        policy => policy.RequireClaim("Department", "IT")
        .RequireClaim("Admin"));
    options.AddPolicy("MustBeEmployee",
        policy => policy.RequireClaim("Department", "IT")
        .RequireClaim("Employee"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapRazorPages();

app.Run();
