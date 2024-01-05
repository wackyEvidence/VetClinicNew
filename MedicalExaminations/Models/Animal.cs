using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Регистрационный номер")]
        public int RegistrationNumber { get; set; }

        [Display(Name = "Населённый пункт")]
        public int LocationId { get; set; }

        public Location Location { get; set; } = null!;

        [Display(Name = "Категория")]
        public int AnimalCategoryId { get; set; }

        public AnimalCategory AnimalCategory { get; set; } = null!;

        [Display(Name = "Пол")]
        public char Sex { get; set; }

        [Display(Name = "Год рождения")]
        public int BirthYear { get; set; }

        [Display(Name = "Номер чипа")]
        public int ChipNumber { get; set; }

        [Display(Name = "Кличка")]
        public string Nickname { get; set; } = null!;

        [Display(Name = "Особые приметы")]
        public string? DistinguishingMarks { get; set; }

        [Display(Name = "Признаки владельца")]
        public List<OwnerSign>? OwnerSigns { get; set; }
        public List<MedicalExamination>? MedicalExaminations { get; set; }
        [Display(Name = "Фотографии")]
        public List<AnimalPhoto>? AnimalPhotos { get; set; }
    }
}