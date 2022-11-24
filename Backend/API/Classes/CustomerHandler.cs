using System;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace API
{
    public class CustomerHandler:DatabaseHandler
    {
        public static List<Customer> GetCustomers() {
            List<Customer> customers = new List<Customer>();

            using(SqlConnection conn = new SqlConnection(GetConnectionString())){

                conn.Open();
                // Creates command
                using (SqlCommand command = new SqlCommand("SELECT * FROM CUSTOMER", conn)) {

                    using(SqlDataReader reader = command.ExecuteReader()){
                        // we have a reader which has the data in it
                        while(reader.Read()){
                            customers.Add(new Customer(){CustID = reader.GetString(0),
                                                  FullName = reader.GetString(1),
                                                  SegID = reader.GetInt32(2),
                                                  Country = reader.GetString(3),
                                                  City = reader.GetString(4),
                                                  State = reader.GetString(5),
                                                  PostCode = reader.GetInt32(6),
                                                  Region = reader.GetString(7)});
                        }
                    }
                }
                conn.Close();
            }
            return customers;   
        }   
    }
}
