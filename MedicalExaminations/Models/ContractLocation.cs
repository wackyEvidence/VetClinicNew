using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models
{
    public class ContractLocation
    {
        [Key]
        public int Id { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public double ExaminationCost { get; set; }
    }
}
