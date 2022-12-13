using Autofac;
using BL;
using BL.DTOs.Author;
using BL.Facades.IFacades;
using BL.Services.IServices;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;
using Infrastructure.UnitOfWork;

public class Program
{
    private static async Task Main(string[] args)
    {

        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule<InfrastructureInjectorModule>();
        containerBuilder.RegisterModule<BLInjectorModule>();

        var container = containerBuilder.Build();

        using var dbcontext = container.Resolve<LibraryappDbContext>();
        dbcontext.Database.EnsureDeleted();
        dbcontext.Database.EnsureCreated();

        using var uow = container.Resolve<IUnitOfWork>();
        var authorService = container.Resolve<IAuthorService>();

        var userFacade = container.Resolve<IUserFacade>();
        var userService = container.Resolve<IUserService>();




    }
}
