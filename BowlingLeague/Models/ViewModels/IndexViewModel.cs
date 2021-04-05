using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Models.ViewModels
{
    public class IndexViewModel //Model for the index cshtml page to use to bring the right info into the page from the controller
    {
        public List<Bowlers> Bowlers { get; set; }
        public PageNumInfo PageNumInfo { get; set; }
        public string TeamName { get; set; }
    }
}
