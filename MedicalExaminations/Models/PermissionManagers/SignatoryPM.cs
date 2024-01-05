using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models.PermissionManagers
{
    [NotMapped]
    public class SignatoryPM : PermissionManager
    {
        private readonly User user;
        public SignatoryPM(User user) => this.user = user;

        public override bool CanViewAnimalsRegistry => true;
        public override bool CanViewOrganizationsRegistry => true;
        public override bool CanViewContractsRegistry => true;
        public override bool CanEditAnimalsRegistry => false;
        public override bool CanEditOrganizationsRegistry => false;
        public override bool CanEditContractsRegistry => false;
        public override Func<Animal, bool> AnimalsFilter => (animal => true);
        public override Func<Organization, bool> OrganizationsFilter => (organization => true);
        /// <summary>
        /// Организация, которая соответствует месту работы пользователя.
        /// </summary>
        public override Func<Contract, bool> ContractsFilter => (contract => contract.ExecutorId == user.WorkplaceId);
    }
}
