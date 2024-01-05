using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models.PermissionManagers
{
    [NotMapped]
    public class VetServiceOperatorPM : PermissionManager
    {
        public override bool CanViewAnimalsRegistry => true;
        public override bool CanViewOrganizationsRegistry => true;
        public override bool CanViewContractsRegistry => true;
        public override bool CanEditAnimalsRegistry => false;
        public override bool CanEditOrganizationsRegistry => true;
        public override bool CanEditContractsRegistry => false;

        public override Func<Animal, bool> AnimalsFilter => (animal => true);
        public override Func<Organization, bool> OrganizationsFilter => (organization =>
        {
            return
            organization.OrganizationType.Name == "Исполнительный орган государственной власти" ||
            organization.OrganizationType.Name == "Орган местного самоуправления" ||
            organization.OrganizationType.Name == "Ветеринарная клиника: государственная";
        });
        public override Func<Contract, bool> ContractsFilter => (contract => true);
    }
}
