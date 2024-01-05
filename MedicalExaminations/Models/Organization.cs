using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class Organization
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Введите наименование организации")]
        public string Name { get; set; } = null!;

        [Display(Name = "ИНН")]
        [Required(ErrorMessage = "Введите ИНН организации")]
        public long INN { get; set; }

        [Display(Name = "КПП")]
        [Required(ErrorMessage = "Введите КПП организации")]
        public long KPP { get; set; }

        [Display(Name = "Населенный пункт")]
        [Required(ErrorMessage = "Укажите населенный пункт")]
        public int? LocationId { get; set; }
        public Location? Location { get; set; }

        [Display(Name = "Улица")]
        [Required(ErrorMessage = "Укажите название улицы")]
        public string Street { get; set; } = null!;

        [Display(Name = "Дом")]
        [Required(ErrorMessage = "Укажите номер дома")]
        public int HouseNumber { get; set; }

        [Display(Name = "Тип организации")]
        [Required(ErrorMessage = "Выберите тип организации")]
        public int? OrganizationTypeId { get; set; }
        public OrganizationType? OrganizationType { get; set; }

        [Display(Name = "Форма организации")]
        [Required(ErrorMessage = "Укажите форму организации")]
        public int? OrganizationAttributeId { get; set; }
        public OrganizationAttribute? OrganizationAttribute { get; set; }
        

        public List<User> Users { get; set; }
        public List<MedicalExamination> MedicalExaminations { get; set; }
    }
}
