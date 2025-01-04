namespace ECommercePlatform.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // Link the order to a user
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
