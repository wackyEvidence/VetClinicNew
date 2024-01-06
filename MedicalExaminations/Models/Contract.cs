using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Номер")]
        [Required(ErrorMessage = "Введите номер контракта")]
        [Range(1, 1000000000, ErrorMessage = "Введите число от 0 до 1000000000")]
        public int Number { get; set; }

        [Display(Name = "Действителен до")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Выберите дату")]
        public DateOnly SigningDate { get; set; }

        [Display(Name = "Дата подписания")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Выберите дату")]
        public DateOnly ValidUntil { get; set; }

        [Display(Name = "Заказчик")]
        [Required(ErrorMessage = "Выберите организацию-заказчика")]
        public int ClientId { get; set; }

        [ValidateNever]
        public Organization? Client { get; set; }

        [Display(Name = "Исполнитель")]
        [Required(ErrorMessage = "Выберите организацию-исполнителя")]
        public int ExecutorId { get; set; }

        [ValidateNever]
        public Organization? Executor { get; set; }

        [ValidateNever]
        public List<ContractLocation> ContractLocations { get; set; }

        [ValidateNever]
        public List<MedicalExamination> MedicalExaminations { get; set; }

        [NotMapped]
        public string Display { get { return $"№{Number} от {SigningDate}"; } }
    }
}
