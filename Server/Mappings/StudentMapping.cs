using FluentNHibernate.Mapping;
using Server.Entities;

namespace Server.Mappings
{
    public class StudentMapping : ClassMap<Student>
    {
        public StudentMapping()
        {
            Table("Student");
            Id(s => s.Id, "student_id").GeneratedBy.Identity();
            Map(s => s.FullName, "student_fullname").Not.Nullable();
            Map(s => s.Birthday, "student_birthday").Not.Nullable();
            Map(s => s.Address, "student_address").Not.Nullable();
            References(s => s.StudentClass, "student_class_id").Not.Nullable();
        }
    }
}
