using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BeltExam.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId {get; set; }
        [Required]
        [MinLength(3)]
        public string Name {get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]

        public TimeSpan Duration {get; set; }
        [Required]
        [MinLength(10)]
        public String Discription {get; set; }
        public int UserId {get; set; }
        public List<Participant> Participants {get; set;}

        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;

    }
}