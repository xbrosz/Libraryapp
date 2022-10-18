using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Test
{
    public class GenericQueryTests
    {
        private readonly LibraryappDbContext dbContext;

        public GenericQueryTests()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryappDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;

            dbContext = new LibraryappDbContext(dbContextOptions);

            dbContext.Genre.Add(new Genre { Id = 1, Name = "horror" });
            dbContext.Genre.Add(new Genre { Id = 2, Name = "sci-fi" });

            dbContext.Branch.Add(new Branch { Id = 1, Name = "Branch1", Address = "Havlickova 22" });
            dbContext.Branch.Add(new Branch { Id = 2, Name = "Branch2", Address = "Vaclavova 42" });

            dbContext.Author.Add(new Author
            {
                Id = 1,
                FirstName = "Mike",
                LastName = "Wazowski",
                MiddleName = "",
                BirthDate = DateTime.Today
            });

            dbContext.Author.Add(new Author
            {
                Id = 2,
                FirstName = "Krista",
                LastName = "Bendova",
                MiddleName = "",
                BirthDate = DateTime.Today.AddDays(-100)
            });

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

        [Fact]
        public void OneBranchExistsWNameBranch1_QueryWhere_Test()
        {
            var efquery = new EFGenericQuery<Branch>(dbContext);
            efquery.Where<string>(a => a == "Branch1", "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);

            Assert.True(result.First().Name == "Branch1");
        }

        [Fact]
        public void BranchesNameStartsWithBra_QueryWhere_Test()
        {
            var efquery = new EFGenericQuery<Branch>(dbContext);
            efquery.Where<string>(a => a.StartsWith("Bra"), "Name");
            var result = efquery.Execute();

            Assert.True(result.Count() == 2);
        }

        [Fact]
        public void OneAuthorExistsWNameMike_QueryWhere_Test()
        {
            var efquery = new EFGenericQuery<Author>(dbContext);
            efquery.Where<string>(a => a == "Mike", "FirstName");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);

            Assert.True(result.First().FirstName == "Mike");
        }

        [Fact]
        public void OneAuthorExistsWBirthDate_QueryWhere_Test()
        {
            var efquery = new EFGenericQuery<Author>(dbContext);
            efquery.Where<DateTime>(a => a < DateTime.Today, "BirthDate");
            var result = efquery.Execute();

            Assert.True(result.Count() == 1);
        }
    }

}