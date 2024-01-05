using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
