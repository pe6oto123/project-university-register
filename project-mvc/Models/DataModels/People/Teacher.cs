using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.University;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		public virtual ICollection<TeachersSubjects>? TeachersSubjects { get; set; }
	}

	public class TeachersSubjects
	{
		[Key, Column(Order = 0)]
		public int TeacherId { get; set; }

		[Key, Column(Order = 1)]
		public int SubjectId { get; set; }

		public virtual Teacher? Teacher { get; set; }

		public virtual Subject? Subject { get; set; }
	}
}
