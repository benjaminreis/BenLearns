using System;
using System.Collections.Generic;
using BenLearns.ViewModels;
using System.Runtime.Caching;
using System.Linq;

namespace Helpers
{
    internal class VolunteerDataHelper
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


#region "Internal Methods"


        internal List<SingleVolunteerViewModel> GetVolunteers(string FirstName, string LastName, string Role)
        {

            var Volunteers = Factory.VolunteerData.GetVolunteers(FirstName, LastName, Role);


            return ParseVolunteers(Volunteers);
        }

        internal List<DataModels.VolunteerRole> GetRoles()
        {
            List<DataModels.VolunteerRole> AllRoles = GetAllRoles();

            //AllRoles.Insert(0, new DataModels.VolunteerRole() { Role = "", Id = -1 });
            return AllRoles;

        }

        internal DataModels.VolunteerRole GetRoleByID(long RoleId)
        {

            if (RoleId > 0)
            {
                var AllRoles = GetRoles();

                var tempRole = AllRoles.Where(x => x.Id == RoleId).FirstOrDefault();
                if (tempRole != null)
                {
                    return tempRole;
                }
                else 
                {
                    return new DataModels.VolunteerRole();    
                }
            }
            else
            {
                return new DataModels.VolunteerRole();
            }
        }


        internal List<string> ValidateVolunteer(VolunteerViewModel model)
        {
            List<string> errors = new List<string>();

            if(string.IsNullOrWhiteSpace(model.FirstName))
            {
                errors.Add("First Name Required");

            }
            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                errors.Add("Last Name Required");

            }

            if (string.IsNullOrWhiteSpace(model.Role))
            {
                errors.Add("Role Required");
            }

            return errors;
        }


#endregion

        #region "Private methods"

        private SingleVolunteerViewModel ParseVolunteer(DataModels.Volunteer dbVolunteer)
        {
            SingleVolunteerViewModel volunteer = new SingleVolunteerViewModel
            {
                FirstName = dbVolunteer.FirstName,
                LastName = dbVolunteer.LastName,
                RoleID = dbVolunteer.RoleID,
                Role = dbVolunteer.Role
            };
            return volunteer;
        }

        private List<DataModels.VolunteerRole> GetAllRoles()
        {

            ObjectCache cache = MemoryCache.Default;
            var Roles = cache.Get("AllRoles") as List<DataModels.VolunteerRole>;
            if (Roles != null)
            {
                return Roles;
            }

            Roles = Factory.VolunteerData.GetRoles();
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(360) };  // 6hrs

            cache.Add("AllRoles", Roles, policy);
            return Roles;

        }
        private List<SingleVolunteerViewModel> ParseVolunteers(List<DataModels.Volunteer> dbVolunteers)
        {
            List<SingleVolunteerViewModel> Volunteers = new List<SingleVolunteerViewModel>();

            foreach (DataModels.Volunteer Volunteer in dbVolunteers)
            {
                Volunteers.Add(ParseVolunteer(Volunteer));
            }

            return Volunteers;
        }

        #endregion


    }
}
