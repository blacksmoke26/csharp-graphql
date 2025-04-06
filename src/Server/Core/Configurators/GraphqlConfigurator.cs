// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql
// https://eduardocp.github.io/hot-chocolate-fluent-validation

using Abstraction;
using Server.Core.Interfaces;

namespace Server.Core.Configurators;

public abstract class GraphqlConfigurator : IApplicationServiceConfigurator {
  /// <inheritdoc/>
  public static void Configure(IServiceCollection services, IConfiguration config) {
    services.AddGraphQLServer()
      .AddAbstractions()
      .AddProjections()
      .AddGraphServerTypes()
      .AddFluentValidation(o => o.UseDefaultErrorMapper())
      .InitializeOnStartup();
  }

  /// <inheritdoc/>
  public static void Use(WebApplication app) {
    app.MapGraphQL();
  }
}