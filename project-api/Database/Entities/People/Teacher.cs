using project_api.Database.Entities.Location;
using project_api.Database.Entities.University;
using System.ComponentModel.DataAnnotations;

namespace project_api.Database.Entities.People
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
		public virtual Address? Address { get; set; }

		[Required]
		public virtual Faculty? Faculty { get; set; }

		public virtual ICollection<Subject>? Subjects { get; set; }
	}
}
