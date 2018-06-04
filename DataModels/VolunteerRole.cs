using System;
using System.Data;

namespace DataModels
{
    public class VolunteerRole
    {
        public long Id { get; set; }

        public string Role { get; set; }


        public VolunteerRole()
        {
        }

        public VolunteerRole(DataRow dr)
        {
            this.Id = int.Parse((dr["id"] != null ? dr["id"].ToString() : ""));
            this.Role = dr["Role"] != null ? dr["Role"].ToString() : "None";


        }

    }
}
