using System.Runtime.Serialization;

namespace project_mvc.Models.AnalysisModels.Derived
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

	[DataContract]
	public class CoursesGradeGraph
	{
		[DataMember(Name = "x")]
		public DateTime? CourseDate { get; set; }
		[DataMember(Name = "y")]
		public double? AverageGrade { get; set; }
	}
}
