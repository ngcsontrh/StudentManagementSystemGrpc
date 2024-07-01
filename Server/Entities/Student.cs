namespace Server.Entities
{
    public class Student
    {
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; } = null!;
        public virtual DateTime Birthday { get; set; }
        public virtual string Address { get; set; } = null!;

        public virtual Class StudentClass { get; set; } = null!;
    }
}
