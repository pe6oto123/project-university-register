using project_mvc.Models.DataModels.Location;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Models.DataModels.University
{
	public class Faculty
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? FacultyName { get; set; }

		[Required]
		public int? AddressId { get; set; }
		public virtual Address? Address { get; set; }
	}
}
