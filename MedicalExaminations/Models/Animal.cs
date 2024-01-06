using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Регистрационный номер")]
        [Required(ErrorMessage = "Введите регистрационный номер")]
        [Range(0, 1000000000, ErrorMessage = "Введите число от 0 до 1000000000")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "Населённый пункт")]
        [Required(ErrorMessage = "Выберите населенный пункт")]
        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Выберите категорию животного")]
        public int AnimalCategoryId { get; set; }
        public AnimalCategory AnimalCategory { get; set; } = null!;

        [Display(Name = "Пол")]
        [Required(ErrorMessage = "Введите пол животного")]
        [StringLength(1)]
        public char Sex { get; set; }

        [Display(Name = "Год рождения")]
        [Required(ErrorMessage = "Введите год рождения")]
        [RegularExpression("\\b\\d{4}\\b", ErrorMessage = "Введите год рождения числом (напр. 2015)")]
        [StringLength(4, ErrorMessage = "Введите год рождения числом (напр. 2015)")]
        public int BirthYear { get; set; }

        [Display(Name = "Номер чипа")]
        [Required(ErrorMessage = "Введите номер чипа")]
        [Range(0, 1000000000, ErrorMessage = "Введите число от 0 до 1000000000")]
        public int ChipNumber { get; set; }

        [Display(Name = "Кличка")]
        [Required(ErrorMessage = "Введите кличку животного")]
        [StringLength(100, ErrorMessage = "Максимальная длина поля 100 символов")]
        public string Nickname { get; set; } = null!;

        [Display(Name = "Особые приметы")]
        public string? DistinguishingMarks { get; set; }

        [Display(Name = "Признаки владельца")]
        [ValidateNever]
        public List<OwnerSign>? OwnerSigns { get; set; }

        [ValidateNever]
        public List<MedicalExamination>? MedicalExaminations { get; set; }

        [Display(Name = "Фотографии")]
        [ValidateNever]
        public List<AnimalPhoto>? AnimalPhotos { get; set; }
    }
}