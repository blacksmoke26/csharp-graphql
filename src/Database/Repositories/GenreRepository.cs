// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-graphql

using Database.Core.Base;

namespace Database.Repositories;

public sealed class GenreRepository(ApplicationDbContext context) : RepositoryBase<Genre>(context) {
}