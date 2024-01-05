using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models.PermissionManagers
{
    [NotMapped]
    public class OmsuOperatorPM : PermissionManager
    {
        private readonly User user;
        public OmsuOperatorPM(User user) => this.user = user;   

        public override bool CanViewAnimalsRegistry => true;
        public override bool CanViewOrganizationsRegistry => true;
        public override bool CanViewContractsRegistry => true;
        public override bool CanEditAnimalsRegistry => false;
        public override bool CanEditOrganizationsRegistry => true;
        public override bool CanEditContractsRegistry => true;
        public override Func<Animal, bool> AnimalsFilter => (animal => animal.LocationId == user.Workplace.LocationId);
        public override Func<Organization, bool> OrganizationsFilter => (organization => organization.LocationId == user.Workplace.LocationId);
        public override Func<Contract, bool> ContractsFilter => (contract =>
        {
            return contract.ContractLocations
                    .Select(cl => cl.LocationId)
                    .ToList()
                    .Contains((int)user.Workplace.LocationId);
        }
        );
    }
}
