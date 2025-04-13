// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Abstraction.Payloads.Account;
using HotChocolate.Data.Sorting;

namespace Server.Resolvers.User.Sorting;

public class UserSortInputType : SortInputType<UserPayload> {
  protected override void Configure(ISortInputTypeDescriptor<UserPayload> descriptor) {
    descriptor.BindFieldsExplicitly();
    descriptor.Field(x => x.FirstName);
  }
}