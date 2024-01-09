using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class ActionType
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(30)]
        public string Name { get; set; }

        public List<LogEntry> LogEntries { get; set; }
    }
}
