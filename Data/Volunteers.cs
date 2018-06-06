using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using BenLearns.Models;
using System.Text;

namespace BenLearns.Data
{
    internal class Volunteers
    {
        public Volunteers()
        {
        }

        private string _sqlConn = "server=den1.mysql2.gear.host;user=benlearns;database=benlearns;port=3306;password=testdb123@;SslMode=none";

        internal List<DataModels.Volunteer> GetVolunteers(string FirstName, string LastName, string Role)
        {
            var temp = GetAllVolunteerRoles();  //TODO BEN Get rid of this
            //return GetAllVolunteers();
            return SearchVolunteers(FirstName, LastName, Role);
            //return null;
        }

        internal void AddVolunteer(string FirstName, string LastName, int RoleID)
        {
            
        }


        internal List<DataModels.VolunteerRole> GetRoles()
        {
            var sql = "SELECT id, Role FROM VolunteerRoles ORDER BY id ASC Limit 0,100;";
            DataTable dataTable = BuildDataTable(sql);

            List<DataModels.VolunteerRole> roles = new List<DataModels.VolunteerRole>();
            foreach (DataRow row in dataTable.Rows)
            {
                roles.Add(new DataModels.VolunteerRole(row));
            }
            return roles;
        }


        private List<DataModels.Volunteer> SearchVolunteers(string FirstName, string LastName, string Role)
        {
            StringBuilder sql = new StringBuilder("select V.id, V.FirstName, V.LastName, V.roleId, VR.Role, V.Active from Volunteers V join VolunteerRoles VR on VR.id = V.RoleId WHERE 1=1 ");
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                sql.Append($"AND V.FirstName = '{FirstName}' ");
            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                sql.Append($"AND V.lastName = '{LastName}' ");
            }
            if (!string.IsNullOrWhiteSpace(Role))
            {
                sql.Append($"AND VR.Role = '{Role}' ");
            }

            sql.Append($"Limit 0,100;");

            DataTable dataTable = BuildDataTable(sql.ToString());
            List<DataModels.Volunteer> volunteers = new List<DataModels.Volunteer>();
            foreach (DataRow row in dataTable.Rows)
            {
                volunteers.Add(new DataModels.Volunteer(row));
            }
            return volunteers;
        }

        private List<DataModels.Volunteer> GetAllVolunteers()
        {

            MySqlConnection conn = new MySqlConnection(_sqlConn);
            conn.Open();

            string sql = "select V.id, V.FirstName, V.LastName, V.Active, V.roleId, VR.Role from Volunteers V join VolunteerRoles VR on VR.id = V.RoleId Limit 0,100;";
            DataTable dataTable = BuildDataTable(sql);

            //List<DataModels.Volunteer> volunteers = dataTable.AsEnumerable().select(dr => DataModels.Volunteer(dr));
            List<DataModels.Volunteer> volunteers = new List<DataModels.Volunteer>();
            foreach(DataRow row in dataTable.Rows)
            {
                volunteers.Add(new DataModels.Volunteer(row));
            }
            conn.Close();

            return volunteers;
        }



        internal List<DataModels.VolunteerRole> GetAllVolunteerRoles()
        {
            string sql = "select Id, Role from VolunteerRoles order by id asc Limit 0,100";

            List<DataModels.VolunteerRole> VolunteerRoles = new List<DataModels.VolunteerRole>();

            DataTable dataTable = BuildDataTable(sql);
            foreach(DataRow row in dataTable.Rows)
            {
                VolunteerRoles.Add(new DataModels.VolunteerRole(row));
            }

            return VolunteerRoles;
        }



        internal string AddVolunteer(DataModels.Volunteer volunteer)
        {
            var sql = $"INSERT INTO Volunteers(FirstName, LastName, roleId) VALUES('{volunteer.FirstName}', '{volunteer.LastName}', {volunteer.RoleID.ToString()}); SELECT LAST_INSERT_ID(); ";
            //TODO BEN change this to a stored procedure, and maybe change it so it 
            var DataTable = BuildDataTable(sql);
            var obj = DataTable.Rows[0]["LAST_INSERT_ID()"];
            if(obj != null)
            {
                return obj.ToString();
            }

            return "Error";
        }

        internal string AddRole(DataModels.VolunteerRole role)
        {
            var sql = $"INSERT INTO volunteerroles (Role) VALUES ('{role.Role}'); SELECT LAST_INSERT_ID()"; 
            var DataTable = BuildDataTable(sql);
            var obj = DataTable.Rows[0]["LAST_INSERT_ID()"];
            if (obj != null)
            {
                return obj.ToString();
            }

            return "Error";
        }



        private DataTable BuildDataTable(string sql)
        {
            MySqlConnection conn = new MySqlConnection(_sqlConn);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(rdr);
            conn.Close();

            return dataTable;
        }
    }
}
