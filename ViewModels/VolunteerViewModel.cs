using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BenLearns.ViewModels
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class VolunteerViewModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public int RoleID { get; set; }

        public List<string> Roles { get; set; }

        public List<ViewModels.SingleVolunteerViewModel> SearchResults { get; set; }
    }
}
