// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using System.Globalization;
using Database.Context.EntityConfigurations;
using Database.Core.Base;
using Database.Seeders;

namespace Database.Context;

public partial class ApplicationDbContext : DbContext {
  /// <summary>The database configuration</summary>
  private readonly DbConfiguration _config;

  public ApplicationDbContext() {
  }

  public ApplicationDbContext(DbConfiguration config) {
    _config = config;
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
  }

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) {
  }

  /// <summary>Prints the query result to the console</summary>
  /// <param name="message">Message details</param>
  private void LogToConsole(string message) {
    if (_config.LogEnabled) Console.WriteLine(message);
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    optionsBuilder.UseNpgsql(_config.ConnectionString, options =>
        options.ConfigureDataSource(builder => {
          // Parse JSON as dynamic Object
          // See: https://www.npgsql.org/doc/types/json.html?tabs=datasource
          builder
            .EnableDynamicJson()
            .ConfigureJsonOptions(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }))
      // use snake_care for table and columns name explicitly 
      .UseSnakeCaseNamingConvention(CultureInfo.InvariantCulture)
      .EnableDetailedErrors()
      /*.UseAsyncSeeding((context, _, token)
        => new SeederContext(context).InitializeAsync(token))
      .UseSeeding((_, _) => { })*/
      .LogTo(LogToConsole, _config.LogLevel);
  }

  protected override void OnModelCreating(ModelBuilder builder) {
    builder.ApplyConfiguration(new GenreEntityTypeConfiguration());
    builder.ApplyConfiguration(new MovieEntityTypeConfiguration());
    builder.ApplyConfiguration(new RatingEntityTypeConfiguration());
    builder.ApplyConfiguration(new UserEntityTypeConfiguration());
    OnModelCreatingPartial(builder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

  /// <inheritdoc/>
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new()) {
    var modifiedEntities = ChangeTracker.Entries();

    foreach (var entity in modifiedEntities) {
      if (entity.Entity is EntityBase model)
        await model.OnTrackChangesAsync(entity.State, cancellationToken);
    }

    return await base.SaveChangesAsync(cancellationToken);
  }
}