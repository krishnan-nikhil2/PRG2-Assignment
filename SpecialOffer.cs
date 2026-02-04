//==========================================================
// Student Number : S10272956J
// Student Name : Li Yanjie James
// Partner Name : Krishnan Nikhil
//==========================================================
namespace Git_Prg2_Assignment
{
    class SpecialOffer : Restaurant
    {
        private string offerCode;
        private string offerDesc;
        private double discount;
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


    }
}
