// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class ServiceCollectionExtensions {
  /// <summary>
  /// Registers application level services 
  /// </summary>
  /// <param name="services">ServiceCollection instance</param>
  /// <param name="config">The database configuration</param>
  /// <returns>The updated service collection instance</returns>
  public static IServiceCollection AddDatabase(this IServiceCollection services, DbConfiguration config) {
    services.AddSingleton<ApplicationDbContext>(_ => {
      ApplicationDbContext instance = new(config);
      instance.Database.ExecuteSql($"SET timezone = '+00:00'");
      return instance;
    });

    // repos
    services.AddSingleton<GenreRepository>();
    services.AddSingleton<MovieRepository>();
    services.AddSingleton<RatingRepository>();
    services.AddSingleton<UserRepository>();

    return services;
  }
}