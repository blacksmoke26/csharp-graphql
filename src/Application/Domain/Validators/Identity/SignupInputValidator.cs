// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-webapp

using Abstraction.Inputs.Identity;
using FluentValidation;

namespace Application.Domain.Validators.Identity;

/// <summary>
/// Validates the input data provided for signing up a new user account.
/// </summary>
/// <remarks>
/// This validator ensures that the signup input data adheres to specified constraints, including:
/// - First and last name must be non-empty and between 3 and 20 characters in length.
/// - Email must be a valid email address and non-empty.
/// - Password must be between 8 and 20 characters in length and non-empty.
/// - Email must be unique and not already registered in the system.
/// </remarks>
public class SignupInputValidator : AbstractValidator<SignupInput> {
  /// <summary>
  /// Provides access to user repository operations such as querying
  /// and validating user data within the database context.
  /// </summary>
  private readonly UserRepository _userRepo;

  /// <summary>
  /// Validates the input provided during user signup
  /// </summary>
  /// <remarks>
  /// This validator ensures the FirstName, LastName, Email, and Password fields
  /// meet their respective requirements such as length, format, and emptiness constraints.
  /// It also checks if the provided email address is already registered in the system.
  /// </remarks>
  public SignupInputValidator(UserRepository userRepo) {
    _userRepo = userRepo;

    RuleFor(x => x.FirstName)
      .MinimumLength(3)
      .MaximumLength(20)
      .NotEmpty();

    RuleFor(x => x.LastName)
      .MinimumLength(3)
      .MaximumLength(20)
      .NotEmpty();
    
    RuleFor(x => x.Email)
      .EmailAddress()
      .NotEmpty();

    RuleFor(x => x.Password)
      .MinimumLength(8)
      .MaximumLength(20)
      .NotEmpty();

    RuleFor(x => x.Email)
      .MustAsync(ValidateEmailAsync)
      .WithMessage("This email address is already registered.");
  }

  /// <summary>
  /// Validates that the provided email address does not exist in the user repository.
  /// </summary>
  /// <param name="userSignup">The input object containing user details.</param>
  /// <param name="email">The email address to validate for uniqueness.</param>
  /// <param name="token">The cancellation token to handle task cancellation.</param>
  /// <returns>True if the email does not exist, false otherwise.</returns>
  private async Task<bool> ValidateEmailAsync(
    SignupInput userSignup, string email, CancellationToken token) {
    return !await _userRepo.ExistsAsync(x => x.Email == email, token);
  }
}