namespace Server.Entities
{
    public class Class
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; } = null!;
        public virtual string Subject { get; set; } = null!;

        public virtual Teacher ClassTeacher { get; set; } = null!;
        public virtual IList<Student> ClassStudents { get; set; } = new List<Student>();
    }
}
