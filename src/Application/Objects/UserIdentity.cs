// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-webapp

using System.Security.Principal;

namespace Application.Objects;

/// <summary>
/// AuthUserIdentity store the authenticated user instance throughout the application
/// </summary>
///
/// <example>
/// To access the current identity, Please check the following example:
/// <code>
/// Authorize(AuthPolicies.AuthPolicy)]
/// [HttpGet(ApiEndpoints.Identity.Me)]
/// public async Task Me(UserIdentity identity) {
///  // implement logic here
/// }
/// </code> 
/// </example> 
public class UserIdentity : IIdentity {
  /// <summary>UserIdentity backed property</summary>
  private User? _user;

  /// <summary>
  /// Get the current authenticated user object
  /// <p>
  /// To void the exception,
  /// use <see cref="IsAuthenticated">IsAuthenticated</see> method to check if user is authenticated or not.
  /// </p>
  /// </summary>
  /// <exception cref="FluentValidation.ValidationException">If user is not authenticated</exception>
  public User User {
    get =>
      _user ?? throw new Exception("Unauthenticated user");
    private set => _user = value;
  }

  /// <inheritdoc/>
  public string AuthenticationType => "Token";

  /// <summary>
  /// Checks whatever the user is authenticated or not
  /// </summary>
  /// <returns>True when authenticated, otherwise False</returns>
  public bool IsAuthenticated => _user is not null;

  /// <inheritdoc/>
  public string? Name => _user?.FullName ?? null;

  /// <summary>
  /// Checks whatever the authenticated user is admin user or not
  /// <p>
  /// To void the exception,
  /// use <see cref="IsAuthenticated">IsAuthenticated</see> method to check if user is authenticated or not.
  /// </p>
  /// </summary>
  /// <exception cref="FluentValidation.ValidationException">If user is not authenticated</exception>
  /// <returns>True if a user is admin user, otherwise False</returns>
  public bool IsAdminUser => User.Role == UserRole.Admin;

  /// <summary>
  /// Returns the authenticated user primary key
  /// <p>
  /// To void the exception,
  /// use <see cref="IsAuthenticated"></see> method to check if user is authenticated or not.
  /// </p>
  /// </summary>
  /// <exception cref="FluentValidation.ValidationException">If user is not authenticated</exception>
  /// <returns>The user primary key</returns>
  public long GetId() => User.Id;

  /// <summary>
  /// Compare the authenticated user id with the given one and verify that
  /// both are the same.
  /// </summary>
  /// <param name="userId">User ID to compare</param>
  /// <param name="includeAdmin">Treat admin as the owner, false to compare ID by ID</param>
  /// <returns>True upon successfully matched, otherwise false</returns>
  public bool CheckSameId(long? userId, bool includeAdmin = false) {
    return IsAuthenticated && (includeAdmin && IsAdminUser || GetId() == userId);
  }

  /// <summary>
  /// Compare the authenticated user role with the given one
  /// <p>
  /// To void the exception,
  /// use <see cref="IsAuthenticated">IsAuthenticated</see> method to check if user is authenticated or not.
  /// </p>
  /// </summary>
  /// <exception cref="FluentValidation.ValidationException">If user is not authenticated</exception>
  /// <param name="role">Role to compare</param>
  /// <returns>True if a user role matched, otherwise False</returns>
  public bool CheckSameRole(string role) => User.Role == role;

  /// <summary>
  /// Sets user as current identity
  /// </summary>
  /// <param name="user">The user instance</param>
  public void SetIdentity(User user) => User = user;

  /// <summary>
  /// Clears the current identity
  /// </summary>
  public void Clear() => User = null!;
}