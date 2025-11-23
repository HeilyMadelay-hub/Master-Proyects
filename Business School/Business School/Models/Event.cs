using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;                 
using Business_School.Models.JoinTables;           

namespace Business_School.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int? Capacity { get; set; }
        public int PointsReward { get; set; } = 10;

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public string? OrganizerId { get; set; } = string.Empty;
        public IdentityUser? Organizer { get; set; } = null!;

        public ICollection<EventClub> EventClubs { get; set; } = new List<EventClub>();
        public ICollection<EventAttendance> EventAttendances { get; set; } = new List<EventAttendance>();
    }
}