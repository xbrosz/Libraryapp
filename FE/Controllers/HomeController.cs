﻿using BL.DTOs;
using BL.Facades.IFacades;
using DAL.Entities;
using FE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FE.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IBookFacade _bookFacade;

        public IEnumerable<BookGridDto> Books { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? BookGenre { get; set; }

        public HomeController(ILogger<HomeController> logger, IBookFacade bookFacade)
        {
            _logger = logger;
            _bookFacade = bookFacade;
        }

        public IActionResult Index(int page = 1)
        {
            var books = _bookFacade.GetAllBooksSortedByRating(page, PageSize);

            var model = new BookListViewModel()
            {
                Books = books,
                Pagination = new PaginationViewModel(page, books.Count(), PageSize)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
        //public async Task OnGetAsync()
        //{

            //if (string.IsNullOrWhiteSpace(SearchString))
            //{
            //Books = _bookFacade.GetAllBooksSortedByRating();
                //return;
            //}

            //Books = _bookFacade.GetBooksBySubstring(SearchString);
        //}
    }
}
