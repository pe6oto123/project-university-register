using project_mvc.Database.Entities.People;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Database.Entities.Access
{
	public class Account
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? UserName { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		public virtual UserRole? UserRole { get; set; }

		public virtual Teacher? Teacher { get; set; }

		public virtual Student? Student { get; set; }
	}

	public class UserRole
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? Name { get; set; }
	}
}
