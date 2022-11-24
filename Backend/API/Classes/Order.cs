using System;

namespace API
{
    public class Order
    {
        public string CustID { get; set; }
        public string ProductID { get; set; }
        public string OrderDate { get; set; }
        public int Quantity { get; set; }
        public string ShipDate { get; set; }
        public string ShipMode { get; set; }

        public float calcTotal(int q, float price) {
            var total = q * price;
            return total;
        }

        public float gst(int q, float price) {
            return calcTotal(q, price)/10;
        }

    }
}
