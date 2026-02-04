//==========================================================
// Student Number : S10272956J
// Student Name : Li Yanjie James
// Partner Name : Krishnan Nikhil
//==========================================================
namespace Git_Prg2_Assignment
{
    public class Restaurant
    {
        private string restaurantId;
        private string restaurantName;
        private string restaurantEmail;
        public Queue<Order> Order_Queue { get; set; } = new Queue<Order>();
        public List<SpecialOffer> SpecialOffer_List { get; set; } = new List<SpecialOffer>();
        public List<Menu> Menu_List { get; set; } = new List<Menu>();

        public Menu menu { get; set; } = new Menu("1", "menu1");




        public string RestaurantId
        {
            get { return restaurantId; }
            set { restaurantId = value; }
        }
        public string RestaurantName
        {
            get { return restaurantName; }
            set { restaurantName = value; }
        }
        public string RestaurantEmail
        {
            get { return restaurantEmail; }
            set { restaurantEmail = value; }
        }
        public Restaurant(string restaurantid, string restaurantname, string restaurantemail)
        {
            RestaurantId = restaurantid;
            RestaurantName = restaurantname;
            RestaurantEmail = restaurantemail;
            Menu_List.Add(menu);
        }
        public Restaurant() { }

        public void DisplayOrders()
        {
            foreach (Order order in Order_Queue)
            {
                Console.WriteLine(order.ToString());
            }
        }
        public void DisplaySpecialOffers()
        {
            foreach (SpecialOffer offer in SpecialOffer_List)
            {
                Console.WriteLine(offer.ToString());
            }
        }
        public void DisplayMenu()
        {
            Console.WriteLine($"Restaurant: {RestaurantName} ({RestaurantId})");
            foreach (Menu menu in Menu_List)
            {
                menu.DisplayFoodItems();
            }
        }
        public void AddMenu(Menu menu)
        {
            Menu_List.Add(menu);
        }
        public bool RemoveMenu(Menu remove_menu)
        {
            bool remove_menu_result = false;
            int count = 0;
            foreach (Menu menu in Menu_List)
            {
                if (menu.MenuId == remove_menu.MenuId)
                {
                    remove_menu_result = true;
                    break;
                }
                count += 1;
            }

            if (remove_menu_result == true)
            {
                Menu_List.RemoveAt(count);
            }

            return remove_menu_result;
        }
        public override string ToString()
        {
            return $"RestaurantId: {RestaurantId}, RestaurantName: {RestaurantName}, RestaurantEmail: {RestaurantEmail}";
        }
    }
}
