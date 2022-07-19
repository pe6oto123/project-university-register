using System.ComponentModel.DataAnnotations;

namespace project_mvc.Database.Entities.Location
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? MainAddress { get; set; }

        public virtual City? City { get; set; }
    }
}
