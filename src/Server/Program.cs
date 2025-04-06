// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Server.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InitBootstrapper(builder.Configuration);

var app = builder.Build();

await app.InitializeDbAsync();

app.UseBootstrapper();

app.Run();

/*[GraphQLDescription("The current user information")]
public record Me {
  [GraphQLDescription("The first name of the user")]
  public string FirstName { get; set; }
}*/

/*[QueryType]
public class Query {
  public async Task<Me> Me(UserRepository userRepo) {
    var user = await userRepo.GetAsync(new FindOptions<User, Me> {
      /*Project = x => new Me {
        FirstName = x.FirstName
      },#1#
      Where = [u => u.Id == 1]
    });

    return user;
  }
}

[QueryType]
public class Users {
  [UsePaging]
  [UseProjection]
  public IQueryable<User> GetAllUsers(ApplicationDbContext context) => context.Users;
}*/