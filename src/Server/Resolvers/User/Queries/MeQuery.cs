// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Account;

namespace Server.Resolvers.User.Queries;

[QueryType]
public static class MeQuery {
  [GraphQLDescription("Fetches the authenticated account information")]
  [Authorize(Policy = AuthPolicy.Trusted)]
  public static Task<MePayload> Me(IHttpContextAccessor httpContext) {
    var user = httpContext.HttpContext!.GetUser();

    return Task.FromResult(new MePayload {
      Id = user.Id,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email,
      Role = user.ToRoleType(),
      Status = user.Status,
      CreatedAt = user.CreatedAt
    });
  }
}