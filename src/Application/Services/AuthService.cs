// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql
// See: https://www.telerik.com/blogs/asp-net-core-basics-authentication-authorization-jwt

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Abstraction.Payloads.Auth;
using Application.Config;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService(JwtConfiguration config) : ServiceBase {
  /// <summary>Generates JWT token against the given user</summary>
  /// <param name="user">The user model instance</param>
  /// <param name="options">Additional options to customize the token</param>
  /// <returns>The generated Json Web Token</returns>
  public AuthTokenResult GenerateToken(User user, JwtOptions? options = null) {
    JwtSecurityTokenHandler tokenHandler = new();

    ArgumentNullException.ThrowIfNull(config.Key);
    SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(config.Key));

    var issuedAt = DateTime.UtcNow;
    var expires = DateTime.UtcNow.AddHours(options?.ExpirationInHours ?? config.ExpirationInHours);

    SecurityTokenDescriptor tokenDescriptor = new() {
      Subject = GenerateClaims(user, options),
      Expires = expires,
      Issuer = options?.Issuer ?? config.Issuer,
      IssuedAt = issuedAt,
      Audience = options?.Audience ?? config.Audience,
      SigningCredentials = new(
        securityKey, SecurityAlgorithms.HmacSha384Signature
      )
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);

    return new() {
      Token = tokenHandler.WriteToken(token),
      IssuedAt = issuedAt,
      Expires = expires,
    };
  }

  /// <summary>Generate the token claims</summary>
  /// <param name="user">The user</param>
  /// <param name="options">Additional options to customize the token</param>
  /// <returns>The generated claims list</returns>
  private static ClaimsIdentity GenerateClaims(User user, JwtOptions? options = null) {
    ClaimsIdentity claims = new();

    claims.AddClaims([
      new Claim(JwtRegisteredClaimNames.Jti, user.AuthKey),
      new Claim(JwtRegisteredClaimNames.Sub, "auth"),
      new Claim(ClaimTypes.Role, user.Role),
    ]);

    if (options is null) return claims;
    
    foreach (var claim in options.Value.Claims)
      claims.AddClaim(claim);

    return claims;
  }
}