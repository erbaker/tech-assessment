namespace CSharp.Models
{
    public class OrderDetail
    {
        public int OrderDetailID {  get; set; }
        public int CustomerId {  get; set; }
        public long OrderID { get; set; }
        public long Quantity { get; set; }
        public bool IsOrderActive {  get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
