﻿// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

namespace Server.Resolvers;

[QueryType]
public static class Query {
  public static Task<string> HealthCheck() {
    return Task.FromResult("OK");
  }
}