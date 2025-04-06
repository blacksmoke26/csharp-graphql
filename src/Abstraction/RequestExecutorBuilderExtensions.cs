// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using HotChocolate.Execution.Configuration;

namespace Abstraction;

public static class RequestExecutorBuilderExtensions {
  /// <summary>
  /// Register GraphQL specific abstractions
  /// </summary>
  /// <param name="builder">The <see cref="HotChocolate.Execution.Configuration.IRequestExecutorBuilder"/> instance</param>
  /// <returns>The <see cref="HotChocolate.Execution.Configuration.IRequestExecutorBuilder"/></returns>
  public static IRequestExecutorBuilder AddAbstractions(this IRequestExecutorBuilder builder) {
    return builder;
  }
}