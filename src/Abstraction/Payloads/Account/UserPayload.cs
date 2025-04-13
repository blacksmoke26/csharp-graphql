namespace Abstraction.Payloads.Account;

[GraphQLDescription("The authenticated user details")]
public struct UserPayload {
  [GraphQLDescription("The unique identifier")] [ID]
  public long Id { get; set; }

  [GraphQLDescription("The user's first name")]
  public string FirstName { get; init; }

  [GraphQLDescription("The user's last name")]
  public string LastName { get; init; }
}