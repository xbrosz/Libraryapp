using DAL.Data;
using DAL.Entities;
using Infrastructure.EFCore.Query;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Test
{
    public class EFReservationQueryTests
    {
        private readonly LibraryappDbContext dbContext;

        public EFReservationQueryTests()
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

            dbContext.User.Add(new User
            {
                Id = 1,
                UserName = "xkarel",
                FirstName = "Karel",
                LastName = "Zidrak",
                Address = "Brno",
                Email = "karel@xyz.com",
                Password = "karolko123",
                PhoneNumber = "1234"
            });

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

            dbContext.Add(

                    new BookPrint
                    {
                        Id = 1,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 2,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 3,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 4,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 5,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 6,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 7,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 8,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new BookPrint
                    {
                        Id = 9,
                        BookId = 1,
                        BranchId = 1
                    });

            dbContext.Add(
                    new Reservation
                    {
                        Id = 1,
                        BookPrintId = 1,
                        UserId = 1,
                        StartDate = DateTime.Today.AddDays(5),
                        EndDate = DateTime.Today.AddDays(15)
                    }
                );

            dbContext.Add(
                    new Reservation
                    {
                        Id = 2,
                        BookPrintId = 1,
                        UserId = 1,
                        StartDate = DateTime.Today.AddDays(-20),
                        EndDate = DateTime.Today.AddDays(-10)
                    }
                );

            dbContext.SaveChanges();
        }

        [Fact]
        public void OneReservationExistsBetweenDateRange_QueryWhere_Test()
        {
            var efquery = new EFReservationQuery(dbContext);

            efquery.FromFilter(DateTime.Today.AddDays(-20));
            efquery.ToFilter(DateTime.Today.AddDays(-10));

            var result = efquery.Execute().Items;

            Assert.Single(result);
            Assert.Equal(2, result.First().Id);
        }

        [Fact]
        public void TwoReservationsExistBetweenDateRange_QueryWhere_Test()
        {
            var efquery = new EFReservationQuery(dbContext);

            efquery.FromFilter(DateTime.Today.AddDays(-15));
            efquery.ToFilter(DateTime.Today.AddDays(10));

            var result = efquery.Execute().Items;

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void ZeroReservationsExistBetweenDateRange_QueryWhere_Test()
        {
            var efquery = new EFReservationQuery(dbContext);

            efquery.FromFilter(DateTime.Today.AddDays(16));
            efquery.ToFilter(DateTime.Today.AddDays(20));

            var result = efquery.Execute().Items;

            Assert.Empty(result);
        }
    }
}
