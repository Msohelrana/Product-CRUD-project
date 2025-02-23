using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.DTOs
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage ="Product Name required!")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Product name must contain at least 2 charachter and atmost 50 character!")]
        public string ProductName { get; set; } = string.Empty;
        [StringLength(500,ErrorMessage ="Description can not exit 500 character!")]
        public string ProductDescription { get; set; } = string.Empty;
    }
}
