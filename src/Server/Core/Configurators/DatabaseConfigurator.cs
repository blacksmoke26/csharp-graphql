// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Database;
using Server.Core.Interfaces;

namespace Server.Core.Configurators;

public abstract class DatabaseConfigurator : IServiceConfigurator {
  /// <inheritdoc/>
  public static void Configure(IServiceCollection services, IConfiguration config) {
    services.AddDatabase(config.GetRequiredSection("Database").Get<DbConfiguration>());
  }
}