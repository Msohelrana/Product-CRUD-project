namespace WebApplication1.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
