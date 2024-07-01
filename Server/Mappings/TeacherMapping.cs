using FluentNHibernate.Mapping;
using Server.Entities;

namespace Server.Mappings
{
    public class TeacherMapping : ClassMap<Teacher>
    {
        public TeacherMapping()
        {
            Table("teacher");
            Id(t => t.Id, "teacher_id");
            Map(t => t.FullName, "teacher_fullname").Not.Nullable();
            Map(t => t.Birthday, "teacher_birthday").Not.Nullable();
            HasMany(t => t.TeacherClasses)
                .KeyColumn("class_id")
                .Inverse()
                .Cascade.None();
        }
    }
}
