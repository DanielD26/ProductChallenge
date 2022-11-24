using System;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace API
{
    public class ProductHandler:DatabaseHandler
    {
        public static List<Product> GetProducts() {
            List<Product> products = new List<Product>();

            using(SqlConnection conn = new SqlConnection(GetConnectionString())){

                conn.Open();
                // Creates command
                using (SqlCommand command = new SqlCommand("SELECT * FROM PRODUCT", conn)) {

                    using(SqlDataReader reader = command.ExecuteReader()){
                        // we have a reader which has the data in it
                        while(reader.Read()){
                            products.Add(new Product(){ProductID = reader.GetString(0),
                                                  CatID = reader.GetInt32(1),
                                                  Description = reader.GetString(2),
                                                  UnitPrice = reader.GetInt32(3)});
                        }
                    }
                }
                conn.Close();
            }
            return products;   
        }   
    }
}
