using Microsoft.AspNetCore.Mvc;

namespace MusicSite.Server.Controllers.Queries
{
    public class ArticlesQueriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
