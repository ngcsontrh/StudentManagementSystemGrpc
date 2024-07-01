namespace Server.Entities
{
    public class Teacher
    {
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; } = null!;
        public virtual DateTime Birthday { get; set; }

        public virtual IList<Class> TeacherClasses { get; set; } = null!;
    }
}
