using System.Runtime.Serialization;

namespace project_mvc.Models.AnalysisModels.Derived
{
    public class CitiesGraph : FacultyGraph
    {
        public ICollection<CitiesGradeGraph>? CitiesGradeGraph { get; set; }
    }

    [DataContract]
    public class CitiesGradeGraph
    {
        [DataMember(Name = "label")]
        public string? CityName { get; set; }
        [DataMember(Name = "y")]
        public double? AverageGrade { get; set; }
    }
}
