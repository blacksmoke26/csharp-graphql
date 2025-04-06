// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Database.Constants;
using Database.Core.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Seeders;

public static class UsersSeeder {
  public static void SeedData(this EntityTypeBuilder<User> entity) {
    User admin = new() {
      Id = 1,
      FirstName = "Admin",
      LastName = "User",
      Email = "admin@example.com",
      Role = UserRole.Admin,
      Status = UserStatus.Active,
      CreatedAt = DateTime.UtcNow
    };
    admin.SetPassword("Password@123");
    admin.GenerateAuthKey();
    
    User user = new() {
      Id = 2,
      FirstName = "Member",
      LastName = "User",
      Email = "user@example.com",
      Role = UserRole.User,
      Status = UserStatus.Active,
      CreatedAt = DateTime.UtcNow
    };
    user.SetPassword("Password@123");
    user.GenerateAuthKey();
    
    entity.HasData(admin, user);
  }
}