// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Application.Config;

/// <summary>
/// JwtConfiguration presents the options required by JWT Auth service
/// </summary>
public struct JwtConfiguration {
  /// <summary>Represents a symmetric security key.</summary>
  public required string Key { get; init; }

  /// <summary>identifies the principal that issued the JWT</summary>
  public string Issuer { get; init; }

  /// <summary>Identifies the recipients that the JWT is intended for.</summary>
  public string Audience { get; init; }

  /// <summary>The expiration time in hours  on or after which the JWT MUST NOT be accepted for processing</summary>
  public int ExpirationInHours { get; init; }
}