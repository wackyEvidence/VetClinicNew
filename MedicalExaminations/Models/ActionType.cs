using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models
{
    public class ActionType
    {
        private readonly Dictionary<string, string> actionTypeConvert = new Dictionary<string, string>()
        {
            { "Create", "Создание" },
            { "Update", "Редактирование" },
            { "Delete", "Удаление" },
            { "Upload file", "Загрузка файла" },
            { "Delete file", "Удаление файла" }
        };

        [Key]
        public int Id { get; set; }
        
        [StringLength(30)]
        public string Name { get; set; }

        public List<LogEntry> LogEntries { get; set; }

        [NotMapped]
        public string Display { get { return actionTypeConvert[Name]; } }
    }
}
