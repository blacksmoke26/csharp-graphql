// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

namespace Abstraction.Inputs.Identity;

[GraphQLDescription("Use to send a request for a password reset")]
public struct RequestResetPasswordInput {
  [GraphQLDescription("The email address")]
  public string Email { get; init; }
}