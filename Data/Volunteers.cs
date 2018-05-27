using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace BenLearns.Data
{
    internal class Volunteers
    {
        public Volunteers()
        {
        }


        internal List<BenLearns.ViewModels.VolunteerViewModel> GetVolunteers()
        {
            ConnectToDataBase();

            return null;
        }

        private void ConnectToDataBase()
        {
            //Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
            //Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;

            //https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-connection.html

            string connStr = "server=sql9.freesqldatabase.com;user=sql9240011;database=sql9240011;port=3306;password=TeRNbIVvZP;SslMode=none";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                //string sql = "select V.id, V.FirstName, V.LastName, V.roleId, VR.Role from Volunteers V join VolunteerRoles VR on VR.id = V.RoleId Limit 0,100;";
                string sql = "select * from Volunteers V join VolunteerRoles VR on VR.id = V.RoleId;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                DataSet ds = new DataSet();
                DataTable dataTable = new DataTable();
                ds.Tables.Add(dataTable);
                ds.EnforceConstraints = false;
                dataTable.Load(rdr);

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                rdr.Close();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
        }
    }
}
