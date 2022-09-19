using System.Runtime.Serialization;

namespace project_mvc.Models.AnalysisModels.Derived
{
	public class StudentsGraph : FacultyGraph
	{
		public int? TotalStudents { get; set; }
		public ICollection<StudentsGenderGraph>? StudentsGenderGraph { get; set; }
	}

	[DataContract]
	public class StudentsGenderGraph
	{
		[DataMember(Name = "y")]
		public int? StudentsNum { get; set; }
		[DataMember(Name = "name")]
		public string? StudentGender { get; set; }
		[DataMember(Name = "color")]
		public string? Color { get; set; }
		[DataMember(Name = "StudentsGradeGraph")]
		public ICollection<StudentsGradeGraph>? StudentsGradeGraph { get; set; }
	}

	[DataContract]
	public class StudentsGradeGraph
	{
		[DataMember(Name = "label")]
		public string? GradeName { get; set; }
		[DataMember(Name = "y")]
		public double? GradeNum { get; set; }

	}
}
