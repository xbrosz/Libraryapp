using Autofac;
using BL;
using BL.DTOs;
using BL.DTOs.Author;
using BL.Facades.IFacades;
using BL.QueryObjects.IQueryObject;
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

        //var b = container.Resolve<IBookService>();

        var query = container.Resolve<IQueryObject<BookFilterDto, BookGridDto>>();



        //var query2 = container.Resolve<IQueryObject<AuthorFilterDto, AuthorDto>>();

        //var res1 = query2.ExecuteQuery(new AuthorFilterDto() { FirstName="Adrian" });

        //Console.WriteLine(res1.Items.First().FirstName);

        //Console.WriteLine(query.ExecuteQuery(new BookFilterDto() { });


        //var res = query.ExecuteQuery(new BookFilterDto() {Title = "the" });

        //foreach (var a in query.ExecuteQuery(new BookFilterDto() { list = new List<string>() { "spa" } }).Items)
        //{
        //    Console.WriteLine("Book: " + a.Title);
        //}

    }
}
