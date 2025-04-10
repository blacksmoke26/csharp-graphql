// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

using Abstraction.Inputs.Identity;
using Application.Domain.Validators.Identity;
using Database.Core.Extensions;
using Database.Repositories;

namespace Server.Resolvers.Identity.Mutations;

[MutationType]
public static class ResetPasswordMutation {
  [GraphQLDescription("Updates the account password using reset code")]
  public static async Task<bool> ResetPassword(
    [UseFluentValidation, UseValidator<ResetPasswordInputValidator>]
    ResetPasswordInput input,
    UserRepository userRepo,
    UserService userService, CancellationToken token
  ) {
    var user = await userRepo.GetByEmailAndResetCodeAsync(input.Email, input.ResetCode, token);

    ErrorHelper.ThrowIfNull(user,
      "Email address or reset code not found");

    ErrorHelper.ThrowIfTrue(user.IsPasswordResetCodeExpired(),
      "The reset code has been expired");

    ErrorHelper.ThrowIfFalse(await userService.ResetPassword(user, input.NewPassword, token),
      "Process failed due to the unknown error");

    return true;
  }
}