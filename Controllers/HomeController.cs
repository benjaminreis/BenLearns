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
            model.CountElements = int.Parse(model.sCountElements);
                
            model.Elements = model.sElements.Split(',').Select(int.Parse).ToList();
            //TODO BEN do stuff

            model.sResult = "Count: " + model.sCountElements + " Elements: " + model.sElements + " All Your Base are Belong to Us";
            return View("HackerRank", model);
        }

        //public ActionResult Submit()
        //{
        //    return View();
        //}
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
