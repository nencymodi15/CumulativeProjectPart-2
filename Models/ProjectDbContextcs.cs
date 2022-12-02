using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CumulativeProject.Models
{
    public class ProjectDbContext
    {
        private static string User { get { return "root"; } }

        private static string Password { get { return "root"; } }

        private static string Database { get { return "cumalative"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }

        protected static string Connectionstring
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database =" + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }

        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(Connectionstring);
        }
    }
}