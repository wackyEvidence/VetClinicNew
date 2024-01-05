using MedicalExaminations.Models;

namespace MedicalExaminations
{
    /// <summary>
    /// Состояние приложения 
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// Текущий авторизованный пользователь
        /// </summary>
        public static User CurrentUser = null!;
    }
}
