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
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [StringLength(100)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [StringLength(100)]
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; } = null!;

        [StringLength(200)]
        [Display(Name = "Личная электронная почта")]
        /// <summary>
        /// Личная электронная почта
        /// </summary>
        public string PersonalEmail { get ; set; } 

        [StringLength(16)]
        [Display(Name = "Личный номер телефона")]
        /// <summary>
        /// Личный номер телефона
        /// </summary>
        public string PersonalPhone { get; set; }

        [StringLength(200)]
        [Display(Name = "Рабочий адрес электронной почты")]
        /// <summary>
        /// Рабочий адрес электронной почты
        /// </summary>
        public string WorkEmail { get; set; }

        [StringLength(16)]
        [Display(Name = "Рабочий номер телефона")]
        /// <summary>
        /// Рабочий номер телефона
        /// </summary>
        public string WorkPhone { get; set; }

        [StringLength(100)]
        [Display(Name = "Наименование структурного подразделения")]
        /// <summary>
        /// Наименование структурного подразделения
        /// </summary>
        public string UnitName { get; set; }

        [Display(Name = "Должность")]
        public int PositionId { get; set; }

        public UserPosition Position { get; set; }
        public int RoleId { get; set; }
        public UserRole Role { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        public string Password { get; set; }

        [Display(Name = "Организация")]
        public int WorkplaceId { get; set; }

        public Organization Workplace { get; set; }

        [NotMapped]
        public PermissionManager? PermissionManager { get; set; }
    }
}
