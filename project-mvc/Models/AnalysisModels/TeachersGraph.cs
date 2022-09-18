using System.Runtime.Serialization;

namespace project_mvc.Models.AnalysisModels
{
	public class TeachersGraph
	{
		public int SubjectId { get; set; }
		public string? SubjectName { get; set; }
		public ICollection<TeachersGradeGraph>? TeachersGradeGraph { get; set; }
	}

	[DataContract]
	public class TeachersGradeGraph
	{
		[DataMember(Name = "label")]
		public string? TeacherFullName { get; set; }
		[DataMember(Name = "y")]
		public double? AverageGrade { get; set; }
	}
}
