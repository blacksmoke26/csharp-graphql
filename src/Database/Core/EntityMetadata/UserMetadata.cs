// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository: https://github.com/blacksmoke26/csharp-graphql

namespace Database.Core.EntityMetadata;

public record UserMetadata {
  [StringLength(10), JsonPropertyName("language")]
  public string Language { get; set; } = "en-US";

  [StringLength(20), JsonPropertyName("timezone")]
  public string Timezone { get; set; } = "UTC";

  [JsonPropertyName("security")]
  public UserMetadataSecurity Security { get; set; } = new();

  [JsonPropertyName("activation")]
  public UserMetadataActivation Activation { get; set; } = new();

  [JsonPropertyName("password")]
  public UserMetadataPassword Password { get; set; } = new();

  [JsonPropertyName("loggedInHistory")]
  public UserMetadataLoggedInHistory LoggedInHistory { get; set; } = new();

  [Owned]
  public record UserMetadataActivation {
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("pending")]
    public bool? Pending { get; set; }

    [JsonPropertyName("requestedAt")]
    public DateTime? RequestedAt { get; set; }

    [JsonPropertyName("completedAt")]
    public DateTime? CompletedAt { get; set; }
  }

  [Owned]
  public record UserMetadataLoggedInHistory {
    [MaxLength(20)] [JsonPropertyName("lastIp")]
    public string? LastIp { get; set; } = null;

    [JsonPropertyName("lastDate")]
    public DateTime? LastDate { get; set; }

    [JsonPropertyName("successCount")]
    public int SuccessCount { get; set; } = 0;

    [JsonPropertyName("failedCount")]
    public int FailedCount { get; set; } = 0;
  }

  [Owned]
  public record UserMetadataPassword {
    [JsonPropertyName("lastResetAt")]
    public DateTime? LastResetAt { get; set; }

    [StringLength(IdentityHelper.PasswordResetCodeSize), JsonPropertyName("resetCode")]
    public string? ResetCode { get; set; }

    [JsonPropertyName("resetCodeRequestedAt")]
    public DateTime? ResetCodeRequestedAt { get; set; }

    [JsonPropertyName("resetCount")]
    public int ResetCount { get; set; }

    [JsonPropertyName("lastUpdatedAt")]
    public DateTime? LastUpdatedAt { get; set; }

    [JsonPropertyName("updatedCount")]
    public int UpdatedCount { get; set; }
  }

  [Owned]
  public record UserMetadataSecurity {
    [JsonPropertyName("tokenInvalidate")]
    public bool TokenInvalidate { get; set; }
  }
}