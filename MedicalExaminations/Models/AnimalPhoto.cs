using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class AnimalPhoto
    {
        [Key]
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public byte[] PhotoData { get; set; }
    }
}