﻿using System.ComponentModel.DataAnnotations;

namespace Client.ConsoleApp.DTOs
{
    public class StudentProfileDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string Address { get; set; } = null!;

        public int ClassId { get; set; }

        public string ClassName { get; set; } = null!;

        public string ClassSubject { get; set; } = null!;

        public int TeacherId { get; set; }

        public string TeacherFullName { get; set; } = null!;

        public DateTime TeacherBirthday { get; set; }

    }
}
