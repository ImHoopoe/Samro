using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.Core.Interfaces.RoleInterfaces;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Core.Services.RoleServices
{
    public class RoleServices : IRole
    {
        private readonly SamroContext _Context;
        public RoleServices(SamroContext context)
        {
            _Context = context; 
        }
        #region CRUD Opration
        public async Task<bool> CreateRole(Role role)
        {
            try
            {
                _Context.Roles.Add(role);
                await _Context.SaveChangesAsync();
                return  true;
            }
            catch 
            {

                return false;
            }
        }

        public async Task<bool> DeleteRole(Guid roleId)
        {
            try
            {
                _Context.Remove(GetRoleByIdAsync(roleId));
                await _Context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<bool> EditRole(Role role)
        {
            try
            {
                _Context.Update(role);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch
            {

                return false;
            }
        }

        public IEnumerable<Role> GetAllRolesAsync()
        {
            return _Context.Roles;
        }

        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
          return  _Context.Roles.Find(roleId);
        }
        #endregion
        #region User

        public async Task<IEnumerable<Role>> GetUserRoles(Guid userId)
        {
            var user = await _Context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            return user?.UserRoles.Select(ur => ur.Role) ?? Enumerable.Empty<Role>();
        }


        public async Task<bool> RemoveRoleFromUser(Guid userId, Guid roleId)
        {
            try
            {
                IEnumerable<Role> roles = await GetUserRoles(userId);
                foreach (var u in roles.ToList()) 
                {
                    _Context.Remove(u);
                    await _Context.SaveChangesAsync();
                }
                return true;
            }
            catch 
            {

                return false;
            }           
        }
        #endregion
    }
}
