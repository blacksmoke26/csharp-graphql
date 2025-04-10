﻿// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Application.Domain.Validators.Auth;

namespace Server.Resolvers.Auth.Mutations;

[MutationType]
public static class LoginMutation {
  [GraphQLDescription("Account authentication using credentials")]
  public static async Task<AuthLoginPayload?> AuthLogin(
    [UseFluentValidation, UseValidator<LoginCredentialInputValidator>]
    LoginCredentialInput input,
    IdentityService idService, AuthService authService,
    IHttpContextAccessor httpContext, CancellationToken token
  ) {
    var user = await idService.LoginAsync(input, new LoginOptions {
      IpAddress = httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString(),
    }, token);

    if (user is null) {
      throw new GraphQLException("Authenticate failed due to the unknown reason");
    }

    return new AuthLoginPayload {
      Auth = authService.GenerateToken(user),
      User = new UserAuthInfo {
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Role = user.Role,
        Fullname = $"{user.FirstName} {user.LastName}"
      }
    };
  }
}