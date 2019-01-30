using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BeltExam.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set; }
        [Required]
        [MinLength(3)]
        public string FirstName {get; set; }
        [Required]
        [MinLength(3)]
        public string LastName {get; set; }
        [Required]
        [MinLength(3)]
        [EmailAddress]
        public string Email {get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
        public List<Participant> Participant {get; set;}
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}