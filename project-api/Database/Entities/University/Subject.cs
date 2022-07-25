using project_api.Database.Entities.People;
using System.ComponentModel.DataAnnotations;

namespace project_api.Database.Entities.University
{
	public class Subject
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		public virtual ICollection<Schedule>? Schedule { get; set; }

		public virtual ICollection<Teacher>? Teachers { get; set; }

		public virtual ICollection<StudentsSubjects>? StudentsSubjects { get; set; }
	}
}
