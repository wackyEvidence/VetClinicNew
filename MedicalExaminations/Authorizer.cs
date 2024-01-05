using MedicalExaminations.Models;
using MedicalExaminations.Models.PermissionManagers;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace MedicalExaminations
{
    public class Authorizer
    {
        private readonly AppDbContext context; 
        public Authorizer(AppDbContext context) => this.context = context;

        public bool Authorize(string login, string password)
        {
            var user = context.Users
                .Include(u => u.Role)
                .Include(u => u.Workplace)
                .Where(u => u.Login == login).FirstOrDefault();

            if (user == null || !BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
                return false;

            GlobalConfig.CurrentUser = user;
            PermissionManagerFactory.SetPermissionManager(user);
            return true;
        }
    }
}
