//==========================================================
// Student Number : S10272956J
// Student Name : Li Yanjie James
// Partner Name : Krishnan Nikhil
//==========================================================
namespace Git_Prg2_Assignment
{
    public class SpecialOffer
    {
        private string offerCode;
        private string offerDesc;
        private double discount;

        public List<Order> Order_List { get; set; } = new List<Order>();


        public string OfferCode
        {
            get { return offerCode; }
            set { offerCode = value; }
        }
        public string OfferDesc
        {
            get { return offerDesc; }
            set { offerDesc = value; }
        }
        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        public SpecialOffer(string offercode, string offerdesc, double discount)
        {
            OfferCode = offercode;
            OfferDesc = offerdesc;
            Discount = discount;
        }
        public SpecialOffer() { }


        public override string ToString()
        {
            return $"OfferCode: {OfferCode}, OfferDesc: {OfferDesc}, Discount: {Discount}";
        }







    }
}
