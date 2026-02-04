//==========================================================
// Student Number : S10272956J
// Student Name : Li Yanjie James
// Partner Name : Krishnan Nikhil
//==========================================================
namespace Git_Prg2_Assignment
{
    class Restaurant : Order
    {
        private string restaurantId;
        private string restaurantName;
        private string restaurantEmail;
        public List<Order> orderList { get; set; } = new List<Order>();
        public List<SpecialOffer> specialOfferList { get; set; } = new List<SpecialOffer>();
        public List<Menu> menuList { get; set; } = new List<Menu>();

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
        }
        public Restaurant() { }

        public void DisplayOrders()
        {
            foreach (Order order in orderList)
            {
                Console.WriteLine(order.ToString());
            }
        }
        public void DisplaySpecialOffers()
        {
            foreach (SpecialOffer offer in specialOfferList)
            {
                Console.WriteLine(offer.ToString());
            }
        }
        public void DisplayMenu()
        {
            foreach (Menu item in menuList)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public void AddMenu(Menu menu)
        {
            menuList.Add(menu);
        }
        public void RemoveMenu(Menu menu)
        {
            menuList.Remove(menu);
        }
        public override string ToString()
        {
            return $"RestaurantId: {RestaurantId}, RestaurantName: {RestaurantName}, RestaurantEmail: {RestaurantEmail}";
        }
    }
}
