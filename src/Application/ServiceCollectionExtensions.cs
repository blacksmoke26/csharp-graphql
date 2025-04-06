// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions {
  /// <summary>
  /// Register the abstractions, objects, validators and other classes
  /// </summary>
  /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> instance</param>
  /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></returns>
  public static IServiceCollection AddApplication(this IServiceCollection services) {
    return services;
  }
}