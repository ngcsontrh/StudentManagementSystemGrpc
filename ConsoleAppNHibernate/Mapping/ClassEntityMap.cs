using FluentNHibernate.Mapping;
using QLSVConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNHibernate.Mapping
{
    internal class ClassEntityMap : ClassMap<Class>
    {
        public ClassEntityMap()
        {
            Table("class");
            Id(x => x.Id, "class_id");
            Map(x => x.Name, "class_name").Not.Nullable();
            Map(x => x.Subject, "class_subject").Not.Nullable();
            References(x => x.Teacher, "teacher_id").Not.Nullable();
            HasMany(x => x.Students)
                .KeyColumn("student_id")
                .Cascade.All();
        }
    }
}
