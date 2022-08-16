using project_mvc.Models.DataModels.People;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Models.DataModels.Access
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? UserName { get; set; }

		[Required]
		public string? Password { get; set; }

		[Required]
		public int? UserRoleId { get; set; }
		public virtual UserRole? UserRole { get; set; }

		public int? TeacherId { get; set; }
		public virtual Teacher? Teacher { get; set; }

		public int? StudentId { get; set; }
		public virtual Student? Student { get; set; }
	}

	public class UserRole
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string? UserRoleName { get; set; }
	}
}
