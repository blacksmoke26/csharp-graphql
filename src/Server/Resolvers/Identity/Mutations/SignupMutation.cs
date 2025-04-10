// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

using Abstraction.Inputs.Identity;
using Application.Domain.Validators.Identity;

namespace Server.Resolvers.Identity.Mutations;

[MutationType]
public static class SignupMutation {
  [GraphQLDescription("Creates a new user account")]
  public static async Task<bool> Signup(
    [UseFluentValidation, UseValidator<SignupInputValidator>]
    SignupInput input, UserService userService, CancellationToken token
  ) {
    var user = await userService.CreateUserAsync(input, token);

    ErrorHelper.ThrowIfNull(user, "An error occurred while creation the account.");

    return true;
  }
}