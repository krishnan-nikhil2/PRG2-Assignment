//==========================================================
// Student Number : S10272831G
// Student Name : Krishnan Nikhil
// Partner Name : Li Yanjie James
//==========================================================



namespace Git_Prg2_Assignment
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

        public OrderedFoodItem() : base()
        {

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
