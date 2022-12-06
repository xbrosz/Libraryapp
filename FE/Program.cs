using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BL;
using BL.Services.IServices;
using BL.Services.Services;
using DAL.Data;
using FE.Controllers;
using Infrastructure.EFCore;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services directly with Autofac here. Don't
// call builder.Populate(), that happens in AutofacServiceProviderFactory.
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule<BLInjectorModule>();
    builder.RegisterModule<InfrastructureInjectorModule>();
}
);


var app = builder.Build();

// For testing purposes
//using var dbcontext = app.Services.GetAutofacRoot().Resolve<LibraryappDbContext>();
//dbcontext.Database.EnsureDeleted();
//dbcontext.Database.EnsureCreated();
//using var uow = app.Services.GetAutofacRoot().Resolve<IUnitOfWork>();


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
