using CQRSExample.Core.Entities;

namespace CQRSExample.WebAPI.Features.Roles
{
    public class RoleApiModel
    {        
        public int RoleId { get; set; }
        public string Name { get; set; }

        public static RoleApiModel FromRole(Role role)
        {
            var model = new RoleApiModel();
            model.RoleId = role.RoleId;
            model.Name = role.Name;
            return model;
        }        
    }
}
