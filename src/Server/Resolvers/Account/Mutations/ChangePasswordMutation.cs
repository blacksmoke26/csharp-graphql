// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Account;
using Application.Domain.Validators.Account;

namespace Server.Resolvers.Account.Mutations;

[MutationType]
public static class AccountChangePasswordMutation {
  [GraphQLDescription("Updates the account password")]
  [Authorize(Policy = AuthPolicy.Trusted)]
  public static async Task<bool> AccountChangePassword(
    [UseFluentValidation, UseValidator<ChangePasswordInputValidator>]
    ChangePasswordInput input, UserService userService,
    IHttpContextAccessor httpContext, CancellationToken token) {
    var isChanged = await userService.ChangePasswordAsync(
      httpContext.HttpContext!.GetUser(), input.NewPassword, token);

    if (!isChanged) {
      throw new GraphQLException("Failed to change the password");
    }

    return true;
  }
}