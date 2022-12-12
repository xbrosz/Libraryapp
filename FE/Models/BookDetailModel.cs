

using BL.Facades.Facades;

namespace FE.Models
{
    public class BookDetailModel
    {
        private readonly BookFacade _bookFacade;

        public BookDetailModel(BookFacade bookFacade)
        {
            _bookFacade = bookFacade;
        }
    }
}
