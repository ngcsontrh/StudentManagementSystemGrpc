namespace Server.DTOs
{
    public class SearchStudentDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Address { get; set; }
        public int? ClassId { get; set; }
    }
}
