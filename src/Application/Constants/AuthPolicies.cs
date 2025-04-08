using Application.Services;

namespace Application.Constants;

/// <summary>
/// AuthPolicies represents the JWT role policies
/// <p>Check <see cref="AuthService"/> class for usage.</p>
/// </summary>
public static class AuthPolicies {
  /// <summary>Applicable to `Admin` role</summary>
  public const string AdminPolicy = "AdminPolicy";

  /// <summary>Applicable to `User` role</summary>
  public const string UserPolicy = "UserPolicy";

  /// <summary>Applicable to signed-in users</summary>
  public const string AuthPolicy = "AuthPolicy";
}