// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Microsoft.Extensions.Logging;

namespace Database;

/// <summary>
/// Configuration presents the options required by the DbContext class
/// </summary>
public struct DbConfiguration {
  public required string ConnectionString { get; init; }
  public bool LogEnabled { get; init; }
  public LogLevel LogLevel { get; init; }
}