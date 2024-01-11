using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public DateOnly StartingDate { get; set; }
        public DateOnly EndingDate { get; set; }

    }
}
