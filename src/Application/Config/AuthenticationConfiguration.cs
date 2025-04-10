// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Application.Config;

public struct AuthenticationConfiguration {
  /// <summary>Expires token and regenerates the authentication key after logout</summary>
  public bool RefreshAuthKeyAfterLogout { get; init; }
}