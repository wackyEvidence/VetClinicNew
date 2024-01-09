using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public int ActionTypeId { get; set; }
        /// <summary>
        /// Тип действия, совершенного пользователем
        /// </summary>
        public ActionType ActionType { get; set; }

        /// <summary>
        /// Id объекта, над которым совершено действие
        /// </summary>
        public int? ObjectId { get; set; }

        /// <summary>
        /// Атрибуты объекта после совершенного действия (JSON)
        /// </summary>
        [DataType(DataType.Text)]
        public string ObjectAttributes { get; set; }

        /// <summary>
        /// Id файла, загруженного пользователем
        /// </summary>
        public int? FileId { get; set; }
    }
}
