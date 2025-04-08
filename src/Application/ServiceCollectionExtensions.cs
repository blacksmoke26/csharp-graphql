// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions {
  /// <summary>
  /// Register the abstractions, objects, validators and other classes
  /// </summary>
  /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> instance</param>
  /// <returns>The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></returns>
  public static IServiceCollection AddApplication(this IServiceCollection services) {
    // services
    services.AddScoped<IdentityService>();
    services.AddScoped<GenreService>();
    services.AddScoped<MovieService>();
    services.AddScoped<RatingService>();
    services.AddScoped<UserService>();
    
    return services;
  }
}