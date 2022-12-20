using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers;

public class BaseController : Controller
{
    private const string _pageSizeSession = "PageSizeSession";

    protected int PageSize
    {
        get
        {
            var pageSize = HttpContext.Session.GetInt32(_pageSizeSession);
            if (!pageSize.HasValue)
            {
                pageSize = 10;
                HttpContext.Session.SetInt32(_pageSizeSession, pageSize.Value);
            }
            return pageSize.Value;
        }
        set
        {
            HttpContext.Session.SetInt32(_pageSizeSession, value);
        }
    }
}