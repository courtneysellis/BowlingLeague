using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(int? teamid, string team, int pageNum = 0)
        {
            int pageSize = 5;

            return View(new IndexViewModel
            {
                //filters the bowlers by team and how many to show for pagination
                Bowlers = (context.Bowlers.
                    Where(b => b.TeamId == teamid || teamid == null).
                    OrderBy(b => b.BowlerLastName).
                    Skip((pageNum - 1) * pageSize).
                    Take(pageSize).
                    ToList()),


                PageNumInfo = new PageNumInfo
                {
                    //This info helps the PageNumInfo class which helps the tag helper
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                                        context.Bowlers.Where(b => b.TeamId == teamid).Count())
                },

                TeamName = team
            });
                
                
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
    }
}
