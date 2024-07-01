namespace CustomerCart.Models
{
    public class OrderedModel
    {
        public int OrderedItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemBrand { get; set; }

        public int QuantityOrdered { get; set; }

        public int Price { get; set; }

        public DateTime DateOrdered { get; set; }
    }
}
