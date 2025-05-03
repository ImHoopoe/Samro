using WinWin.DataLayer.Entities.Roles;

public interface IRolePermission
{
    Task<int> AddRoleAsync(Role role);
    Task AddRolesToUserAsync(List<int> roleIds, Guid userId);
    Task EditUserRolesAsync(List<int> roles, Guid userId);
    Task<Role?> GetRoleByIdAsync(int roleId);
    Task<IEnumerable<Role>> GetRolesAsync();
    Task UpdateRoleAsync(Role role);
    Task DeleteRoleAsync(Role role);
    Task<IEnumerable<Permission>> GetAllPermissionsAsync();
    Task AddPermissionsAsync(int roleId, List<int> permissions);
    Task<List<int>> GetRolePermissionsAsync(List<int> roleId);
    Task UpdatePermissionsAsync(int roleId, List<int> permissions);
    Task<bool> CheckPermissionAsync(int permissionId, string userName);
}