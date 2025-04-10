// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

namespace Abstraction.Inputs.Identity;

[GraphQLDescription("Use to reset the account password")]
public struct ResetPasswordInput {
  [GraphQLDescription("The reset code")]
  public string ResetCode { get; init; }

  [GraphQLDescription("The email address")]
  public string Email { get; init; }

  [GraphQLDescription("The new password")]
  public string NewPassword { get; init; }
}