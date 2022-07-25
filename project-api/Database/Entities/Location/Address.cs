using System.ComponentModel.DataAnnotations;

namespace project_api.Database.Entities.Location
{
	public class Address
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string? Name { get; set; }

		public virtual City? City { get; set; }
	}
}
