// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using System.Diagnostics.CodeAnalysis;
using HotChocolate;

namespace Application.Helpers;

public static class ErrorHelper {
  /// <summary>Throws a GraphQL exception</summary>
  /// <param name="message">The error message</param>
  [DoesNotReturn]
  public static void ThrowError(string message) {
    throw new GraphQLException(message);
  }

  /// <summary>
  /// Throw an HTTP error if the given condition is null
  /// </summary>
  /// <param name="input">The nullable condition to verify</param>
  /// <param name="message">The error message</param>
  public static void ThrowIfNull([NotNull] object? input, string message) {
    if (input is null) ThrowError(message);
  }

  /// <summary>
  /// Throw an HTTP error if the given condition is null
  /// </summary>
  /// <param name="input">The nullable condition to verify</param>
  /// <param name="message">The error message</param>
  public static void ThrowIfNotNull(object? input, string message) {
    if (input is not null) ThrowError(message);
  }

  /// <summary>
  /// Throw an HTTP error if the given condition is null
  /// </summary>
  /// <param name="argument">The string argument to validate.</param>
  /// <param name="message">The error message</param>
  public static void ThrowIfNullOrEmpty([NotNull] string? argument, string message) {
    if (string.IsNullOrEmpty(argument)) ThrowError(message);
  }

  /// <summary>
  /// Throw an HTTP error if the given condition is null
  /// </summary>
  /// <param name="argument">The string argument to validate.</param>
  /// <param name="message">The error message</param>
  public static void ThrowIfNullOrWhiteSpace([NotNull] string? argument, string message) {
    if (string.IsNullOrWhiteSpace(argument)) ThrowError(message);
  }

  /// <summary>
  /// Throw an HTTP error if the given condition is falsy
  /// </summary>
  /// <param name="condition">The nullable condition to verify</param>
  /// <param name="message">The error message</param>
  public static void ThrowIfFalse([DoesNotReturnIf(false)] bool condition, string message) {
    if (!condition) ThrowError(message);
  }

  /// <summary>
  /// Throw an HTTP error if the given condition is falsy
  /// </summary>
  /// <param name="condition">The nullable condition to verify</param>
  /// <param name="message">The error message</param>
  public static void ThrowIfTrue([DoesNotReturnIf(true)] bool condition, string message) {
    if (condition) ThrowError(message);
  }
}