using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public DateOnly SigningDate { get; set; }
        public DateOnly ValidUntil { get; set; }
        public int ClientId { get; set; }
        public Organization? Client { get; set; }
        public int ExecutorId { get; set; }
        public Organization? Executor { get; set; }

        public List<ContractLocation> ContractLocations { get; set; }
        public List<MedicalExamination> MedicalExaminations { get; set; }
        [NotMapped]
        public string Display { get { return $"№{Number} от {SigningDate}"; } }
    }
}
