using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WinWin.DataLayer.Entities.Roles;
namespace WinWin.Core.Interfaces.RoleInterfaces
{
    public interface IRole
    {
        #region CRUD Operations
        Task<bool> CreateRole(Role role);
        //ممد ریدی با این اسم گذاریت ! : Creat????? خودم اصلاحش کردم
        Task<bool> EditRole(Role role);
        Task<Role?> GetRoleByIdAsync(Guid roleId);
        Task<List<Role>> GetAllRolesAsync();
        Task<bool> DeleteRole(Guid roleId);
        #endregion
        #region User
        Task<bool> RemoveRoleFromUser(Guid userId, Guid roleId);
        Task<IEnumerable<Role>> GetUserRoles(Guid userId);
        #endregion


    }
}
