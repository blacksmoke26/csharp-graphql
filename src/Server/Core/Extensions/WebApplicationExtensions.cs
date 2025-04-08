// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Database.Context;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Server.Core.Configurators;
using Server.Core.Middleware;

namespace Server.Core.Extensions;

public static class WebApplicationExtensions {
  /// <summary>Registers web application level services</summary>
  /// <param name="app">The <see cref="Microsoft.AspNetCore.Builder.WebApplication"/></param>
  public static void UseBootstrapper(this WebApplication app) {
    app.UseForwardedHeaders(new ForwardedHeadersOptions {
      ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    AuthenticationConfigurator.Use(app);
    GraphqlConfigurator.Use(app);
    
    app.UseMiddleware<AuthValidationMiddleware>();
  }

  /// <summary>Initialize the database</summary>
  /// <param name="app">The <see cref="Microsoft.AspNetCore.Builder.WebApplication"/></param>
  public static async Task InitializeDbAsync(this WebApplication app) {
    var dbContext = app.Services.GetRequiredService<ApplicationDbContext>();

    try {
      //await dbContext.Database.EnsureCreatedAsync();
      await dbContext.Database.MigrateAsync();
    }
    catch (Exception) {
      // ignored
    }
  }
}