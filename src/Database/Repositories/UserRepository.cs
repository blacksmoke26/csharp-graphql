// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Database.Core.Base;

namespace Database.Repositories;

public sealed class UserRepository(ApplicationDbContext context) : RepositoryBase<User>(context) {
  /// <summary>
  /// Fetch a single entity against authorization key
  /// </summary>
  /// <param name="authKey">The authorization key</param>
  /// <param name="token">Cancellation token</param>
  /// <returns>A task that represents the asynchronous operation. Either an entity or null upon none</returns>
  public Task<User?> GetByAuthKeyAsync(string authKey, CancellationToken token = default) {
    return DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.AuthKey == authKey, token);
  }

  /// <summary>
  /// Fetch a single entity against email address
  /// </summary>
  /// <param name="email">The email address</param>
  /// <param name="token">Cancellation token</param>
  /// <returns>A task that represents the asynchronous operation. Either an entity or null upon none</returns>
  public Task<User?> GetByEmailAsync(string email, CancellationToken token = default) {
    return DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email, token);
  }
}