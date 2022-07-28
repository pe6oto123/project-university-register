using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.University;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Models.DataModels.People
{
	public class Teacher
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? FirstName { get; set; }

		[Required]
		[StringLength(50)]
		public string? LastName { get; set; }

		[Required]
		[EmailAddress, StringLength(50)]
		public string? Email { get; set; }

		[Required]
		public int? AddressId { get; set; }
		public virtual Address? Address { get; set; }

		[Required]
		public int? FacultyId { get; set; }
		public virtual Faculty? Faculty { get; set; }

		public virtual ICollection<Subject>? Subjects { get; set; }
	}
}
