// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Website: https://github.com/blacksmoke26/

using Abstraction.Inputs.Rating;
using FluentValidation;

namespace Application.Domain.Validators.Rating;

public class MovieRatingInputValidator: AbstractValidator<MovieRatingInput> {
  public MovieRatingInputValidator() {
    RuleFor(x => x.MovieId)
      .GreaterThan(0)
      .NotEmpty();

    RuleFor(x => x.Score)
      .InclusiveBetween((short)0, (short)5)
      .NotNull();

    RuleFor(x => x.Feedback)
      .MaximumLength(500);
  }
}