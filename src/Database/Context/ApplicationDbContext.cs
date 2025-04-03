// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using System.Globalization;
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

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<Genre>(entity => {
      entity.HasKey(e => e.Id).HasName("genres_pkey");

      entity.Property(e => e.Id).HasComment("ID");
      entity.Property(e => e.MovieId).HasComment("Movie");
      entity.Property(e => e.Name).HasComment("Name");

      entity.HasOne(d => d.Movie).WithMany(p => p.Genres).HasConstraintName("FK_genres_movie_id_movies_id");
      entity.SeedData();
    });

    modelBuilder.Entity<Movie>(entity => {
      entity.HasKey(e => e.Id).HasName("movies_pkey");

      entity.Property(e => e.Id).HasComment("ID");
      entity.Property(e => e.CreatedAt).HasComment("Created");
      entity.Property(e => e.Slug).HasComment("Slug");
      entity.Property(e => e.Status).HasComment("Status");
      entity.Property(e => e.Title).HasComment("Title");
      entity.Property(e => e.UpdatedAt).HasComment("Updated");
      entity.Property(e => e.UserId).HasComment("User");
      entity.Property(e => e.YearOfRelease).HasComment("Year of Release");

      entity.HasOne(d => d.User).WithMany(p => p.Movies).HasConstraintName("FK_movies_user_id_users_id");
      entity.SeedData();
    });

    modelBuilder.Entity<Rating>(entity => {
      entity.HasKey(e => e.Id).HasName("ratings_pkey");

      entity.Property(e => e.Id).HasComment("ID");
      entity.Property(e => e.CreatedAt).HasComment("Created");
      entity.Property(e => e.Feedback).HasComment("Feedback");
      entity.Property(e => e.MovieId).HasComment("Movie");
      entity.Property(e => e.Score)
        .HasDefaultValue((short)1)
        .HasComment("Score");
      entity.Property(e => e.UpdatedAt).HasComment("Updated");
      entity.Property(e => e.UserId).HasComment("User");

      entity.HasOne(d => d.Movie).WithMany(p => p.Ratings).HasConstraintName("FK_ratings_movie_id_movies_id");

      entity.HasOne(d => d.User).WithMany(p => p.Ratings).HasConstraintName("FK_ratings_user_id_users_id");
      entity.SeedData();
    });

    modelBuilder.Entity<User>(entity => {
      entity.HasKey(e => e.Id).HasName("users_pkey");

      entity.HasIndex(e => e.Metadata, "IDX_users_metadata")
        .HasMethod("gin")
        .HasAnnotation("Npgsql:StorageParameter:gin_pending_list_limit", "2097151");

      entity.Property(e => e.Id).HasComment("ID");
      entity.Property(e => e.AuthKey).HasComment("Authorization Key");
      entity.Property(e => e.CreatedAt).HasComment("Created");
      entity.Property(e => e.Email).HasComment("Email Address");
      entity.Property(e => e.FirstName).HasComment("First name");
      entity.Property(e => e.LastName).HasComment("Last name");
      entity.Property(e => e.Metadata)
        .HasDefaultValueSql("'{}'::jsonb")
        .HasComment("Metadata");
      entity.Property(e => e.Password).HasComment("Password");
      entity.Property(e => e.PasswordHash).HasComment("Password Hash");
      entity.Property(e => e.Role).HasComment("Role");
      entity.Property(e => e.Status).HasComment("Status");
      entity.Property(e => e.UpdatedAt).HasComment("Updated");
      entity.SeedData();
    });

    OnModelCreatingPartial(modelBuilder);
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