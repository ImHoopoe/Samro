using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WinWin.DataLayer.Entities.Roles;

public class JwtService
{
    private readonly IConfiguration _config;
    private readonly byte[] _key;
    private readonly int _expiryMinutes;
    private readonly string? _issuer;
    private readonly string? _audience;

    public JwtService(IConfiguration config)
    {
        _config = config;
        _key = Convert.FromBase64String(_config["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey"));
        if (_key.Length != 32)
        {
            throw new CryptographicException($"طول کلید باید 256 بیت (32 بایت) باشد، اما طول فعلی کلید {_key.Length * 8} بیت است.");
        }

        _expiryMinutes = int.Parse(_config["JwtSettings:ExpiryMinutes"] ?? "60");
        _issuer = _config["JwtSettings:Issuer"];
        _audience = _config["JwtSettings:Audience"];
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var symmetricKey = new SymmetricSecurityKey(_key)
        {
            KeyId = "e2f3b8b4-25e6-4d57-8c56-0f2b0d9e63f7"
        };

        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expiryMinutes),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        token.Header["kid"] = symmetricKey.KeyId;

        return tokenHandler.WriteToken(token);
    }
}