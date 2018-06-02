using System;
using System.Collections.Generic;
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
            //Code Golf :D
            return Factory.VolunteerDataHelper.GetVolunteers(model.FirstName, model.LastName, model.Role).OrderBy(x=> x.LastName).ToList();


        }





    }

}