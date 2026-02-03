using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10272831_PRG2Assignment
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
