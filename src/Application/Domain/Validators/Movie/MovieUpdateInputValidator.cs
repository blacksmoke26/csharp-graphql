// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

using Abstraction.Inputs.Movie;
using FluentValidation;

namespace Application.Domain.Validators.Movie;

public class MovieUpdateInputValidator : AbstractValidator<MovieUpdateInput> {
  public MovieUpdateInputValidator() {
    RuleFor(x => x.Id)
      .GreaterThan(0)
      .NotEmpty();
    
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
  }
}