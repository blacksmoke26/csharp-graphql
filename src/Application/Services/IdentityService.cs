// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Auth;
using HotChocolate;

namespace Application.Services;

public class IdentityService(
  UserRepository userRepository
) : ServiceBase {
  /// <summary>HTTPContext items identity key name</summary>
  public const string IdentityKey = "%UserIdentity%";

  /// <summary>Logins the user by email and password</summary>
  /// <param name="input">The user DTO object</param>
  /// <param name="options">Additional login options</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The created user / null when failed</returns>
  public async Task<User?> LoginAsync(
    LoginCredentialInput input, LoginOptions? options = null,
    CancellationToken token = default
  ) {
    var user = await userRepository.GetByEmailAsync(input.Email, token);

    if (user is null || !user.ValidatePassword(input.Password))
      throw new GraphQLException("Incorrect email address or password");

    if (user.Status != UserStatus.Active)
      ThrowBadStatusException(user.Status);

    user.OnLogin();
    await userRepository.UpdateAsync(user, token);

    return user;
  }

  /// <summary>User account status verification excluding "active"</summary>
  /// <param name="status">The status to verify</param>
  /// <exception cref="GraphQLException">Throws when abnormal status detected</exception>
  private static void ThrowBadStatusException(UserStatus status) {
    if (status == UserStatus.Inactive)
      throw new GraphQLException("Cannot signed in due to the pending account verification.");

    throw new GraphQLException($"Your account has been ${status.ToString().ToLower()}");
  }

  /// <summary>Login the user as current identity against given JWT claims</summary>
  /// <param name="payload">The login claims object</param>
  /// <param name="options">Additional login options</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The created user / null when failed</returns>
  public async Task<User?> LoginWithClaimAsync(
    UserLoginClaimPayload payload, LoginOptions? options = null, CancellationToken token = default
  ) {
    var user = await userRepository.GetByAuthKeyAsync(payload.Jti, token);

    // The missing user, which means the auth-key is no longer valid
    if (user is null) {
      throw new GraphQLException("Token may invalidated or user not found");
    }

    // Either a user is unverified or insufficient status
    if (user.Status != UserStatus.Active)
      ThrowBadStatusException(user.Status);

    // Forcefully logged-out user globally
    if (user.Metadata.Security.TokenInvalidate) {
      throw new GraphQLException("The token is either disabled or revoked. Please sign in again.");
    }

    // Role mismatched. Tempered JWT "role" claim?
    if (user.Role != payload.Role) {
      throw new GraphQLException("Ineligible authorization role");
    }

    user.OnLogin(options?.IpAddress ?? null);
    await userRepository.UpdateAsync(user, token);

    return user;
  }

  /// <summary>Creates a user</summary>
  /// <param name="user">The user instance</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The created user / null when failed</returns>
  public async Task<bool> LogoutAsync(User user, CancellationToken token = default) {
    user.OnLogout();
    return await userRepository.UpdateAsync(user, token) > 0;
  }
}