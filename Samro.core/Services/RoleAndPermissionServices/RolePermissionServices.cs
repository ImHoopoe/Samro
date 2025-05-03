using Microsoft.EntityFrameworkCore;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.RolePernissionUser;
using WinWin.DataLayer.Entities.Roles;

namespace HopLearn.Services.UserService
{
    public class RolePermissionServices : IRolePermission
    {
        private readonly SamroContext _context;

        public RolePermissionServices(SamroContext context)
        {
            _context = context;
        }

        public async Task<int> AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role.RoleId;
        }

        public async Task AddRolesToUserAsync(List<int> roleIds, Guid userId)
        {
            var userRoles = roleIds.Select(roleId => new UserRole { RoleId = roleId, UserId = userId }).ToList();
            await _context.UserRoles.AddRangeAsync(userRoles);
            await _context.SaveChangesAsync();
        }

        public async Task EditUserRolesAsync(List<int> roles, Guid userId)
        {
            var existingRoles = await _context.UserRoles.Where(r => r.UserId == userId).ToListAsync();
            _context.UserRoles.RemoveRange(existingRoles);
            await AddRolesToUserAsync(roles, userId);
        }

        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Role role)
        {
            role.IsDeleted = true;
            await UpdateRoleAsync(role);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permisions.ToListAsync();
        }

        public async Task AddPermissionsAsync(int roleId, List<int> permissions)
        {
            var rolePermissions = permissions.Select(permissionId => new RolePermission { RoleId = roleId, PermissonId = permissionId }).ToList();
            await _context.RolePermisions.AddRangeAsync(rolePermissions);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetRolePermissionsAsync(List<int> roleIds)
        {
            var permissionIds = new List<int>();

            foreach (int roleId in roleIds)
            {
                var permissions = await _context.RolePermisions
                    .Where(r => r.RoleId == roleId)
                    .Select(r => r.PermissonId)
                    .ToListAsync();

                permissionIds.AddRange(permissions);
            }

            return permissionIds;
        }

        public async Task UpdatePermissionsAsync(int roleId, List<int> permissions)
        {
            var existingPermissions = await _context.RolePermisions.Where(p => p.RoleId == roleId).ToListAsync();
            _context.RolePermisions.RemoveRange(existingPermissions);
            await AddPermissionsAsync(roleId, permissions);
        }

        public async Task<bool> CheckPermissionAsync(int permissionId, string userName)
        {
            var userHasPermission = await _context.Users
                .Where(u => u.UserName == userName)
                .SelectMany(u => u.UserRoles)
                .SelectMany(ur => ur.Role.RolePermisions)
                .AnyAsync(rp => rp.PermissonId == permissionId);

            return userHasPermission;
        }



        public async Task<List<string>> GetUserRolesAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();
        }

        public async Task<List<string>> GetUserPermissionsAsync(Guid userId)
        {
            var roles = await GetUserRolesAsync(userId);

            return await _context.RolePermisions
                .Include(rp => rp.Role)
                .Include(rp => rp.Permisson)
                .Where(rp => roles.Contains(rp.Role.RoleName))
                .Select(rp => rp.Permisson.PermissonTitle)
                .ToListAsync();
        }
    }
}
