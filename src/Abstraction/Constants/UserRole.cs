// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Abstraction.Constants;

public static class UserRole {
  /// <summary>
  /// Only allowed for `Admin` role
  /// </summary>
  public const string Admin = "admin";

  /// <summary>
  /// Only allowed for `User` role
  /// </summary>
  public const string User = "user";
}

public enum RoleType {
  User = 0,
  Admin = 1
}