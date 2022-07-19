﻿using project_mvc.Database.Entities.Location;
using project_mvc.Database.Entities.University;
using System.ComponentModel.DataAnnotations;

namespace project_mvc.Database.Entities.People
{
    public class Teacher
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
        [StringLength(50)]
        public string? Position { get; set; }

        [Required]
        public virtual Address? Address { get; set; }

        [Required]
        public virtual Faculty? Faculty { get; set; }

        public virtual ICollection<Subject>? Subjects { get; set; }
    }
}