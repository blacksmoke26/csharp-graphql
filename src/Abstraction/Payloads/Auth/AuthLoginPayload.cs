// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Account;

namespace Abstraction.Payloads.Auth;

[GraphQLDescription("This response class contains the information regarding user and authorization")]
[GraphQLName("LoginPayload")]
public struct AuthLoginPayload {
  [JsonPropertyName("auth"), GraphQLDescription("The authorization details")]
  public AuthTokenResult Auth { get; init; }

  [JsonPropertyName("user"), GraphQLDescription("The user account details")]
  public MePayload User { get; init; }
}

[GraphQLDescription("This object represents the authorization information")]
[GraphQLName("TokenPayload")]
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