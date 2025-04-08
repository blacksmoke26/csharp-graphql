// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Application.Services;

public struct LoginOptions {
  /// <summary>The IP address</summary>
  public string? IpAddress { get; init; }
}

public class UserService(
  UserRepository userRepository) : ServiceBase {
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
    return await userRepository.UpdateAsync(user, token) > 0;
  }
}