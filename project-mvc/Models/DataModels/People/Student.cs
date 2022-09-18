using project_mvc.Models.DataModels.Location;
using project_mvc.Models.DataModels.University;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_mvc.Models.DataModels.People
{
	public class Student
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
		public int? GenderId { get; set; }
		public virtual Gender? Gender { get; set; }

		[Required]
		[StringLength(12)]
		public string? FacultyNumber { get; set; }

		[Required]
		public int? AddressId { get; set; }
		public virtual Address? Address { get; set; }

		[Required]
		public int? FacultyId { get; set; }
		public virtual Faculty? Faculty { get; set; }

		[Required]
		public int? CourseId { get; set; }
		public virtual Course? Course { get; set; }

		public virtual ICollection<StudentsSubjects>? StudentsSubjects { get; set; }
	}

	public class Gender
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? GenderName { get; set; }
	}

	public class StudentsSubjects
	{
		[Key, Column(Order = 0)]
		public int StudentId { get; set; }

		[Key, Column(Order = 1)]
		public int SubjectId { get; set; }

		public virtual Student? Student { get; set; }

		public virtual Subject? Subject { get; set; }

		[Required]
		public int? Year { get; set; }

		[Required]
		public int? GradeId { get; set; }
		public virtual Grade? Grade { get; set; }

		public int? TeacherId { get; set; }
	}

	public class Grade
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(20)]
		public string? GradeName { get; set; }
	}
}
