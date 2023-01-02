using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BL;
using BL.DTOs.Reservation;
using BL.Facades.IFacades;
using BL.Services.IServices;
using BL.Services.Services;
using DAL.Data;
using DAL.Entities;
using FE.Controllers;
using Infrastructure.EFCore;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule<BLInjectorModule>();
    builder.RegisterModule<InfrastructureInjectorModule>();
}
);

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSession(options =>
{
    //sessions hold for 20 minutes
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryappDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(o => o.LoginPath = new PathString("/User/Login"));

builder.Services.AddDbContext<LibraryappDbContext>(options =>
                    options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));


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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())                                                  // Just for Testing !!!!
{
    scope.ServiceProvider.GetRequiredService<LibraryappDbContext>().Database.EnsureDeleted();
    scope.ServiceProvider.GetRequiredService<LibraryappDbContext>().Database.EnsureCreated();

    scope.ServiceProvider.GetRequiredService<IUserService>().Register(new BL.DTOs.User.UserCreateDto()
    {
        Address = "Brno",
        Password = "Heslo_je_123",
        UserName = "Ricko48",
        FirstName = "Richard",
        LastName = "Cernansky",
        PhoneNumber = "+4219873645",
        Email = "lalalala@gmail.com",
        RoleId = 2
    });

    scope.ServiceProvider.GetRequiredService<IUserService>().Register(new BL.DTOs.User.UserCreateDto()
    {
        Address = "Praha",
        Password = "Heslo_je_123",
        UserName = "Admin",
        FirstName = "Peter",
        LastName = "Biely",
        PhoneNumber = "+4219873645",
        Email = "admin@gmail.com",
        RoleId = 1
    });

    var dto = new ReservationCreateFormDto()
    {
        BookId = 1,
        BranchId = 1,
        StartDate = DateTime.Now,
        EndDate = DateTime.Now.AddDays(10),
        UserId = 3
    };

    scope.ServiceProvider.GetRequiredService<IReservationFacade>().ReserveBook(dto);

    var dto2 = new ReservationCreateFormDto()
    {
        BookId = 1,
        BranchId = 1,
        StartDate = DateTime.Now.AddDays(-20),
        EndDate = DateTime.Now.AddDays(-14),
        UserId = 3
    };

    scope.ServiceProvider.GetRequiredService<IReservationFacade>().ReserveBook(dto2);
}

app.Run();

