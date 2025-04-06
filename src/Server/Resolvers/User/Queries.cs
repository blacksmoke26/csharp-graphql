// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Server.Resolvers.User;

[QueryType]
public static class Queries {
  public static string Me() {
    throw new GraphQLException("Not implemented");
  }
}