namespace Pos.Web.Models
{
    // post items to calculate total
    public class POSRequest
    {
        public List<POSItem> Items { get; set; }
    }

    // create model with fields
    public class POSItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
