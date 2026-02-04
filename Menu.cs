//==========================================================
// Student Number : S10272956J
// Student Name : Li Yanjie James
// Partner Name : Krishnan Nikhil
//==========================================================
namespace Git_Prg2_Assignment
{
    class Menu : Restaurant
    {
        private string menuId;
        private string menuName;
        public List<FoodItem> FoodItems_List { get; set; } = new List<FoodItem>();

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
            FoodItems_List.Add(fooditem);
        }
        public void RemoveFoodItem(FoodItem fooditem)
        {
            FoodItems_List.Remove(fooditem);
        }
        public void DisplayFoodItems()
        {
            foreach (FoodItem item in FoodItems_List)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public override string ToString()
        {
            return $"MenuId: {MenuId}, MenuName: {MenuName}, FoodItems_Count: {FoodItems_List.Count}";
        }

    }
}
