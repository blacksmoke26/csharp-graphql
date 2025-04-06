// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Abstraction;
using Application;
using Server.Core.Configurators;

namespace Server.Core.Extensions;

public static class ApplicationBuilderExtensions {
  /// <summary>Registers application level services</summary>
  /// <param name="services">ServiceCollection instance</param>
  /// <param name="configuration">Application configuration</param>
  public static IServiceCollection InitBootstrapper(this IServiceCollection services, IConfiguration configuration) {
    services.AddAbstractions();
    services.AddApplication();

    DatabaseConfigurator.Configure(services, configuration);
    GraphqlConfigurator.Configure(services, configuration);

    return services;
  }
}