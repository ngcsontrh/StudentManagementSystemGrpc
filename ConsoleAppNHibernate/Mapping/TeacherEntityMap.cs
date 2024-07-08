using FluentNHibernate.Mapping;
using QLSVConsole.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNHibernate.Mapping
{
    internal class TeacherEntityMap : ClassMap<Teacher>
    {
        public TeacherEntityMap()
        {
            Table("teacher");
            Id(x => x.Id, "teacher_id");
            Map(x => x.Name, "teacher_fullname").Not.Nullable();
            Map(x => x.Birthday, "teacher_birthday").Not.Nullable();
            HasMany(x => x.Classes)
                .KeyColumn("class_id")
                .Cascade.All();
        }
    }
}
