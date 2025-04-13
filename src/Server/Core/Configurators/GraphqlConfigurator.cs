// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql
// Validation: https://eduardocp.github.io/hot-chocolate-fluent-validation

using Abstraction;
using Server.Core.Config;
using Server.Core.Interfaces;
using Server.Graphql.Filtering;

namespace Server.Core.Configurators;

public abstract class GraphqlConfigurator : IApplicationServiceConfigurator {
  /// <inheritdoc/>
  public static void Configure(IServiceCollection services, IConfiguration config) {
    var conf = config.GetSection("Graphql").Get<GraphQlConfiguration>();

    services.AddGraphQLServer()
      .AddAbstractions()
      .AddProjections()
      .AddGraphqlFiltering()
      .AddSorting()
      .AddPagingArguments()
      .AddMutationConventions(new MutationConventionOptions {
        ApplyToAllMutations = false,
      })
      .AddQueryType(q => q.Name("Query"))
      .AddMutationType(q => q.Name("Mutation"))
      .AddAuthorization()
      .AddGraphServerTypes()
      .ModifyRequestOptions(opt =>
        opt.IncludeExceptionDetails = conf.IncludeExceptionDetails)
      .AddFluentValidation(o => o.UseDefaultErrorMapper())
      .InitializeOnStartup();
  }

  /// <inheritdoc/>
  public static void Use(WebApplication app) {
    app.MapGraphQL();
  }
}