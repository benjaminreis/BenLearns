﻿using System;
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

        private string _sqlConn = "server=sql9.freesqldatabase.com;user=sql9240011;database=sql9240011;port=3306;password=TeRNbIVvZP;SslMode=none";

        internal List<DataModels.Volunteer> GetVolunteers(string FirstName, string LastName, string Role)
        {
            var temp = GetAllVolunteerRoles();  //TODO BEN Get rid of this
            //return GetAllVolunteers();
            return SearchVolunteers(FirstName, LastName, Role);
            //return null;
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



        internal void AddVolunteer(DataModels.Volunteer volunteer)
        {
            var sql = $"INSERT INTO Volunteers(FirstName, LastName, roleId) VALUES('{volunteer.FirstName}', '{volunteer.LastName}', {volunteer.RoleID.ToString()})";
            //TODO BEN change this to a stored procedure, and maybe change it so it 
            var temp = BuildDataTable(sql);
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
