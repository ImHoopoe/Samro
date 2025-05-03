using Microsoft.AspNetCore.Identity;
using System;
using WinWin.DataLayer.Entities.Roles;

namespace WinWin.Core.Tools.Account
{
    public class AccountTools
    {
        private readonly PasswordHasher<User> _passwordHasher;
        public AccountTools(PasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }
        public static Guid GenerateNewId() => Guid.NewGuid();
        public string HashPassword(User user, string password)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("رمز عبور نمی تواند خالی باشد", nameof(password));

            return _passwordHasher.HashPassword(user, password);
        }
        public bool VerifyPassword(User user, string hashedPassword, string passwordToCheck)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("رمز عبور نمی تواند خالی باشد", nameof(hashedPassword));

            if (string.IsNullOrEmpty(passwordToCheck))
                throw new ArgumentException("رمز عبور نمی تواند خالی باشد", nameof(passwordToCheck));

            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, passwordToCheck);

            return result == PasswordVerificationResult.Success;
        }
    }
}