using project_api.Database.Entities.People;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_api.Database.Entities.University
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? SubjectName { get; set; }

		[Required]
		public int? FacultyId { get; set; }

		public virtual Faculty? Faculty { get; set; }

		public virtual ICollection<SchedulesSubjects>? SchedulesSubjects { get; set; }

		public virtual ICollection<TeachersSubjects>? TeachersSubjects { get; set; }

		public virtual ICollection<StudentsSubjects>? StudentsSubjects { get; set; }
	}

	public class SchedulesSubjects
	{
		[Key, Column(Order = 0)]
		public int ScheduleId { get; set; }

		[Key, Column(Order = 1)]
		public int SubjectId { get; set; }

		public virtual Schedule? Schedule { get; set; }

		public virtual Subject? Subject { get; set; }
	}
}
