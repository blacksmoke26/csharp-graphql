// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Server.Resolvers.Auth.Mutations;

[MutationType]
public static class LogoutMutation {
  [GraphQLDescription("Logouts the user and invalidate the token")]
  [Authorize(Policy = AuthPolicies.AuthPolicy)]
  public static async Task<bool> AuthLogout(
    IdentityService idService,
    IHttpContextAccessor httpContext, CancellationToken token
  ) {
    var user = httpContext.HttpContext!.GetIdentity().User;
    return await idService.LogoutAsync(user, token);
  }
}