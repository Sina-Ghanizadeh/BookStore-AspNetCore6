using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.CategoryAgg
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The Display Order Range is Invalid.")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
