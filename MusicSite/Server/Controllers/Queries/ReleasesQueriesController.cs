using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using MusicSite.Server.Queries.Releases;


namespace MusicSite.Server.Controllers.Queries
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ReleasesQueriesController : Controller
    {

    }
}
