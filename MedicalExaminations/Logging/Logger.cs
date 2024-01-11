using MedicalExaminations.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicalExaminations.Logging
{
    /// <summary>
    /// Singleton logger
    /// </summary>
    public class Logger
    {
        private static Logger instance;

        private Logger() { }

        public static Logger GetInstance()
        {
            if (instance == null)
                instance = new Logger();
            return instance;
        }

        /// <summary>
        /// Создает экземпляр LogEntry и сохраняет его в базу данных
        /// </summary>
        /// <param name="context">DbContext базы данных, в которую записывается лог</param>
        /// <param name="actionType">Тип действия, совершенного пользователем</param>
        /// <param name="objectId">Id объекта, над которым совершено действие</param>
        /// <param name="objectAttributes">Атрибутивный состав объекта после совершенного пользователем действия (JSON)</param>
        /// <param name="fileId">Id загруженного или удаленного файла (при наличии)</param>
        public void Log(AppDbContext context, ActionType actionType, int objectId, string objectAttributes = "-", int? fileId = null)
        {
            context.LogEntries.Add(new LogEntry()
            {
                UserId = GlobalConfig.CurrentUser.Id, 
                Timestamp = DateTime.UtcNow, 
                ActionTypeId = actionType.Id,
                ObjectId = objectId,
                ObjectAttributes = objectAttributes,
                FileId = fileId
            });
        }
    }
}
