﻿using System;
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
            model.SelectedProblemId = 0;
            model.sTitle = "Test for coding club";  //TODO BEN we need to set this based upon what they select from the dropdownlist.
            return View(model);
        }


        public ActionResult Submit(BenLearns.ViewModels.HackerRankViewModel model)
        {
            var choice = model.SelectedProblemId;
            model.ProblemTypes = GetProblems();

            switch (choice)
            {
                case 0:
                    model.sResult = "Please Select a problem.";
                    return View("HackerRank", model);
                case 1:
                    Factory.HomeManager.ComputeMeanMedianMode(ref model);
                    return View("HackerRank", model);
                case 2:
                    Factory.HomeManager.ComputeStandardDeviation(ref model);
                    return View("HackerRank", model);

                default: 
                    model.sResult = "Please Select a problem.";
                    return View("HackerRank", model);                    
            }
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<SelectListItem> GetProblems()
        {
            var problems = new List<SelectListItem>();
            problems.Add(new SelectListItem { Value = "0", Text = "Select a problem" });

              problems.Add(new SelectListItem {Value = "1", Text = "Statistics:  Mean, Median Mode"});
            problems.Add(new SelectListItem { Value = "2", Text = "Statistics:  Standard Deviation" });


            return new SelectList(problems, "Value", "Text");
        }

        public ActionResult Volunteers()
        {
            return View(new ViewModels.VolunteerViewModel());
        }

        [HttpPost]
        public ActionResult Volunteers(ViewModels.VolunteerViewModel model)
        {
           // List<BenLearns.ViewModels.VolunteerViewModel> volunteers = new List<ViewModels.VolunteerViewModel>();

            //TODO BEN clean this up
            //var a = new ViewModels.VolunteerViewModel();
            //a.FirstName = "ben";
            //a.LastName = "reis";
            //a.Role = "greeter";
            //volunteers.Add(a);

            model.SearchResults = Factory.VolunteerManager.GetVolunteers(model);

            return View(model);
        }

        [HttpPost]
        public string AddVoluteer(ViewModels.VolunteerViewModel model)
        {

            return "";
            
        }

    }
}
