// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Application.Constants;
using Application.Services;

namespace Server.Resolvers.Auth;

[MutationType]
public static class LogoutMutation {
  [GraphQLDescription("Logouts the user and invalidate the token")]
  [Authorize(Policy = AuthPolicies.AuthPolicy)]
  public static async Task<bool> Logout(
    IdentityService idService,
    IHttpContextAccessor httpContext, CancellationToken token
  ) {
    var user = httpContext.HttpContext!.GetIdentity().User;
    return await idService.LogoutAsync(user, token);
  }
}