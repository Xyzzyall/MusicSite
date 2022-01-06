﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using MusicSite.Server.Queries.Releases;
using MusicSite.Shared.SharedModels;


namespace MusicSite.Server.Controllers.Queries
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class ReleasesQueriesController : Controller
    {

    }
}
