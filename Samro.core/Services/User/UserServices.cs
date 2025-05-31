using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinWin.Core.Interfaces;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.Roles;
using WinWin.DataLayer.Entities;
using WinWin.DataLayer.DTOS;

namespace WinWin.Core.Services
{
    public class UserServices : IUser
    {
        private readonly SamroContext _context;
        private readonly AccountTools _accountTools;
        public UserServices(SamroContext context, AccountTools accountTools)
        {
            _context = context;
            _accountTools = accountTools;
        }

        #region CRUDSystem
        private async Task<bool> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving changes: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> CreateUser(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return false;
                }

                user.IsActivated = false;
                await SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> EditUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                return await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error editing user: {ex.Message}");
                return false;
            }
        }
        public IEnumerable<User> GetAllUser(int pageNumber, int pageSize)
        {
            return _context.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        public async Task<User> GetUserById(Guid userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error retrieving user: {ex.Message}");
                throw new ApplicationException("خطا در بازیابی کاربر");
            }
        }
        #endregion

        #region AccountServices
        public async Task<User?> GetUserByIdentifier(string identifier)
        {
            return await _context.Users
                .Where(u => u.Email == identifier || u.PhoneNumber == identifier || u.UserName == identifier)
                .Select(u => new User
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsActivated = u.IsActivated,
                    Password = u.Password
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> IsRegistered(string identifier)
        {
            return await _context.Users.AnyAsync(u => u.Email == identifier || u.PhoneNumber == identifier);
        }
        public async Task<bool> IsActivated(string identifier)
        {
            var user = await GetUserByIdentifier(identifier);
            return user?.IsActivated ?? false;
        }
        public async Task<int> GetTotalUsersCount()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<List<PlayerTournumentsViewModel>> GetUserTournuments(Guid userId)
        {
            var res = await _context.MatchUsers
                .Where(matchUser => matchUser.PlayerId == userId)
                .Select(matchUser => new PlayerTournumentsViewModel()
                {
                    CreatedByUserId = matchUser.Match.Tournament.CreatedByUserId,
                    IsDeleted = matchUser.Match.Tournament.IsDeleted,
                    MatchDate = matchUser.Match.MatchDate,
                    MatchLocation = matchUser.Match.Location,
                    Title = matchUser.Match.Tournament.Title,
                    TournamentType = matchUser.Match.Tournament.TournamentType
                })
                .ToListAsync();
            return res;
        }

        public async Task<bool> IsPasswordCorrect(string identifier, string password)
        {
            User user = await GetUserByIdentifier(identifier);

            if (user == null)
            {
                return false; 
            }

            var res = _accountTools.VerifyPassword(user, user.Password, password);

            return res; 
        }

        public async Task<User> GetUserByActivationCode(Guid activationCode)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ActivationCode == activationCode);
        }

        public async Task<string> GetUserByNationalId(string nationalId)
        {
            return await _context.Users
                .Where(u => u.NationalId == nationalId)
                .Select(u => $"{u.UserId}:{u.Name} {u.LastName}")
                .FirstOrDefaultAsync();
        }


        #endregion
    }
}
