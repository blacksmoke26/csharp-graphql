// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Abstraction.Payloads.Auth;

[GraphQLDescription("This response class contains the information regarding user and authorization")]

public struct AuthLoginPayload {
  [JsonPropertyName("auth"), GraphQLDescription("The authorization details")]
  public AuthTokenResult Auth { get; init; }

  [JsonPropertyName("user"), GraphQLDescription("The user account details")]
  public UserAuthInfo User { get; init; }
}

[GraphQLDescription("This object represents the authorization information")]
[GraphQLName("AuthTokenPayload")]
public struct AuthTokenResult {
  [JsonPropertyName("token"), GraphQLDescription("The JWT authorization token")]
  public string Token { get; init; }

  [JsonPropertyName("issuedAt"),
   GraphQLDescription("The token issued timestamp")]
  public DateTime IssuedAt { get; init; }

  [JsonPropertyName("expires"),
   GraphQLDescription("The token expiration timestamp")]
  public DateTime Expires { get; init; }
}

[GraphQLDescription("This object represents the user's authorization information")]
public struct UserAuthInfo {
  [JsonPropertyName("fullname"), GraphQLDescription("The user's first and last name")]
  public string Fullname { get; init; }

  [JsonPropertyName("firstName"), GraphQLDescription("The user's first name")]
  public string FirstName { get; init; }

  [JsonPropertyName("lastName"), GraphQLDescription("The user's last name")]
  public string LastName { get; init; }

  [JsonPropertyName("email"), GraphQLDescription("The email address")]
  public string Email { get; init; }

  [JsonPropertyName("role"), GraphQLDescription("The role name")]
  public string Role { get; init; }
}