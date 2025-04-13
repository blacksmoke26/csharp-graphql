// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/
// Guide: https://chillicream.com/docs/hotchocolate/v15/fetching-data/filtering

using HotChocolate.Execution.Configuration;

namespace Server.Graphql.Filtering;

public static class OperationFilterExtensions {
  public static IRequestExecutorBuilder AddGraphqlFiltering(this IRequestExecutorBuilder builder) {
    builder.AddFiltering(descriptor => {
      descriptor.AddDefaults().AllowAnd(false).AllowOr(false);

      descriptor.BindRuntimeType<StringType, StringOperationFilter>();

      // descriptor.BindRuntimeType<IdType, IdOperationFilter>();
      // descriptor.BindRuntimeType<LongType, LongOperationFilter>();
      // descriptor.BindRuntimeType<IntType, IntOperationFilter>();
      // descriptor.BindRuntimeType<ShortType, ShortOperationFilter>();
      // descriptor.BindRuntimeType<FloatType, FloatOperationFilter>();
    });

    return builder;
  }
}