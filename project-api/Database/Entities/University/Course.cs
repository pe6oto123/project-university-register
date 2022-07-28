using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_api.Database.Entities.University
{
	public class Course
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int? CourseNId { get; set; }
		public virtual CourseN? CourseN { get; set; }

		[Required, Column(TypeName = "date")]
		public DateTime? Enrolment { get; set; }

		[Required]
		public virtual Faculty? Faculty { get; set; }
	}

	public class FacultyNum
	{
		[Key]
		public int CourseId { get; set; }

		public virtual Course? Course { get; set; }

		[Required]
		public int NextFreeId { get; set; }
	}

	public class CourseN
	{
		[Key]
		public int Id { get; set; }

		[Required, StringLength(50)]
		public string? CourseName { get; set; }
	}

	public class Schedule
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int? CourseId { get; set; }
		public virtual Course? Course { get; set; }

		[Required]
		public int? Year { get; set; }

		public virtual ICollection<Subject>? Subjects { get; set; }
	}
}
