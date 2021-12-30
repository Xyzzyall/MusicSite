using Microsoft.AspNetCore.Mvc;

namespace MusicSite.Server.Controllers.Queries
{
    public class ReleasesQueriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
