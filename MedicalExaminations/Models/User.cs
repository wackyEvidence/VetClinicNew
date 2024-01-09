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

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [StringLength(100)]
        public string? Patronymic { get; set; } = null!;

        [StringLength(200)]
        /// <summary>
        /// Личная электронная почта
        /// </summary>
        public string PersonalEmail { get ; set; } 

        [StringLength(16)]
        /// <summary>
        /// Личный номер телефона
        /// </summary>
        public string PersonalPhone { get; set; }

        [StringLength(200)]
        /// <summary>
        /// Рабочий адрес электронной почты
        /// </summary>
        public string WorkEmail { get; set; }

        [StringLength(16)]
        /// <summary>
        /// Рабочий номер телефона
        /// </summary>
        public string WorkPhone { get; set; }

        [StringLength(100)]
        /// <summary>
        /// Наименование структурного подразделения
        /// </summary>
        public string UnitName { get; set; }
        public int PositionId { get; set; }
        public UserPosition Position { get; set; }
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
