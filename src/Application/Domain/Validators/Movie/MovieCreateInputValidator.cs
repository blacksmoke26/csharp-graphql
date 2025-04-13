// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Movie;
using FluentValidation;

namespace Application.Domain.Validators.Movie;

public class MovieCreateInputValidator : AbstractValidator<MovieCreateInput> {
  private readonly MovieRepository _movieRepo;

  public MovieCreateInputValidator(MovieRepository movieRepo) {
    _movieRepo = movieRepo;

    RuleFor(x => x.Title)
      .MinimumLength(3)
      .MaximumLength(50)
      .NotEmpty();

    RuleFor(x => x.Genres).NotNull().NotEmpty()
      .WithMessage("At least one or more genres are required");

    RuleFor(x => x.Genres)
      .Must(x => x != null && x.Count() <= 5).WithMessage("No more than 5 genres are allowed");

    RuleForEach(x => x.Genres)
      .MinimumLength(3)
      .MaximumLength(50)
      .NotNull();

    RuleFor(x => x.Genres)
      .Must(x => {
        if (x == null) return true;
        return !x.Any() || x.Count() == x.Distinct().Count();
      })
      .WithMessage("Genres should not contains duplicate items");

    RuleFor(x => x.YearOfRelease)
      .InclusiveBetween((short)1900, (short)DateTime.UtcNow.Year);

    RuleFor(x => x)
      .MustAsync(ValidateAgainstTitleAndYearAsync)
      .WithMessage("Movie with a same title and year is already exist. "
                   + "Please choose a different title or release year");
  }

  /// <summary>
  /// Validate data against Title and Year of release 
  /// </summary>
  /// <param name="createInput">The input object</param>
  /// <param name="token">The cancellation token</param>
  /// <returns>Whatever the validation has passed or failed</returns>
  private async Task<bool> ValidateAgainstTitleAndYearAsync(
    MovieCreateInput createInput, CancellationToken token = default
  ) {
    return !await _movieRepo.ExistsAsync(x
      => x.Title == createInput.Title&& x.YearOfRelease == createInput.YearOfRelease, token);
  }
}