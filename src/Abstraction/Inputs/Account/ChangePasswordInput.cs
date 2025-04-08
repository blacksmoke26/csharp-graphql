// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Abstraction.Inputs.Account;

[GraphQLDescription("Use to change the account password")]
[InputObjectType]
public struct ChangePasswordInput {
  [GraphQLDescription("The current password")] [property: Range(8, 20)]
  public string CurrentPassword { get; init; }

  [GraphQLDescription("The new password to update")] [property: Range(8, 20)]
  public string NewPassword { get; init; }
}