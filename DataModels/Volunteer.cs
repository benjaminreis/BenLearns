using System;
using System.Data;

namespace DataModels
{
    public class Volunteer
    {

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoleID { get; set; }

        public string Role { get; set; }

        public Boolean Active { get; set; }



        public Volunteer(DataRow dr)
        {
            this.Id = int.Parse((dr["id"] != null ? dr["id"].ToString() : ""));
            this.FirstName = dr["FirstName"] != null ? dr["FirstName"].ToString() : "";
            this.LastName = dr["LastName"] != null ? dr["LastName"].ToString() : "";
            this.Role = dr["Role"] != null ? dr["Role"].ToString() : "None";
            this.RoleID = int.Parse((dr["roleId"] != null ? dr["roleId"].ToString() : "1"));
            this.Active = bool.Parse((dr["Active"] != null ? dr["Active"].ToString() : "1"));

        }


    }




}
