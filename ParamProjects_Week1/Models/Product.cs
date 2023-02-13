using System.ComponentModel.DataAnnotations;

namespace ParamProjects_Week1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name alanı zorunludur")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Stock alanı zorunludur")]
        public int Stock { get; set; }
        [Required]
        [Display(Name = "Price alanı zorunludur")]
        public double Price { get; set; }
    }
}
