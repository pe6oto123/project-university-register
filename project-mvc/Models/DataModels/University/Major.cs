using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_mvc.Database.Entities.University
{
	public class Major
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public virtual MajorName? MajorName { get; set; }

		[Required, Column(TypeName = "date")]
		public DateTime? Enrolment { get; set; }

		[Required]
		public virtual Faculty? Faculty { get; set; }
	}

	public class FacultyNum
	{
		[Key]
		public int MajorId { get; set; }

		public virtual Major? Major { get; set; }

		[Required]
		public int NextFreeId { get; set; }
	}

	public class MajorName
	{
		[Key]
		public int Id { get; set; }

		[Required, StringLength(50)]
		public string? Name { get; set; }
	}

	public class Schedule
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public virtual Major? Major { get; set; }

		[Required]
		public int? Year { get; set; }

		public virtual ICollection<Subject>? Subjects { get; set; }
	}
}
