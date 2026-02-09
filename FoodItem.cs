//==========================================================
// Student Number : S10272831G
// Student Name : Krishnan Nikhil
// Partner Name : Li Yanjie James
//==========================================================
namespace Git_Prg2_Assignment
{
    public class FoodItem
    {

        private string itemName;
        private string itemDesc;
        private double itemPrice;
        private string customise;

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string ItemDesc
        {
            get { return itemDesc; }
            set { itemDesc = value; }
        }

        public double ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        public string Customise
        {
            get { return customise; }
            set { customise = value; }
        }

        public FoodItem()
        {

        }

        public FoodItem(string itemname, string itemdesc, double itemprice, string custom)
        {
            ItemName = itemname;
            ItemDesc = itemdesc;
            ItemPrice = itemprice;
            Customise = custom;
        }




        public override string ToString()
        {
            return $"ItemName: {ItemName}, ItemDesc: {ItemDesc}, ItemPrice: {ItemPrice}, Customise: {Customise}";
        }



    }
}
