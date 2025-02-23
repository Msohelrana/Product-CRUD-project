namespace WebApplication1.DTOs
{
    public class ProductReadDto
    {
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
