﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BenLearns.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using models;

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
            var VolunteerViewModel = new ViewModels.VolunteerViewModel();
            VolunteerViewModel.Roles = Factory.VolunteerDataHelper.GetRoles();
            VolunteerViewModel.Roles.Insert(0, new DataModels.VolunteerRole() { Role = "", Id = -1 });

            //ViewBag.Roles = Factory.VolunteerDataHelper.GetAllRolesNames();
            //return View(new ViewModels.VolunteerViewModel());
            return View(VolunteerViewModel);
        }

        [HttpPost]
        public ActionResult Volunteers(ViewModels.VolunteerViewModel model)
        {

            model.Roles = Factory.VolunteerDataHelper.GetRoles();
            //This is super hacky because the POSTing of the dropdownlistFor automatically grabs the 
            //long RoleId = new long();

            model.SearchResults = Factory.VolunteerManager.GetVolunteers(model);

            return View(model);
        }

        [HttpPost]
        public JsonResult AddVolunteer(ViewModels.VolunteerViewModel volunteer)
        {
            List<string> errors = Factory.VolunteerManager.AddVolunteer(volunteer);
            return Json(errors);
            
        }

        [HttpPost]
        public JsonResult AddRole(string role)
        {

            var result = Factory.VolunteerManager.AddRole(role);
            return Json(result.ToString().ToLower());  //javascript wants lowercase boolean true/false values...

        }

        [HttpPost]
        public JsonResult DeleteVolunteer(int VolunteerId)
        {

            var result = Factory.VolunteerManager.DeleteVolunteer(VolunteerId);
            return Json(result);

        }

        [Route("comments")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Comments()
        {


            var _comments = new List<CommentModel>
            {
                new CommentModel
                {
                    Id = 1,
                    Author = "Daniel Lo Nigro",
                    Text = "Hello ReactJS.NET World!"
                },
                new CommentModel
                {
                    Id = 2,
                    Author = "Pete Hunt",
                    Text = "This is one comment"
                },
                new CommentModel
                {
                    Id = 3,
                    Author = "Jordan Walke",
                    Text = "This is *another* comment"
                },
            };
            return Json(_comments);
        }
    }
}
