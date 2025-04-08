namespace Abstraction.Payloads.Account;

[GraphQLDescription("This class formats the successful user details response")]
public struct MePayload {
  [GraphQLDescription("The unique identifier")]
  public required long Id { get; set; }

  [GraphQLDescription("The user's first and last name")]
  public required string Fullname { get; init; }

  [GraphQLDescription("The user's first name")]
  public required string FirstName { get; init; }

  [GraphQLDescription("The user's last name")]
  public required string LastName { get; init; }

  [GraphQLDescription("The email address")]
  public required string Email { get; init; }

  [GraphQLDescription("The role name")]
  public required string Role { get; init; }

  [GraphQLDescription("Status")]
  public required string Status { get; set; }

  [GraphQLDescription("Creation date")]
  public required DateTime? CreatedAt { get; set; }
}