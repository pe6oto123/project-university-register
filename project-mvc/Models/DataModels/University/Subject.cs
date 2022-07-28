using project_mvc.Models.DataModels.People;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Models.DataModels.University
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? SubjectName { get; set; }

		public virtual ICollection<Schedule>? Schedule { get; set; }

		public virtual ICollection<Teacher>? Teachers { get; set; }

		public virtual ICollection<StudentsSubjects>? StudentsSubjects { get; set; }
	}
}
