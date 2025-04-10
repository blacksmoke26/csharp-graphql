// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Identity;

namespace Application.Services;

public struct LoginOptions {
  /// <summary>The IP address</summary>
  public string? IpAddress { get; init; }
}

public class UserService(
  UserRepository userRepo) : ServiceBase {
  /// <summary>
  /// Change the account password
  /// </summary>
  /// <param name="user">The user object</param>
  /// <param name="newPassword">The password to change</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the password has changed or not</returns>
  public async Task<bool> ChangePasswordAsync(User user, string newPassword, CancellationToken token = default) {
    user.SetPassword(newPassword);
    user.OnPasswordUpdate();
    user.GenerateAuthKey();
    return await userRepo.UpdateAsync(user, token) > 0;
  }

  /// <summary>
  /// Creates a new user entity and write change on database
  /// </summary>
  /// <param name="dto">The user details</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The user instance if created successfully, Null upon failed</returns>
  public async Task<User?> CreateUserAsync(SignupInput dto, CancellationToken token = default) {
    var user = new User {
      Email = dto.Email.ToLower(),
      Password = dto.Password,
      FirstName = dto.FirstName,
      LastName = dto.LastName,
      Role = UserRole.User,
      Status = UserStatus.Inactive,
      Metadata = new()
    };

    user.SetPassword(dto.Password);
    user.OnSignedUp();

    return await userRepo.AddAsync(user, token) > 0 ? user : null;
  }

  /// <summary>
  /// Completes the pending account verification
  /// </summary>
  /// <param name="user">The user object</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the process was a success or failure</returns>
  public async Task<bool> VerifyAccount(User user, CancellationToken token = default) {
    user.Status = UserStatus.Active;
    user.OnActivated();
    return await userRepo.UpdateAsync(user, token) > 0;
  }

  /// <summary>
  /// Sends a request for a password reset
  /// </summary>
  /// <param name="user">The user object</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the process was a success or failure</returns>
  public async Task<bool> SendResetPasswordRequest(User user, CancellationToken token = default) {
    user.OnPasswordResetRequest();
    return await userRepo.UpdateAsync(user, token) > 0;
  }

  /// <summary>
  /// Changes the account password
  /// </summary>
  /// <param name="user">The user object</param>
  /// <param name="password">The password to set</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the process was a success or failure</returns>
  public async Task<bool> ResetPassword(User user, string password, CancellationToken token = default) {
    user.SetPassword(password);
    user.SetTokenInvalidateState(true);
    user.OnPasswordReset();
    return await userRepo.UpdateAsync(user, token) > 0;
  }
}