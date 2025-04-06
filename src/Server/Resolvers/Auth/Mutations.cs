// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Validators.Auth;

namespace Server.Resolvers.Auth;

[MutationType]
public static class Mutations {
  [GraphQLDescription("Account authentication using credentials")]
  public static AuthLoginPayload Login(
    [UseFluentValidation, UseValidator<LoginCredentialInputValidator>]
    LoginCredentialInput input) {
    throw new GraphQLException("Not implemented yet");
  }
}