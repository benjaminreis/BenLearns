using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MySql.Data.MySqlClient;

namespace BenLearns.Managers

{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    internal class VolunteerManager
    {

        private BenLearns.Factory _Factory { get; set; }
        public BenLearns.Factory Factory
        {
            get
            {
                if (_Factory == null)
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




        internal List<BenLearns.ViewModels.SingleVolunteerViewModel> GetVolunteers(ViewModels.VolunteerViewModel model)
        {

            return Factory.VolunteerDataHelper.GetVolunteers(model.FirstName, model.LastName, model.Role).OrderBy(x=> x.LastName).ToList();

        }


        internal List<string> AddVolunteer(ViewModels.VolunteerViewModel model)
        {
           List<string> errors =  Factory.VolunteerDataHelper.ValidateVolunteer(model);
            if (errors.Count > 0 )
            {
                return errors;
            }
            else 
            {
                List<string> tempErrors = Factory.VolunteerDataHelper.AddVoluteer(model);
                errors.AddRange(tempErrors);   //Visual studio for Mac 2017 was being weird so i had to break this up into multiple lines.
                return errors;
            }

                   
        }

        internal bool AddRole(string role)
        {
            DataModels.VolunteerRole volunteerRole = new DataModels.VolunteerRole();
            volunteerRole.Role = role;
            string value = Factory.VolunteerData.AddRole(volunteerRole);

            long id = new long();

            if (!string.IsNullOrWhiteSpace(value) && (value.IndexOf("error", StringComparison.OrdinalIgnoreCase) >= 0) && (long.TryParse(value, out id)))
            {
                return true;
            }
            

            return false;
        }


        internal string DeleteVolunteer(int VolunteerId)
        {

            var result = Factory.VolunteerData.DeleteVolunteer(VolunteerId);
            return result;
        }

    }

}