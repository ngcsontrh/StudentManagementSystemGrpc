using FluentNHibernate.Mapping;
using Server.Entities;

namespace Server.Mappings
{
    public class ClassMapping : ClassMap<Class>
    {
        public ClassMapping()
        {
            Table("class");
            Id(c => c.Id, "class_id");
            Map(c => c.Name, "class_name").Not.Nullable();
            Map(c => c.Subject, "class_subject").Not.Nullable();
            References(c => c.ClassTeacher, "teacher_id").Not.Nullable();
            HasMany(c => c.ClassStudents)
                .KeyColumn("student_id")
                .Inverse()
                .Cascade.All();
        }
    }
}
