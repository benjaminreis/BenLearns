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


        #region "Internal Methods"

        internal List<DataModels.Volunteer> GetVolunteers(string FirstName, string LastName, string Role)
        {
            return SearchVolunteers(FirstName, LastName, Role);
        }


        internal string DeleteVolunteer(int VolunteerId)
        {
            var sql = $"DELETE FROM Volunteers WHERE ID = ?VolunteerId; ";
            List<MySqlParameter> Parameters = new List<MySqlParameter>();

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.AddWithValue("?VolunteerId", VolunteerId);

            var DataTable = BuildDataTable(cmd);
            //var obj = DataTable.Rows[0]["LAST_INSERT_ID()"];
            //if (obj != null)
            //{
            //    return obj.ToString();
            //}

            //TODO BEN see whats in the DataTabe to see if it has #Rows Affected
            return "TEST";

        }

        internal List<DataModels.VolunteerRole> GetRoles()
        {
            var sql = "SELECT id, Role FROM VolunteerRoles ORDER BY id ASC Limit 0,100;";
            MySqlConnection conn = new MySqlConnection(_sqlConn);
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            DataTable dataTable = BuildDataTable(cmd);

            List<DataModels.VolunteerRole> roles = new List<DataModels.VolunteerRole>();
            foreach (DataRow row in dataTable.Rows)
            {
                roles.Add(new DataModels.VolunteerRole(row));
            }
            return roles;
        }








        internal string AddVolunteer(DataModels.Volunteer volunteer)
        {
            var sql = $"INSERT INTO Volunteers(FirstName, LastName, roleId) VALUES(?firstname, ?lastname, ?roleid); SELECT LAST_INSERT_ID(); ";
            List<MySqlParameter> Parameters = new List<MySqlParameter>();

            MySqlCommand cmd = new MySqlCommand(sql);

            cmd.Parameters.AddWithValue("?firstname", volunteer.FirstName);
            cmd.Parameters.AddWithValue("?lastname", volunteer.LastName);
            cmd.Parameters.AddWithValue("?roleid", volunteer.RoleID);

            var DataTable = BuildDataTable(cmd);
            var obj = DataTable.Rows[0]["LAST_INSERT_ID()"];
            if (obj != null)
            {
                return obj.ToString();
            }

            return "Error";
        }



        internal string AddRole(DataModels.VolunteerRole role)
        {

            var sql = $"INSERT INTO volunteerroles (Role) VALUES ('@role'); SELECT LAST_INSERT_ID()";

            var Cmd = new MySqlCommand(sql);
            Cmd.Parameters.Add("@role", MySqlDbType.VarChar, 100);

            var DataTable = BuildDataTable(Cmd);

            var obj = DataTable.Rows[0]["LAST_INSERT_ID()"];
            if (obj != null)
            {
                return obj.ToString();
            }

            return "Error";
        }

#endregion

        #region "private methods"

        #region "private DB calls"

        private List<DataModels.Volunteer> GetAllVolunteers()
        {

            MySqlConnection conn = new MySqlConnection(_sqlConn);
            conn.Open();

            string sql = "select V.id, V.FirstName, V.LastName, V.Active, V.roleId, VR.Role from Volunteers V join VolunteerRoles VR on VR.id = V.RoleId Limit 0,100;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);


            DataTable dataTable = BuildDataTable(cmd);

            List<DataModels.Volunteer> volunteers = new List<DataModels.Volunteer>();
            foreach (DataRow row in dataTable.Rows)
            {
                volunteers.Add(new DataModels.Volunteer(row));
            }
            conn.Close();

            return volunteers;
        }

        private List<DataModels.Volunteer> SearchVolunteers(string FirstName, string LastName, string Role)
        {
            StringBuilder sql = new StringBuilder("select V.id, V.FirstName, V.LastName, V.roleId, VR.Role, V.Active from Volunteers V join VolunteerRoles VR on VR.id = V.RoleId WHERE 1=1 ");

            List<MySqlParameter> Parameters = new List<MySqlParameter>();
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                sql.Append($"AND V.FirstName = ?firstname ");
                var param = new MySqlParameter("?firstname", MySqlDbType.VarChar, 100);
                param.Value = FirstName;
                Parameters.Add(param);

            }
            if (!string.IsNullOrWhiteSpace(LastName))
            {
                sql.Append($"AND V.LastName = ?lastname ");
                var param = new MySqlParameter("?lastname", MySqlDbType.VarChar, 50);
                param.Value = LastName;
                Parameters.Add(param);

            }
            if (!string.IsNullOrWhiteSpace(Role))
            {
                sql.Append($"AND VR.Role = ?role ");
                var param = new MySqlParameter("?role", MySqlDbType.VarChar, 100);
                param.Value = Role;
                Parameters.Add(param);
            }

            sql.Append($"Limit 0,100;");

            var myCmd = new MySqlCommand(sql.ToString());
            myCmd.Parameters.AddRange(Parameters.ToArray());  //TODO BEN fix this later so i dont have to "ToArray()" it.

            DataTable dataTable = BuildDataTable(myCmd);
            List<DataModels.Volunteer> volunteers = new List<DataModels.Volunteer>();
            foreach (DataRow row in dataTable.Rows)
            {
                volunteers.Add(new DataModels.Volunteer(row));
            }
            return volunteers;
        }


#endregion

        private DataTable BuildDataTableOLD(string sql)
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

        private DataTable BuildDataTable(MySqlCommand cmd)
        {
            MySqlConnection conn = new MySqlConnection(_sqlConn);

            conn.Open();  //TODO BEN add a using statement

            cmd.Connection = conn;
            MySqlDataReader rdr = cmd.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(rdr);
            conn.Close();

            return dataTable;
        }

        #endregion
    }
}
