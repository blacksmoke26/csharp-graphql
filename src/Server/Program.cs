// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

var builder = WebApplication.CreateBuilder(args);

builder.Services.InitBootstrapper(builder.Configuration);
var app = builder.Build();

await app.InitializeDbAsync();

app.UseBootstrapper();

app.Run();
