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

        foreach(var a in uow.UserRepository.GetAll())
        {
            Console.WriteLine(a.FirstName+ " " + a.LastName + " has role " + a.Role.Name);
        }

        Console.WriteLine("\n");

        userFacade.Register(new BL.DTOs.User.UserCreateDto() { UserName = "Ricko48", FirstName = "Richard", LastName = "Ondrejka", Password = "1234456", Address = "Brno", Email = "dgdf@gfg.com", PhoneNumber = "0987654", RoleId = 1 });


        var name = userService.GetUserByUserName("Ricko48").UserName;


        if (name == null)
        {
            Console.WriteLine("NULL");
        }
        else
        {
            Console.WriteLine(name);

        }


    }
}
