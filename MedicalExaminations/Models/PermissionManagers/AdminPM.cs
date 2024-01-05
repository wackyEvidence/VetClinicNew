namespace MedicalExaminations.Models.PermissionManagers
{
    public class AdminPM : PermissionManager
    {
        public override bool CanViewAnimalsRegistry => true;
        public override bool CanViewOrganizationsRegistry => true;
        public override bool CanViewContractsRegistry => true;
        public override bool CanEditAnimalsRegistry => true;
        public override bool CanEditOrganizationsRegistry => true;
        public override bool CanEditContractsRegistry => true;
        public override Func<Animal, bool> AnimalsFilter => (animal) => true;
        public override Func<Organization, bool> OrganizationsFilter => (organization) => true;
        public override Func<Contract, bool> ContractsFilter => (contract) => true;
    }
}
