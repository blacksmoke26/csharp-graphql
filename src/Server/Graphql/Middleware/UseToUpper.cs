// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using System.Reflection;
using HotChocolate.Types.Descriptors;

namespace Server.Graphql.Middleware;

public static class UseToUpperObjectFieldDescriptorExtensions {
  public static IObjectFieldDescriptor UseToUpper(this IObjectFieldDescriptor descriptor) {
    return descriptor.Use(next => async context => {
      await next(context);

      if (context.Result is string s) {
        context.Result = s.ToUpperInvariant();
      }
    });
  }
}

public class UseToUpperAttribute : ObjectFieldDescriptorAttribute {
  protected override void OnConfigure(
    IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member) {
    descriptor.UseToUpper();
  }
}