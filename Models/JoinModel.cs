namespace CustomerCart.Models
{
    public class JoinModel
    {
        public List<OrderedModel> OrderedItems { get; set; }
        public List<SoldModel> SoldItems { get; set; }
    }
}
