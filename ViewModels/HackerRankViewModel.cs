using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BenLearns.ViewModels
{
    public class HackerRankViewModel
    {

        public int CountElements { get; set; }
        public string sCountElements { get; set; }
        public List<int> Elements { get; set; }
        public string sElements { get; set; }
        public string sResult { get; set; }


        // Display Attribute will appear in the Html.LabelFor
        [Display(Name = "What to Compute")]
        public int SelectedProblemId { get; set; }
        public IEnumerable<SelectListItem> ProblemTypes { get; set; }
    }


}
