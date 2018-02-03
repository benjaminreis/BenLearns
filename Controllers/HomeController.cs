using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BenLearns.Models;

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
            return View(model);
        }


        public ActionResult Submit(BenLearns.ViewModels.HackerRankViewModel model)
        {
            Factory.HomeManager.ComputeMeanMedianMode(ref model);
            return View("HackerRank", model);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
