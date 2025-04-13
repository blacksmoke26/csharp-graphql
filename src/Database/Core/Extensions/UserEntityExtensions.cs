// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Account;

namespace Database.Core.Extensions;

/// The class represents the extensions methods for 'User' entity
public static class UserEntityExtensions {
  /// <summary>
  /// Encrypt the password and also generate the password hash
  /// </summary>
  /// <param name="user">The User model</param>
  /// <param name="password">The password to set</param>
  public static void SetPassword(this User user, string password) {
    var result = IdentityHelper.EncryptPassword(password);
    user.Password = result.Password;
    user.PasswordHash = result.PasswordHash;
  }

  /// <summary>
  /// Sets the token invalidation state
  /// </summary>
  /// <param name="user">The User model</param>
  /// <param name="state">The invalidate state to set</param>
  public static void SetTokenInvalidateState(this User user, bool state) =>
    user.Metadata.Security.TokenInvalidate = state;

  /// <summary>
  /// Validates the given password against the existing password
  /// </summary>
  /// <param name="user">The User model</param>
  /// <param name="password">The password to verify</param>
  /// <returns>True if password is correct, false otherwise</returns>
  public static bool ValidatePassword(this User user, string password) =>
    IdentityHelper.ValidatePassword(new EncryptedPasswordResult {
      Password = password,
      PasswordHash = user.PasswordHash,
    });

  /// <summary>
  /// Generates token authentication key
  /// </summary>
  /// <param name="user">The User model</param>
  public static void GenerateAuthKey(this User user)
    => user.AuthKey = IdentityHelper.GenerateAuthKey();

  /// <summary>
  /// Validates the given auth key
  /// </summary>
  /// <param name="user">The User model</param>
  /// <param name="authKey">The key to validate</param>
  /// <returns>True when the key is valid, False otherwise</returns>
  public static bool ValidateAuthKey(this User user, string authKey) => user.AuthKey == authKey;

  /// <summary>An event method which invoked when account successfully created</summary>
  /// <param name="user">The user entity</param>
  public static void OnSignedUp(this User user) {
    user.Metadata.Activation.Pending = true;
    user.Metadata.Activation.Code = IdentityHelper.GenerateActivationCode();
    user.Metadata.Activation.RequestedAt = DateTime.UtcNow;
  }

  /// <summary>An event method which invoked when account successfully activated/verified</summary>
  /// <param name="user">The user entity</param>
  public static void OnActivated(this User user) {
    user.Metadata.Activation.Pending = false;
    user.Metadata.Activation.Code = null;
    user.Metadata.Activation.CompletedAt = DateTime.UtcNow;
  }

  /// <summary>An event method which invoked upon successful authentication</summary>
  /// <param name="user">The user entity</param>
  /// <param name="ipAddress">The current IP address</param>
  public static void OnLogin(this User user, string? ipAddress = null) {
    user.Metadata.LoggedInHistory.LastIp = ipAddress;
    user.Metadata.LoggedInHistory.LastDate = DateTime.UtcNow;
    user.Metadata.LoggedInHistory.FailedCount = 0;
    user.Metadata.LoggedInHistory.SuccessCount += 1;
    user.SetTokenInvalidateState(false);
  }

  /// <summary>An event method which invoked upon logout</summary>
  /// <param name="user">The user entity</param>
  public static void OnLogout(this User user) {
    user.SetTokenInvalidateState(true);
  }

  /// <summary>An event method which invoked upon password changing</summary>
  /// <param name="user">The user entity</param>
  public static void OnPasswordUpdate(this User user) {
    user.Metadata.Password.UpdatedCount += 1;
    user.Metadata.Password.LastUpdatedAt = DateTime.UtcNow;
  }

  /// <summary>An event method which invoked upon requesting for a password reset</summary>
  /// <param name="user">The user entity</param>
  public static void OnPasswordResetRequest(this User user) {
    user.Metadata.Password.ResetCode = IdentityHelper.GeneratePasswordResetCode();
    user.Metadata.Password.ResetCodeRequestedAt = DateTime.UtcNow;
  }

  /// <summary>An event method which invoked upon resetting the password</summary>
  /// <param name="user">The user entity</param>
  public static void OnPasswordReset(this User user) {
    user.Metadata.Password.ResetCode = null;
    user.Metadata.Password.ResetCodeRequestedAt = null;
    user.Metadata.Password.ResetCount += 1;
    user.Metadata.Password.LastResetAt = DateTime.UtcNow;
  }

  /// <summary>Verifies the reset code is expired or not</summary>
  /// <param name="user">The user entity</param>
  public static bool IsPasswordResetCodeExpired(this User user) {
    if (user.Metadata.Password.ResetCodeRequestedAt is null) return false;
    return user.Metadata.Password.ResetCodeRequestedAt.Value
      .ToUniversalTime()
      .AddDays(3d)
      .CompareTo(DateTime.UtcNow) < 0;
  }

  public static string? GetFullName(this User? user) {
    return user?.FirstName is null ? null : string.Concat(user.FirstName, " ", user.LastName);
  }

  public static IQueryable<MePayload> SelectMePayload(this IQueryable<User> source) {
    return source.Select(user => new MePayload {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email,
      Role = Enum.Parse<RoleType>(user.Role),
      Status = user.Status.ToString(),
      CreatedAt = user.CreatedAt
    });
  }

  public static IQueryable<UserPayload> SelectUserPayload(this IQueryable<User> source) {
    return source.Select(user => new UserPayload {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
    });
  }
}