// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

using System.Text.RegularExpressions;
using Abstraction.Inputs.Identity;
using Database.Helpers;
using FluentValidation;

namespace Application.Domain.Validators.Identity;

/// <summary>
/// Validator for password reset requests.
/// </summary>
/// <remarks>
/// This validator ensures that the password reset payload meets the required criteria:
/// - Email is a valid email address and not empty
/// - Reset code is exactly 6 characters long and not empty
/// - New password is between 8 and 20 characters long and not empty
/// </remarks>
public partial class ResetPasswordInputValidator : AbstractValidator<ResetPasswordInput> {
  [GeneratedRegex(IdentityHelper.PasswordResetCodeRegex)]
  private static partial Regex PasswordResetCodeRegex();

  /// <summary>
  /// Initializes a new instance of the <see cref="ResetPasswordInputValidator"/> class.
  /// </summary>
  public ResetPasswordInputValidator() {
    RuleFor(x => x.Email)
      .EmailAddress().NotNull().NotEmpty();

    RuleFor(x => x.ResetCode)
      .Length(IdentityHelper.PasswordResetCodeSize)
      .Must(x => x?.Length != IdentityHelper.PasswordResetCodeSize || PasswordResetCodeRegex().IsMatch(x))
      .WithMessage("The reset code is invalid")
      .NotEmpty();

    RuleFor(x => x.NewPassword)
      .MinimumLength(8).MaximumLength(20).NotEmpty();
  }
}