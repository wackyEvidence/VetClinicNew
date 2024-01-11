using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MedicalExaminations.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Введите наименование организации")]
        [StringLength(100, ErrorMessage = "Максимальная длина поля 100 символов")]
        public string Name { get; set; } = null!;

        [Display(Name = "ИНН")]
        [Required(ErrorMessage = "Введите ИНН организации")]
        [RegularExpression("\\b\\d{12}\\b", ErrorMessage = "ИНН должен содержать не более 12 цифр")]
        public long INN { get; set; }

        [Display(Name = "КПП")]
        [Required(ErrorMessage = "Введите КПП организации")]
        [RegularExpression("\\b\\d{9}\\b", ErrorMessage = "КПП должен содержать не более 9 цифр")]
        public long KPP { get; set; }

        [Display(Name = "Населенный пункт")]
        [Required(ErrorMessage = "Укажите населенный пункт")]
        public int? LocationId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public Location? Location { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Укажите название улицы")]
        [StringLength(50, ErrorMessage = "Максимальная длина поля 50 символов")]
        public string Street { get; set; } = null!;

        [Display(Name = "Дом")]
        [Required(ErrorMessage = "Укажите номер дома")]
        [Range(1, 1000, ErrorMessage = "Номер дома - число в пределах от 1 до 1000")]
        public int HouseNumber { get; set; }

        [Display(Name = "Тип организации")]
        [Required(ErrorMessage = "Выберите тип организации")]
        public int? OrganizationTypeId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public OrganizationType? OrganizationType { get; set; }

        [Display(Name = "Форма организации")]
        [Required(ErrorMessage = "Укажите форму организации")]
        public int? OrganizationAttributeId { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public OrganizationAttribute? OrganizationAttribute { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public List<User> Users { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public List<MedicalExamination> MedicalExaminations { get; set; }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
