using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.Test
{
    public class GenericRepositoryTests
    {
        private readonly LibraryappDbContext dbContext;

        public GenericRepositoryTests()
        {
            var myDatabaseName = "mydatabase_" + DateTime.Now.ToFileTimeUtc();

            var dbContextOptions = new DbContextOptionsBuilder<LibraryappDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .Options;

            dbContext = new LibraryappDbContext(dbContextOptions);

            dbContext.Branch.Add(new Branch
            {
                Id = 1,
                Name = "City Library",
                Address = "Botanická 69"
            });

            dbContext.Book.Add(new Book
            {
                Id = 1,
                BookPrints = new List<BookPrint>
                {
                    new BookPrint
                    {
                        Id = 1,
                        BookId = 1,
                        BranchId = 1
                    },
                    new BookPrint
                    {
                        Id = 2,
                        BookId = 1,
                        BranchId = 1
                    },
                    new BookPrint
                    {
                        Id = 3,
                        BookId = 1,
                        BranchId = 1
                    },

                },
                Ratings = new List<Rating>
                {
                   new Rating
                   {
                       Id = 1,
                       BookId = 1,
                       Comment = "Absolutely awesome",
                       RatingNumber = 5
                   },
                   new Rating
                   {
                       Id = 2,
                       BookId = 1,
                       Comment = "Absolutely awesome",
                       RatingNumber = 5
                   },
                   new Rating
                   {
                       Id = 3,
                       BookId = 1,
                       Comment = "Absolutely awesome",
                       RatingNumber = 5
                   },
                   new Rating
                   {
                       Id = 4,
                       BookId = 1,
                       Comment = "Absolutely awesome",
                       RatingNumber = 5
                   },
                },
                Release = new DateTime(1956, 7, 29),
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                AuthorId = 1
            });
            dbContext.Book.Add(new Book
            {
                Id = 2,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "John",
                    MiddleName = "Ronald Reuel",
                    LastName = "Tolkien",
                    BirthDate = new DateTime(1982, 1, 3)
                },
                BookPrints = new List<BookPrint>
                {
                    new BookPrint
                    {
                        Id = 4,
                        BookId = 1,
                        BranchId = 1
                    },
                },
                Ratings = new List<Rating>
                {
                   new Rating
                   {
                       Id = 5,
                       BookId = 2,
                       Comment = "Absolutely awesome",
                       RatingNumber = 5
                   },
                },
                Release = new DateTime(1956, 11, 11),
                Title = "The Lord of the Rings: The Two Towers",
                AuthorId = 1
            });

            dbContext.Genre.Add(new Genre
            {
                Id = 1,
                Name = "Fantasy"
            });

            dbContext.BookGenre.Add(new BookGenre
            {
                Id = 1,
                BookId = 1,
                GenreId = 1
            });
            dbContext.BookGenre.Add(new BookGenre
            {
                Id = 2,
                BookId = 2,
                GenreId = 1
            });

            dbContext.SaveChanges();


            [Fact]
            void GenericRepoGetBookByIDTest()
            {
                var efRepository = new Repository<Book>(dbContext);
                var book = efRepository.GetByID(1);
                Assert.True(book.Title.Equals("The Lord of the Rings: The Fellowship of the Ring"));
                Assert.True(book.Author.Id == 1);

            }
            [Fact]
            void GenericRepoInsertBookTest()
            {
                var efRepository = new Repository<Book>(dbContext);
                efRepository.Insert(new Book
                {
                    AuthorId = 1,
                    Ratings = new List<Rating>(),
                    Title = "The Lord of the Rings: Return of the King",
                    Release = new DateTime(1955, 10, 20),
                    BookPrints = new List<BookPrint>()
                });
                dbContext.SaveChanges();
                Assert.True(dbContext.Book.Count() == 3);
            }
            [Fact]
            void GenericRepoUpdateBookTest()
            {
                var efRepository = new Repository<Book>(dbContext);
                var bookToUpdate = efRepository.GetByID(1);
                bookToUpdate.Title = "Updated name";
                efRepository.Update(bookToUpdate);
                dbContext.SaveChanges();
                Assert.True(efRepository.GetByID(1).Title.Equals(bookToUpdate.Title));

            }
            [Fact]
            void GenericDeleteBookTest()
            {
                var efRepository = new Repository<Book>(dbContext);
                var bookToDelete = efRepository.GetByID(1);
                efRepository.Delete(bookToDelete);
                dbContext.SaveChanges();
                Assert.True(dbContext.Book.Count() == 1);
                Assert.False(dbContext.Book.Select(b => b.Id).Contains(1));
            }


        }

    }
}
