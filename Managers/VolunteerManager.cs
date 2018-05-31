﻿using System;
using System.Collections.Generic;
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

             //Factory.VolunteerData.AddVolunteer();  //TODO BEN implement this in its own function
            return Factory.VolunteerDataHelper.GetVolunteers(model.FirstName, model.LastName, model.Role);


        }





    }

}