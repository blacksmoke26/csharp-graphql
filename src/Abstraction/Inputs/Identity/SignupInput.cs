// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

namespace Abstraction.Inputs.Identity;

[GraphQLDescription("Use to signup a new user account")]
public struct SignupInput {
  [GraphQLDescription("The first name of user")]
  public string FirstName { get; init; }

  [GraphQLDescription("The last name of user")]
  public string LastName { get; init; }

  [GraphQLDescription("The email address")]
  public string Email { get; init; }

  [GraphQLDescription("Password with alphanumeric chars along with symbols")]
  public string Password { get; init; }
}