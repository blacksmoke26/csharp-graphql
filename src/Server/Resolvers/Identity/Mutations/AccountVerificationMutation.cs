// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Abstraction.Inputs.Identity;
using Application.Domain.Validators.Identity;

namespace Server.Resolvers.Identity.Mutations;

[MutationType]
public static class AccountVerificationMutation {
  [GraphQLDescription("Completes the pending account verification")]
  public static async Task<bool> AccountVerification(
    [UseFluentValidation, UseValidator<AccountVerificationInputValidator>]
    AccountVerificationInput input,
    UserRepository userRepo,
    UserService userService, CancellationToken token
  ) {
    var user = await userRepo.GetByEmailAndActivationCodeAsync(input.Email, input.Code, token);

    ErrorHelper.ThrowIfNull(user,
      "Email address or activation code not found");

    ErrorHelper.ThrowIfFalse(await userService.VerifyAccount(user, token),
      "Process failed due to the unknown error");

    return true;
  }
}