using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models.PermissionManagers
{
    [NotMapped]
    public class VetServicePM : PermissionManager
    {
        public override bool CanViewAnimalsRegistry => true;

        public override bool CanViewOrganizationsRegistry => true;

        public override bool CanViewContractsRegistry => true;

        public override bool CanEditAnimalsRegistry => false;

        public override bool CanEditOrganizationsRegistry => false;

        public override bool CanEditContractsRegistry => false;

        public override Func<Animal, bool> AnimalsFilter => (animal => true);

        public override Func<Organization, bool> OrganizationsFilter => (organization => true);

        public override Func<Contract, bool> ContractsFilter => (contract => true);
    }
}
