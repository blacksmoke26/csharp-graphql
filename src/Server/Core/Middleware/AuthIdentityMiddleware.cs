// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-webapp
// See: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/extensibility

namespace Server.Core.Middleware;

/// <summary>
/// Server middleware to validate against auth-key, fetch user and set as current identity upon verified
/// </summary>
/// <param name="next">The middleware delegate</param>
public class AuthValidationMiddleware(
  RequestDelegate next
) {
  public async Task InvokeAsync(HttpContext context, IdentityService idService) {
    context.GetIdentity().Clear();

    var authKey = context.GetAuthKey();
    var role = context.GetRole();

    if (string.IsNullOrWhiteSpace(role) || string.IsNullOrWhiteSpace(authKey)) {
      // Skipping anonymous request
      await next(context);
      return;
    }

    // Fetch and validate user against claims
    var user = await idService.LoginWithClaimAsync(new() {
      Jti = authKey,
      Role = role
    }, new() {
      IpAddress = context.Connection.RemoteIpAddress?.ToString(),
    });

    if (user is null) {
      throw new GraphQLException("Authenticate failed due to the unknown reason");
    }

    // Set user as authenticated identity
    context.GetIdentity().SetIdentity(user!);

    await next(context);
  }
}