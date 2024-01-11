using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class UserPosition
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
