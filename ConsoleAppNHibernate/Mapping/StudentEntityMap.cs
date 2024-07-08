using FluentNHibernate.Mapping;
using QLSVConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNHibernate.Mapping
{
    internal class StudentEntityMap : ClassMap<Student>
    {
        public StudentEntityMap()
        {
            Id(x => x.Id, "student_id").GeneratedBy.Identity();
            Map(x => x.Name, "student_fullname").Not.Nullable();
            Map(x => x.Birthday, "student_birthday").Not.Nullable();
            Map(x => x.Address, "student_address").Not.Nullable();
            References(x => x.Class, "student_class_id").Not.Nullable();
        }
    }
}
