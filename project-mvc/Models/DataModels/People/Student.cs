using project_mvc.Database.Entities.Location;
using project_mvc.Database.Entities.University;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_mvc.Database.Entities.People
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
        [StringLength(12)]
        public string? FacultyNumber { get; set; }

        [Required]
        public virtual Address? Address { get; set; }

        [Required]
        public virtual Faculty? Faculty { get; set; }

        [Required]
        public virtual Major? Major { get; set; }

        public virtual ICollection<StudentsSubjects>? StudentsSubjects { get; set; }
    }

    public class StudentsSubjects
    {
        [Key, Column(Order = 0)]
        public int StudentsId { get; set; }

        [Key, Column(Order = 1)]
        public int SubjectsId { get; set; }

        public virtual Student? Student { get; set; }

        public virtual Subject? Subject { get; set; }

        [MaxLength(20)]
        public virtual Grade? Grade { get; set; }
    }

    public class Grade
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string? Name { get; set; }
    }
}
