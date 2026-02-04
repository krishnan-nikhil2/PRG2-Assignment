//==========================================================
// Student Number : S10272956J
// Student Name : Li Yanjie James
// Partner Name : Krishnan Nikhil
//==========================================================
namespace Git_Prg2_Assignment
{
    public class Menu 
    {
        private string menuId;
        private string menuName;
        public List<FoodItem> FoodItem_List { get; set; } = new List<FoodItem>();

        public string MenuId
        {
            get { return menuId; }
            set { menuId = value; }
        }
        public string MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }
        public Menu(string menuid, string menuname)
        {
            MenuId = menuid;
            MenuName = menuname;
        }
        public Menu() { }

        public void AddFoodItem(FoodItem fooditem)
        {
            FoodItem_List.Add(fooditem);
        }
        public bool RemoveFoodItem(FoodItem remove_fooditem)
        {
            bool remove_item = false;
            int count = 0;
            foreach (FoodItem foodItem in FoodItem_List)
            {
                if (foodItem.ItemName == remove_fooditem.ItemName)
                {
                    remove_item = true;
                    break;
                }
                count += 1;
            }

            if (remove_item == true)
            {
                FoodItem_List.RemoveAt(count);
            }

            return remove_item;
        }
        public void DisplayFoodItems()
        {
            foreach (FoodItem foodItem in FoodItem_List)
            {
                Console.WriteLine($"  - {foodItem.ItemName}: {foodItem.ItemDesc} - ${foodItem.ItemPrice.ToString("0.00")}");
            }
        }
        public override string ToString()
        {
            return $"MenuId: {MenuId}, MenuName: {MenuName}, FoodItems_Count: {FoodItem_List.Count}";
        }

    }
}
