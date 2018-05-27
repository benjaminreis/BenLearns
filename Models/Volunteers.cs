using System;

namespace Volunteers.DataModels
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Volunteer
    {

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public Boolean Active { get; set; }



    }


}
