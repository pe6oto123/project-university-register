using project_mvc.Database.Entities.Location;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Database.Entities.University
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        public virtual Address? Address { get; set; }
    }
}
