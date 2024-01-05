using System.ComponentModel.DataAnnotations;

namespace MedicalExaminations.Models
{
    public class MedicalExamination
    {
        [Key]
        public int Id { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }

        [Display(Name = "Особенности поведения")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string? BehaviourFeatures { get; set; }

        [Display(Name = "Состояние животного")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string AnimalCondition { get; set; }

        [Display(Name = "Температура тела")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [Range(30, 50, ErrorMessage = "Значение должно быть в промежутке от 30 до 50")]
        public double BodyTemperature { get; set; }

        [Display(Name = "Кожные покровы")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string SkinCovers { get; set; }

        [Display(Name = "Состояние шерсти")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string WoolCondition { get; set; }

        [Display(Name = "Ранения, травмы, и другие повреждения")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string? Injuries { get; set; }

        [Display(Name = "Требуется экстренная ветеринарная помощь")]
        public bool EmergencyHelpRequired { get; set; }

        [Display(Name = "Диагноз")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string Diagnosis { get; set; }

        [Display(Name = "Проведенные манипуляции")]
        [StringLength(300, ErrorMessage = "Максимальная длина: 300 символов")]
        public string? ActionsTaken { get; set; }

        [Display(Name = "Назначено лечение")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(300, ErrorMessage = "Максимальная длина: 300 символов")]
        public string TreatmentPrescribed { get; set; }

        [Display(Name = "Дата осмотра")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [DataType(DataType.Date)]
        public DateOnly ExaminationDate { get; set; }

        [Display(Name = "ФИО ветеринарного специалиста")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(200, ErrorMessage = "Максимальная длина: 200 символов")]
        public string VeterinarianFullName { get; set; }

        [Display(Name = "Должность ветеринарного специалиста")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        [StringLength(50, ErrorMessage = "Максимальная длина: 50 символов")]
        public string VeterinarianPosition { get; set; }

        [Display(Name = "Ветклиника, в которой проведён осмотр")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public int VetClinicId { get; set; }

        public Organization? VetClinic { get; set; }

        [Display(Name = "Муниципальный контракт")]
        [Required(ErrorMessage = "Необходимо заполнить поле")]
        public int ContractId { get; set; }

        public Contract? Contract { get; set; }
    }
}
