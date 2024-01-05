using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class OrganizationType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Organization> Organizations { get; set; }
    }
}
