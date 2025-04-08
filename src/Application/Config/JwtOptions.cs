// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using System.Security.Claims;

namespace Application.Config;

/// <summary>Additional JWT options to customize the token</summary>
public struct JwtOptions {
  /// <summary>The issuer domain</summary>
  /// <example><code>https://example.com</code></example>
  public string Issuer { get; init; }

  /// <summary>The target audience domain</summary>
  /// <example><code>https://department.example.com</code></example>
  public string Audience { get; init; }

  /// <summary>Expiration time in hours</summary>
  public int ExpirationInHours { get; init; }

  /// <summary>The claims to provide</summary>
  public readonly IList<Claim> Claims => [];
}