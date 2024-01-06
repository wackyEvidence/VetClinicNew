using MedicalExaminations.Controllers;
using MedicalExaminations.Models.PermissionManagers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; } = null!;
        public int RoleId { get; set; }
        public UserRole Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int WorkplaceId { get; set; }
        public Organization Workplace { get; set; }

        [NotMapped]
        public PermissionManager? PermissionManager { get; set; }
    }
}
