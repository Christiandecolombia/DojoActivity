using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BeltExam.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId {get; set; }

        public int UserId {get; set; }
        public int ActivityId {get; set; }
        public User User {get; set; }
        public Activity Activity {get; set; }

    }
}