namespace FE.Models;

    public class PaginationViewModel
{
    public int LeftBound { get; private set; }
    public int RightBound { get; private set; }
    public int Current { get; private set; }
    public int Last => (_listCount + _pageSize - 1) / _pageSize;

    private readonly int _listCount;
    private readonly int _maxNumberOfPages;
    private readonly int _pageSize;

    public PaginationViewModel(int currentPage, int listCount, int pageSize)
    {
        Current = currentPage;
        _listCount = listCount;
        _maxNumberOfPages = 5;
        _pageSize = pageSize;
        CalculateBounds();
    }

    private void CalculateBounds()
    {
        if (_listCount / _pageSize > _maxNumberOfPages)
        {
            LeftBound = Current - 2;
            RightBound = Current + 2;
        }

        LeftBound = 1;
        RightBound = (_listCount + _pageSize - 1) / _pageSize;
    }
}
