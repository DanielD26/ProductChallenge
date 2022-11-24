using System;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace API
{
    public class OrderHandler:DatabaseHandler
    {
        public static List<Order> GetOrders() {
            List<Order> orders = new List<Order>();

            using(SqlConnection conn = new SqlConnection(GetConnectionString())){

                conn.Open();
                // Creates command
                using (SqlCommand command = new SqlCommand("SELECT * FROM [ORDER]", conn)) {

                    using(SqlDataReader reader = command.ExecuteReader()){
                        // we have a reader which has the data in it
                        while(reader.Read()){
                            orders.Add(new Order(){
                                                  OrderDate = reader.GetString(0),
                                                  CustID = reader.GetString(1),
                                                  ProductID = reader.GetString(2),
                                                  Quantity = reader.GetInt32(3),
                                                  ShipDate = reader.GetString(4),
                                                  ShipMode = reader.GetString(5)});
                        }
                    }
                }
                conn.Close();
            }
            return orders;   
        }

        public static string PutOrder(Order order) {
            using(SqlConnection conn = new SqlConnection(GetConnectionString())){

                conn.Open();
                // Creates command
                using (SqlCommand command = new SqlCommand("PutOrder", conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pOrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@pCustID", order.CustID);
                    command.Parameters.AddWithValue("@pProductID", order.ProductID);
                    command.Parameters.AddWithValue("@pQuantity", order.Quantity);
                    command.Parameters.AddWithValue("@pShipDate", order.ShipDate);
                    command.Parameters.AddWithValue("@pShipMode", order.ShipMode);
 
                    command.ExecuteNonQuery();                    
                }
                conn.Close();
            }
            return "";
        }

        public static string PostOrder(Order order) {
            using(SqlConnection conn = new SqlConnection(GetConnectionString())){

                conn.Open();
                // Creates command
                using (SqlCommand command = new SqlCommand("PostOrder", conn))
                {
                    
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@pCustID", order.CustID);
                    command.Parameters.AddWithValue("@pProductID", order.ProductID);
                    command.Parameters.AddWithValue("@pOrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@pQuantity", order.Quantity);
                    command.Parameters.AddWithValue("@pShipDate", order.ShipDate);
                    command.Parameters.AddWithValue("@pShipMode", order.ShipMode);

                    command.ExecuteNonQuery();
                    
                    
                }
                conn.Close();
            }
            return "";
        }

        public static string DeleteOrder(Order order) {
            using(SqlConnection conn = new SqlConnection(GetConnectionString())){

                conn.Open();
            using (SqlCommand command = new SqlCommand("DELETE FROM [ORDER] WHERE OrderDate = @pOrderDate AND ProductID = @pProductID AND CustID = @pCustID", conn))
                {
                    command.Parameters.AddWithValue("@pOrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@pProductID", order.ProductID);
                    command.Parameters.AddWithValue("@pCustID", order.CustID);

                    command.ExecuteNonQuery();
                    
                    
                }
                conn.Close();
            }
            return "";
        }
    }
}
