using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Core.Interfaces
{
    public interface IUser
    {
        #region CRUD
        Task<bool> CreateUser(User user);
        Task<bool> EditUser(User user);
        IEnumerable<User> GetAllUser(int pageNumber, int pageSize);
        Task<User> GetUserById(Guid userId);
        Task<bool> DeleteUser(Guid userId);
        #endregion
        #region AccountServices
        Task<bool> IsRegistered(string identifier);
        Task<bool> IsActivated( string identifier);
        Task<User?> GetUserByIdentifier(string emailOrPhoneNumber);
        Task<bool> IsPasswordCorrect(string identifier, string Password);
        Task<int> GetTotalUsersCount();
        Task<User> GetUserByActivationCode(Guid activationCode);
        #endregion
        Task<User> GetUserByNationalId(string nationalId);
    }
}
