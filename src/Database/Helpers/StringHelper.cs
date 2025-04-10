// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Database.Helpers;

public static class StringHelper {
  /// <summary>Generates slug from the given values</summary>
  /// <param name="values">The stringy values</param>
  /// <returns>The computed sluggish value</returns>
  public static string Slugify(params object?[] values) {
    return string.Concat(values).ToLower().Kebaberize();
  }
}