using Autofac;
using BL;
using BL.DTOs.Author;
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


        var authorDto = new AuthorDto() { Id = 15, FirstName = "Richard", MiddleName = "Ondrejka", LastName = "Ondrejka", BirthDate = DateTime.Now };

        Console.WriteLine("Pred: ");
        foreach (var f in uow.AuthorRepository.GetAll())
        {
            Console.WriteLine(f.FirstName);
        }

        Console.WriteLine("\n");

        var author1 = new AuthorDto() { FirstName = "Richard", MiddleName = "Ondrejka", LastName = "Ondrejka", BirthDate = DateTime.Now };
        var author2 = new AuthorDto() { FirstName = "Rich", MiddleName = "Ondrejka", LastName = "Ondrejka", BirthDate = DateTime.Now };
        var author3 = new AuthorDto() { FirstName = "Pietro", MiddleName = "Ondrejka", LastName = "Ondrejka", BirthDate = DateTime.Now };
        var author4 = new AuthorDto() { FirstName = "Marek", MiddleName = "Ondrejka", LastName = "Ondrejka", BirthDate = DateTime.Now };
        var author5 = new AuthorDto() { FirstName = "Hasek", MiddleName = "Ondrejka", LastName = "Ondrejka", BirthDate = DateTime.Now };
        var author6 = new AuthorDto() { FirstName = "Richard", MiddleName = "Ondrejka", LastName = "", BirthDate = DateTime.Now };


        authorService.Insert(author1);
        authorService.Insert(author2);
        authorService.Insert(author3);
        authorService.Insert(author4);
        authorService.Insert(author5);
        authorService.Insert(author6);

        Console.WriteLine("Po: ");
        foreach (var f in uow.AuthorRepository.GetAll())
        {
            Console.WriteLine(f.FirstName);
        }

        Console.WriteLine("\n");

        var res = authorService.GetAuthorsByName("Richard", "Ond", "");

        Console.WriteLine("Service: ");
        foreach (var author in res)
        {
            Console.WriteLine(author.FirstName);
        }
    }
}
