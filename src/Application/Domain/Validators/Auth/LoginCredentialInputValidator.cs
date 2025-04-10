// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Auth;
using FluentValidation;

namespace Application.Domain.Validators.Auth;

public class LoginCredentialInputValidator : AbstractValidator<LoginCredentialInput> {
  public LoginCredentialInputValidator() {
    RuleFor(x => x.Email)
      .EmailAddress()
      .NotEmpty();

    RuleFor(x => x.Password)
      .MinimumLength(8)
      .MaximumLength(20)
      .NotEmpty();
  }
}