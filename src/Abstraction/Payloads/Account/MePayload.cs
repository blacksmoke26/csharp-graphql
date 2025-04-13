namespace Abstraction.Payloads.Account;

[GraphQLDescription("This class formats the successful user details response")]
public struct MePayload {
  [GraphQLDescription("The unique identifier")] [ID]
  public long Id { get; set; }

  [GraphQLDescription("The user's first name")]
  public string FirstName { get; init; }

  [GraphQLDescription("The user's last name")]
  public string LastName { get; init; }

  [GraphQLDescription("The email address")]
  public string Email { get; init; }

  [GraphQLDescription("The role name")]
  public string Role { get; init; }

  [GraphQLDescription("Status")]
  public string Status { get; set; }

  [GraphQLDescription("Creation date")]
  public DateTime? CreatedAt { get; set; }
}