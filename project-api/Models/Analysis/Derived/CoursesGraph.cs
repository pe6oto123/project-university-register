namespace project_api.Models.Analysis.Derived
{
	public class CoursesGraph : FacultyGraph
	{
		public ICollection<CourseYearGraph>? CourseYearGraph { get; set; }
	}

	public class CourseYearGraph
	{
		public int CourseId { get; set; }
		public string? CourseName { get; set; }
		public ICollection<CoursesGradeGraph>? CoursesGradeGraph { get; set; }
	}

	public class CoursesGradeGraph
	{
		public DateTime? CourseDate { get; set; }
		public double? AverageGrade { get; set; }
	}
}
