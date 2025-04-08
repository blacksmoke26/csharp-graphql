// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Payloads.Account;

namespace Server.Resolvers.Account.Queries;

[QueryType]
[Authorize(Policy = AuthPolicies.AuthPolicy)]
public static class MeQuery {
  public static Task<MePayload> Me(IHttpContextAccessor httpContext) {
    var user = httpContext.HttpContext!.GetUser();
    
    return Task.FromResult(new MePayload {
      Id = user.Id,
      Fullname = user.FullName,
      FirstName = user.FirstName,
      LastName = user.LastName,
      Email = user.Email,
      Role = user.Role,
      Status = user.Status.ToString(),
      CreatedAt = user.CreatedAt
    });
  }
}