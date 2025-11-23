using Business_School.Models.JoinTables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Business_School.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public StudentLevel Level { get; set; }

        // Many-to-many with Student

        public ICollection<StudentClub> ClubMemberships { get; set; } = new List<StudentClub>();
        public ICollection<EventAttendance> EventAttendances { get; set; } = new List<EventAttendance>();
    }


}
