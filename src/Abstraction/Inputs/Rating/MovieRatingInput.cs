namespace Abstraction.Inputs.Rating;

[GraphQLDescription("Rate a movie")]
public struct MovieRatingInput {
  [GraphQLDescription("Movie ID")] [ID]
  public long MovieId { get; init; }

  [GraphQLDescription("Overall rating")]
  public short Score { get; init; }

  [GraphQLDescription("Additional feedback")]
  public string? Feedback { get; init; }
}