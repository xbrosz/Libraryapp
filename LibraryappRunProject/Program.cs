// See https://aka.ms/new-console-template for more information
using DAL.Data;
using Infrastructure.Query.Book;



var context = new LibraryappDbContext();
var bookQueries = new BookQuery(context);
var books = await bookQueries.GetAll();
var book = await bookQueries.GetByID(1);
Console.WriteLine("Hello");
