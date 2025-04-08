// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Abstraction.Inputs.Auth;

[GraphQLDescription("Use to login the existing user with credentials")]
[InputObjectType]
public struct LoginCredentialInput {
  [GraphQLDescription("The email address")]
  public string Email { get; init; }

  [GraphQLDescription("The password")]
  public string Password { get; init; }
}

public struct UserLoginClaimPayload {
  public string Jti { get; init; }
  public string Role { get; init; }
}