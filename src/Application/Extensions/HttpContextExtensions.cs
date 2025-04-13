// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp
// See: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis

using System.Security.Claims;
using Application.Objects;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Application.Extensions;

public static class HttpContextExtensions {
  /// <summary>
  /// Checks that user is authenticated or not
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>Returns true when authenticated, false otherwise</returns>
  public static bool IsAuthenticated(this HttpContext context)
    => GetIdentity(context).IsAuthenticated;

  /// <summary>
  /// Returns the authentication key if user is authenticated
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The authenticated key, null otherwise</returns>
  public static string? GetAuthKey(this HttpContext context) {
    return context.User.Claims
      .SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
  }

  /// <summary>
  /// Returns the authentication user role if user is authenticated
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The user role, null otherwise</returns>
  public static string? GetRole(this HttpContext context) {
    return context.User.Claims
      .SingleOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
  }

  /// <summary>
  /// Returns true when the authentication user role is admin
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The user role, null otherwise</returns>
  public static bool IsAdminRole(this HttpContext context) {
    return context.GetRole() == UserRole.Admin;
  }

  /// <summary>
  /// Returns the authentication user primary key
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The user PK</returns>
  /// <exception cref="FluentValidation.ValidationException">If user is not logged in</exception>
  public static long GetId(this HttpContext context) => GetIdentity(context).GetId();

  /// <summary>
  /// Try to get the authentication user primary key if user is authenticated
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The user primary key, null if not authenticated</returns>
  /// <exception cref="FluentValidation.ValidationException">If user is not logged in</exception>
  public static long? GetIdOrNull(this HttpContext context)
    => IsAuthenticated(context) ? GetId(context) : null;

  /// <summary>
  /// Returns the current identity instance
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The user identity</returns>
  public static UserIdentity GetIdentity(this HttpContext context) {
    if (context.Items.TryGetValue(IdentityService.IdentityKey, out var identity))
      return (UserIdentity)identity!;

    UserIdentity instance = new();
    context.Items.Add(IdentityService.IdentityKey, instance);
    return instance;
  }

  /// <summary>
  /// Returns the authenticated identity user instance
  /// </summary>
  /// <param name="context">HttpContext instance</param>
  /// <returns>The user instance</returns>
  public static User GetUser(this HttpContext context) {
    return context.GetIdentity().User;
  }
}