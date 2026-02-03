using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10272831_PRG2Assignment
{
    public class OrderedFoodItem : FoodItem
    {

        private int qtyOrdered;
        private double subTotal;

        public int QtyOrdered
        {

            get { return qtyOrdered; }
            set { qtyOrdered = value; }
        }

        public double SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }


        public OrderedFoodItem(string itemname, string itemdesc, double itemprice, string custom, int qtyordered, double subtotal) : base(itemname,itemdesc,itemprice,custom)
        {
            QtyOrdered = qtyordered;
            SubTotal = subtotal;
        }


        public double CalculateSubTotal()
        {
            SubTotal = QtyOrdered * ItemPrice;
            return SubTotal;
        }


        public override string ToString()
        {
            return base.ToString() + $", QtyOrdered: {QtyOrdered}, SubTotal: {CalculateSubTotal()}";
        }




    }
}
