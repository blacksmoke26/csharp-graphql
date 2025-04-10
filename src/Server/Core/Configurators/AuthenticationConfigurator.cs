// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-webapp
// Guide: https://chillicream.com/docs/hotchocolate/v15/security/authentication

using System.Text;
using Application.Config;
using Database.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server.Core.Interfaces;

namespace Server.Core.Configurators;

public abstract class AuthenticationConfigurator : IApplicationServiceConfigurator {
  /// <summary>
  /// Configures the JWT authentication and authorization to the service collection
  /// </summary>
  /// <inheritdoc/>
  public static void Configure(IServiceCollection services, IConfiguration config) {
    var jwtConfig = config.GetRequiredSection("Jwt").Get<JwtConfiguration>();
    
    services.AddAuthentication(x => {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x => {
      x.TokenValidationParameters = new TokenValidationParameters() {
        IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.UTF8.GetBytes(jwtConfig.Key)
        ),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidAudience = jwtConfig.Audience,
        ValidateIssuer = true,
        ValidateAudience = true
      };
    });

    services.AddAuthorization(x => {
      // Policy Type: Admin
      x.AddPolicy(AuthPolicies.AdminPolicy, p
        => p.RequireRole(UserRole.Admin));

      // Policy Type: User
      x.AddPolicy(AuthPolicies.UserPolicy, p
        => p.RequireRole(UserRole.User));

      // Policy Type: Trusted
      x.AddPolicy(AuthPolicies.AuthPolicy, p
        => p.RequireRole(UserRole.Admin, UserRole.User));
    });

    services.AddSingleton<AuthService>(_ => new AuthService(jwtConfig));
  }

  /// <summary>
  /// Configures the authentication to the web application
  /// </summary>
  /// <inheritdoc/>
  public static void Use(WebApplication app) {
    app.UseAuthentication();
    app.UseAuthorization();
  }
}