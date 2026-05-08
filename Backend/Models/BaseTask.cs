using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Acadeno.Backend.Enums;

namespace Acadeno.Backend.Models
{
    public class BaseTask
    {
        [Key]
        public Guid TaskID {get; set;} = Guid.NewGuid();

        public string Name {get; set;} = string.Empty;
        public string? Description {get; set;} = string.Empty;
        public int Difficulty {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime DueDate {get; set;}

        [Column("Status")]
        public Status TaskStatus {get; set;} = (Status) 1;
        public RiskLevel RiskLevel {get; set;}

        //  Foreign Key
        public Guid UserID {get; set;}
    }
}
