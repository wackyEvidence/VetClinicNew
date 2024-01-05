using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class OrganizationAttribute
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Organization> Organizations { get; set; }
    }
}
