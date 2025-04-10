// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

namespace Database.Constants;

public enum UserStatus {
  Deleted = 0,
  Disabled = 1,
  Blocked = 2,
  Inactive = 3,
  Active = 10
}

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

public enum MovieStatus {
  Deleted = 0,
  Pending = 1,
  Blocked = 2,
  Draft = 3,
  Published = 10
}