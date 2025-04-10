// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Microsoft.Extensions.DependencyInjection;

namespace Abstraction;

public static class ServiceCollectionExtensions {
  /// <summary>
  /// Register the abstractions, objects, validators and other classes
  /// </summary>
  /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> instance</param>
  /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></returns>
  public static IServiceCollection AddAbstractions(this IServiceCollection services) {
    services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Transient);
    return services;
  }
}