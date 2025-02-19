using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using OrchardCore.Environment.Commands;
using OrchardCore.Security;
using System.Threading.Tasks;
using static OrchardCore.Security.Permissions.Permission;

namespace Lombiq.HelpfulLibraries.Libraries.Users
{
    public class RoleCommands : DefaultCommandHandler
    {
        private readonly RoleManager<IRole> _roleManager;

        [OrchardSwitch]
        public string RoleName { get; set; }

        [OrchardSwitch]
        public string Permission { get; set; }

        public RoleCommands(RoleManager<IRole> roleManager, IStringLocalizer<RoleCommands> localizer)
            : base(localizer) =>
            _roleManager = roleManager;

        [CommandName("addPermissionToRole")]
        [CommandHelp("addPermissionToRole " +
            "/RoleName:<rolename> " +
            "/Permission:<permission> " +
            "\r\n\t" + "Adds the permission to the role")]
        [OrchardSwitches("RoleName, Permission")]
        public async Task AddPermissionToRoleAsync()
        {
            var role = (Role)await _roleManager.FindByNameAsync(_roleManager.NormalizeKey(RoleName));
            role.RoleClaims.Add(new RoleClaim { ClaimType = ClaimType, ClaimValue = Permission });
            await _roleManager.UpdateAsync(role);
        }

        [CommandName("removePermissionFromRole")]
        [CommandHelp("removePermissionFromRole " +
                     "/RoleName:<rolename> " +
                     "/Permission:<permission> " +
                     "\r\n\t" + "Removes the permission from the role")]
        [OrchardSwitches("RoleName, Permission")]
        public async Task RemovePermissionFromRoleAsync()
        {
            if ((Role)await _roleManager.FindByNameAsync(_roleManager.NormalizeKey(RoleName)) is not { } role) return;
            role.RoleClaims.RemoveAll(claim => claim.ClaimType == ClaimType && claim.ClaimValue == Permission);
            await _roleManager.UpdateAsync(role);
        }
    }
}
