namespace Client.Blazor.Models
{
    public class StudentPaginatedModel
    {
        public int Total { get; set; }
        public List<StudentProfileModel>? Students { get; set; }
    }
}
