using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MedicalExaminations.Models
{
    public class LogEntry
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; } 
        /// <summary>
        /// Пользователь, совершивший действие
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Дата и время совершения действия
        /// </summary>
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата и время (UTC)")]
        public DateTime Timestamp { get; set; }

        [Display(Name = "Тип действия")]
        public int ActionTypeId { get; set; }

        /// <summary>
        /// Тип действия, совершенного пользователем
        /// </summary>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Id объекта, над которым совершено действие
        /// </summary>
        [Display(Name = "Идентификационный номер экземпляра объекта")]
        public int? ObjectId { get; set; }

        /// <summary>
        /// Атрибуты объекта после совершенного действия (JSON)
        /// </summary>
        [DataType(DataType.Text)]
        [Display(Name = "Описание экземпляра объекта после совершения действия")]
        public string ObjectAttributes { get; set; }

        /// <summary>
        /// Id файла, загруженного пользователем
        /// </summary>
        [Display(Name = "Идентификационный номер загруженного файла")]
        public int? FileId { get; set; }

        [NotMapped]
        public string FileIdDisplay { get { return FileId == null ? "-" : FileId.ToString(); } }
    }
}
