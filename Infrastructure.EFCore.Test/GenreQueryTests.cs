using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Infrastructure.EFCore.Test
{
    public class GenreQueryTests
    {
    
        private readonly LibraryappDbContext dbContext;

        public GenreQueryTests()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryappDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;

            dbContext = new LibraryappDbContext(dbContextOptions);

            dbContext.Genre.Add(new Genre { Id = 1, Name = "horror" });
            dbContext.Genre.Add(new Genre { Id = 2, Name = "sci-fi" });

            dbContext.SaveChanges();
        }

        [Fact]
        public void OneHorrorGenreExists_QueryWhere_Test()
        {
            
            var efquery = new EFGenericQuery<Genre>(dbContext);
            efquery.Where<string>(a => a == "horror", "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);

            Assert.True(result.First().Name == "horror");
        
        }

    }
    
}