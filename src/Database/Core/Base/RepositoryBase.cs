// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql
// See: https://sequelize.org/api/v7/interfaces/_sequelize_core.index.findoptions

using System.Linq.Expressions;

namespace Database.Core.Base;

public record FindOptions<TEntity, TResult> : FilterOptions<TEntity> where TEntity : class {
  public Expression<Func<TEntity, TResult>>? Project { get; set; }
}

public record FilterOptions<TEntity> where TEntity : class {
  public List<Expression<Func<TEntity, bool>>> Where { get; set; } = [];
  public Dictionary<bool, Expression<Func<TEntity, bool>>> AndWhere { get; set; } = new();
  public bool NoTracking { get; set; } = false;
  public Func<IQueryable<TEntity>, IQueryable<TEntity>>? Query { get; set; }
  public Dictionary<Expression<Func<TEntity, object>>, SortOrder> Order { get; set; } = new();
  public List<Expression<Func<TEntity, object>>> Include { get; set; } = [];
  public int? Offset { get; set; } = null;
  public int? Limit { get; set; } = null;
}

public abstract class RepositoryBase<TEntity>(ApplicationDbContext context)
  where TEntity : class {
  protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

  protected IQueryable<TEntity> CreateQueryFromFilterOptions(FilterOptions<TEntity> options) {
    var query = DbSet.AsQueryable();

    if (options.NoTracking is true) {
      query = query.AsNoTracking();
    }

    if (options.Offset is not null) {
      query = query.Skip((int)options.Offset);
    }

    if (options.Limit is not null) {
      query = query.Take((int)options.Limit);
    }

    if (options.Where.Count > 0) {
      foreach (var where in options.Where)
        query = query.Where(where);
    }

    if (options.AndWhere.Count > 0) {
      foreach (var where in options.AndWhere)
        if (where.Key)
          query = query.Where(where.Value);
    }

    if (options.Include.Count > 0) {
      foreach (var include in options.Include)
        query = query.Include(include);
    }

    if (options.Order.Count > 0) {
      foreach (var order in options.Order) {
        query = order.Value == SortOrder.Ascending
          ? query.OrderBy(order.Key)
          : query.OrderByDescending(order.Key);
      }
    }

    return options.Query?.Invoke(query) ?? query;
  }

  protected IQueryable<TResult> CreateProjectedQuery<TResult>(
    FindOptions<TEntity, TResult> options, CancellationToken token = default) {
    var query = CreateQueryFromFilterOptions(options);

    return options.Project is not null
      ? query.Select(options.Project).Cast<TResult>()
      : query.Cast<TResult>();
  }

  /// <summary>
  /// Asynchronously returns the first element of a sequence.
  /// </summary>
  /// <param name="options">The find options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The found record, null if there is not</returns>
  public Task<TResult> GetAsync<TResult>(
    FindOptions<TEntity, TResult> options, CancellationToken token = default) {
    return CreateProjectedQuery(options).FirstAsync(token);
  }

  /// <summary>
  /// Asynchronously returns the first element of a sequence.
  /// </summary>
  /// <param name="options">The find options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The found record, null if there is not</returns>
  public Task<TEntity> GetAsync(
    FindOptions<TEntity, TEntity> options, CancellationToken token = default) {
    return CreateProjectedQuery(options).FirstAsync(token);
  }

  /// <summary>
  /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
  /// </summary>
  /// <param name="options">The find options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The found record, null if there is not</returns>
  public Task<TResult?> GetOrDefaultAsync<TResult>(
    FindOptions<TEntity, TResult> options, CancellationToken token = default) {
    return CreateProjectedQuery(options).FirstOrDefaultAsync(token);
  }

  /// <summary>
  /// Asynchronously returns the first element of a sequence, or a default value if the sequence contains no elements.
  /// </summary>
  /// <param name="options">The find options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The found record, null if there is not</returns>
  public Task<TEntity?> GetOrDefaultAsync(
    FindOptions<TEntity, TEntity> options, CancellationToken token = default) {
    return CreateProjectedQuery(options).FirstOrDefaultAsync(token);
  }

  /// <summary>
  /// Asynchronously creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it
  /// asynchronously.
  /// </summary>
  /// <param name="options">The find options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The found record, null if there is not</returns>
  public Task<List<TResult>> GetAllAsync<TResult>(
    FindOptions<TEntity, TResult> options, CancellationToken token = default) {
    return CreateProjectedQuery(options).ToListAsync(token);
  }

  /// <summary>
  /// Asynchronously creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it
  /// asynchronously.
  /// </summary>
  /// <param name="options">The find options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>The found record, null if there is not</returns>
  public Task<List<TEntity>> GetAllAsync(
    FindOptions<TEntity, TEntity> options, CancellationToken token = default) {
    return CreateProjectedQuery(options).ToListAsync(token);
  }

  /// <summary>
  /// Deletes one or many records against the given condition
  /// </summary>
  /// <param name="predicate">A function to test each element for a condition.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>True upon exist, false otherwise</returns>s
  public Task<int> DeleteAsync(
    Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) {
    return DeleteAsync(new FilterOptions<TEntity> { Where = [predicate] }, token);
  }

  /// <summary>
  /// Deletes one or many records against the given condition
  /// </summary>
  /// <param name="options">The filter options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>True upon exist, false otherwise</returns>s
  public Task<int> DeleteAsync(
    FilterOptions<TEntity> options, CancellationToken token = default) {
    return CreateQueryFromFilterOptions(options).ExecuteDeleteAsync(token);
  }

  /// <summary>
  /// Verifies existence of record against the given condition
  /// </summary>
  /// <param name="predicate">A function to test each element for a condition.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Returns the total counts or entities satisfied by the condition</returns>
  public Task<bool> ExistsAsync(
    Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) {
    return ExistsAsync(new FilterOptions<TEntity> { Where = [predicate] }, token);
  }

  /// <summary>
  /// Verifies existence of record against the given condition
  /// </summary>
  /// <param name="options">The filter options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Returns the total counts or entities satisfied by the condition</returns>
  public Task<bool> ExistsAsync(
    FilterOptions<TEntity> options, CancellationToken token = default)
    => CreateQueryFromFilterOptions(options).AnyAsync(token);

  /// <summary>
  /// Counts the records against the given condition
  /// </summary>
  /// <param name="predicate">A function to test each element for a condition.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Returns the total counts or entities satisfied by the condition</returns>
  public Task<int> CountAsync(
    Expression<Func<TEntity, bool>> predicate, CancellationToken token = default) {
    return CountAsync(new FilterOptions<TEntity> { Where = [predicate] }, token);
  }

  /// <summary>
  /// Counts the records against the given condition
  /// </summary>
  /// <param name="options">The filter options.</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Returns the total counts or entities satisfied by the condition</returns>
  public Task<int> CountAsync(
    FilterOptions<TEntity> options, CancellationToken token = default) {
    return CreateQueryFromFilterOptions(options).CountAsync(token);
  }

  /// <summary>
  /// Writes the entity object changes with JSONB into a database
  /// </summary>
  /// <param name="token">The cancellation token</param>
  /// <returns>The entity instance if updated successfully, Null upon failed</returns>
  public virtual async Task<int> SaveAsync(CancellationToken token = default) {
    return await context.SaveChangesAsync(token);
  }

  public virtual async Task<int> AddAsync(TEntity entity, CancellationToken token = default) {
    await DbSet.AddAsync(entity, token);
    return await SaveAsync(token);
  }

  public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token = default) {
    await DbSet.AddRangeAsync(entities, token);
    return await SaveAsync(token);
  }

  public virtual async Task<int> UpdateAsync(TEntity entityToUpdate, CancellationToken token = default) {
    DbSet.Attach(entityToUpdate);
    context.Entry(entityToUpdate).State = EntityState.Modified;
    return await SaveAsync(token);
  }

  public virtual async Task<int> UpdateRangeAsync(
    IEnumerable<TEntity> entitiesToUpdate, CancellationToken token = default) {
    var entities = entitiesToUpdate.ToList();
    DbSet.AttachRange(entities);
    context.Entry(entities).State = EntityState.Modified;
    return await SaveAsync(token);
  }
}