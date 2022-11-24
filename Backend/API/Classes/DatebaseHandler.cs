using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace API {
    public class DatabaseHandler {
        public static string GetConnectionString() {
            try {
                // Connects API to database
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "DanielD26.mssql.somee.com";
                builder.UserID = "DanielD26_SQLLogin_1";
                builder.Password = "Jgtcah1P";
                builder.InitialCatalog = "DanielD26";
                return builder.ConnectionString;
            }
            catch(Exception e) {
                throw new Exception("Error in GetConnectionString(): " + e.Message);
            }
        }
    }
}
