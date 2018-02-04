using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BenLearns.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BenLearns.Controllers
{
    public class HomeController : Controller
    {

        private BenLearns.Factory _Factory { get; set; }
        public BenLearns.Factory Factory
        {
            get
            {
                if(_Factory == null)
                {
                    _Factory = new BenLearns.Factory();
                }
                return _Factory;
            }
            set
            {
                _Factory = value;
            }
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult  LeetCode()
        {
            return View();
        }

        public ActionResult HackerRank()
        {
            var model = new ViewModels.HackerRankViewModel();
            model.ProblemTypes = GetProblems();
            return View(model);
        }


        public ActionResult Submit(BenLearns.ViewModels.HackerRankViewModel model)
        {
            var temp = model.SelectedProblemId;
            Factory.HomeManager.ComputeMeanMedianMode(ref model);
            return View("HackerRank", model);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<SelectListItem> GetProblems()
        {
            var problems = new List<SelectListItem>();
              problems.Add(new SelectListItem {Value = "0", Text = "Statistics:  Mean, Median Mode"});
            problems.Add(new SelectListItem { Value = "1", Text = "Statistics:  Standard Deviation" });


            return new SelectList(problems, "Value", "Text");
        }
    }
}
