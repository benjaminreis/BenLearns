using System;
using System.Collections.Generic;
using BenLearns.ViewModels;

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

        //TODO BEN Maybe add searching/filtering based upon names, roles, etc
        internal List<SingleVolunteerViewModel> GetVolunteers()
        {

            var Volunteers = Factory.VolunteerData.GetVolunteers();


            return ParseVolunteers(Volunteers);
        }



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


        private List<SingleVolunteerViewModel> ParseVolunteers(List<DataModels.Volunteer> dbVolunteers)
        {
            List<SingleVolunteerViewModel> Volunteers = new List<SingleVolunteerViewModel>();

            foreach(DataModels.Volunteer Volunteer in dbVolunteers)
            {
                Volunteers.Add(ParseVolunteer(Volunteer));
            }

            return Volunteers;
        }

    }
}
