﻿// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

using Abstraction.Inputs.Identity;
using Application.Domain.Validators.Identity;

namespace Server.Resolvers.Account.Mutations;

[MutationType]
public static class RequestResetPasswordMutation {
  [GraphQLDescription("Sends a request for resetting the account password")]
  public static async Task<bool> AccountRequestResetPassword(
    [UseFluentValidation, UseValidator<RequestResetPasswordInputValidator>]
    RequestResetPasswordInput input,
    UserRepository userRepo,
    UserService userService, CancellationToken token
  ) {
    var user = await userRepo.GetByEmailAsync(input.Email, token);

    ErrorHelper.ThrowIfNull(user,
      "The email address does not exist");

    ErrorHelper.ThrowIfTrue(user.Status == UserStatus.Inactive,
      "This account requires verification");

    ErrorHelper.ThrowIfTrue(user.Status != UserStatus.Active,
      "This account has been deleted or disabled by the admin");

    ErrorHelper.ThrowIfFalse(await userService.SendResetPasswordRequest(user, token),
      "Process failed due to the unknown error");

    return true;
  }
}