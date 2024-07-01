namespace CustomerCart.Models
{
    public class SoldModel
    {
        public int SoldItemID { get; set; }

        public string ItemName { get; set; }

        public string ItemBrand { get; set; }

        public int QuantitySold { get; set; }

        public int Price { get; set; }

        public DateTime DateSold { get; set; }
    }
}
