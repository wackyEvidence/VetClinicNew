using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalExaminations.Models.PermissionManagers
{
    [NotMapped]
    public static class PermissionManagerFactory
    {
        /// <summary>
        /// Устанавливает для переданного пользователя нужный Permission Manager 
        /// </summary>
        /// <param name="user">Пользователь, которому требуется получить PermissionManager</param>
        public static void SetPermissionManager(this User user)
        {
            switch(user.Role.Name)
            {
                case "VetServiceCurator":
                case "VetServiceSignatory":
                    user.PermissionManager = new VetServicePM();
                    break;
                case "TrappingCurator":
                case "AnimalShelterCurator":
                    user.PermissionManager = new CuratorPM(user);
                    break;
                case "VetServiceOperator":
                case "TrappingOperator":
                    user.PermissionManager = new VetServiceOperatorPM();
                    break;
                case "TrappingSignatory":
                case "AnimalShelterSignatory":
                    user.PermissionManager = new SignatoryPM(user);
                    break;
                case "OMSU_Curator":
                case "OMSU_Signatory":
                    user.PermissionManager = new OmsuPM(user);
                    break;
                case "OmsuOperator":
                    user.PermissionManager = new OmsuOperatorPM(user);
                    break;
                case "AnimalShelterOperator":
                case "Veterinarian":
                case "AnimalShelterVeterinarian":
                    user.PermissionManager = new VetPM();
                    break;
            }
        }
    }
}
