﻿using System.ComponentModel.DataAnnotations;

namespace project_mvc.Models.DataModels.University
{
	public class Course
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int? CourseNId { get; set; }
		public virtual CourseN? CourseN { get; set; }

		[Required, DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime? Enrolment { get; set; }

		[Required, Range(1, 4)]
		public int? CourseLength { get; set; }

		[Required]
		public int? FacultyId { get; set; }
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

		public virtual ICollection<SchedulesSubjects>? SchedulesSubjects { get; set; }
	}
}
