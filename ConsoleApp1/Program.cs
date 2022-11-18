
using Autofac;
using BL;
using BL.DTOs.Author;
using BL.Services.IServices;
using BL.Services.Services;
using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore;


public class Program
{
    private static void Main(string[] args)
    {
        var containerBuilder = new ContainerBuilder();
        containerBuilder.RegisterModule<InfrastructureInjectorModule>();
        containerBuilder.RegisterModule<BLInjectorModule>();

        var container = containerBuilder.Build();

        var dbcontext = container.Resolve<LibraryappDbContext>();

        dbcontext.Database.EnsureDeleted();
        dbcontext.Database.EnsureCreated();

        var author1 = new Author { BirthDate = DateTime.Now, FirstName = "Štefan", Id = 1, LastName = "Hemingway", MiddleName = "" };
        var author2 = new Author { BirthDate = DateTime.Now, FirstName = "Adrian", Id = 2, LastName = "McKinty", MiddleName = "Alfonz" };

        dbcontext.Author.Add(author1);
        dbcontext.Author.Add(author2);

        var authorService = container.Resolve<IAuthorService>();

        var authorService.Find(1);

        //var res = authorService.GetAuthorByName(new AuthorFilterDto() { FirstName = "Adrian", LastName = "McKinty", MiddleName = "Alfonz" });

        //Console.WriteLine(res.FirstName);


    }
}