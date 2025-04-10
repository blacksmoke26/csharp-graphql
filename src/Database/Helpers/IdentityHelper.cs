// Licensed to the end users under one or more agreements.
// Copyright (c) 2025 Junaid Atari, and contributors
// Repository:https://github.com/blacksmoke26/csharp-webapp
// See also: https://claudiobernasconi.ch/blog/how-to-hash-passwords-with-bcrypt-in-csharp/

using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using NanoidDotNet;
using BC = BCrypt.Net.BCrypt;

namespace Database.Helpers;

/// <summary>EncryptedPasswordResult represents the result of password encryption </summary>
public struct EncryptedPasswordResult {
  /// <summary>The sha1 encrypted password</summary>
  public required string Password { get; init; }

  /// <summary>The password key to verify the encrypted password</summary>
  public required string PasswordHash { get; init; }
}

/// <summary>IdentityHelper class to encrypt and validate password and authentication key </summary>
public static class IdentityHelper {
  /// <summary>The hash type to verify the password integrity</summary>
  public static HashType HashType => HashType.SHA384;

  /// <summary>The char. size of reset password code</summary>
  public const int PasswordResetCodeSize = 6;
  
  /// <summary>The char. size of reset password code</summary>
  public const int ActivationCodeSize = 6;

  /// <summary>The regex to validate the reset code</summary>
  public const string PasswordResetCodeRegex = "^[A-Z0-9]+$";

  /// <summary>The regex to validate the activation code</summary>
  public const string ActivationCodeRegex = "^[A-Z0-9]+$";

  /// <summary>Creates a SHA1 of the string</summary>
  /// <param name="str">The string to sha1</param>
  /// <returns>The SHA1 text</returns>
  public static string Sha1(string str) {
    var hashBytes = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(str));
    return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLower();
  }

  /// <summary>Encrypts the password and returns the encoded and a hashing key</summary>
  /// <param name="password">The password</param>
  /// <returns>The encoded password and a hash key</returns>
  public static EncryptedPasswordResult EncryptPassword(string password) {
    return new EncryptedPasswordResult {
      Password = Sha1(password),
      PasswordHash = GeneratePasswordHash(password)
    };
  }

  /// <summary>Generates a secure hash from a random salt and password.</summary>
  /// <param name="password">The password</param>
  /// <param name="cost">Which denotes the algorithmic cost that should be used.</param>
  /// <returns>The generated password hash</returns>
  public static string GeneratePasswordHash(string password, int cost = 11) {
    return BC.EnhancedHashPassword(password, HashType, cost);
  }

  /// <summary>Return current Unix timestamp</summary>
  /// <param name="isUtc">Time in UTC or not</param>
  /// <returns>Returns the current time measured in the number of seconds since
  /// the Unix Epoch (January 1, 1970 00:00:00 GMT).</returns>
  public static long GetUnixTime(bool isUtc = true) {
    return isUtc
      ? TimeProvider.System.GetUtcNow().ToUnixTimeSeconds()
      : TimeProvider.System.GetLocalNow().ToUnixTimeSeconds();
  }

  /// <summary>Generates new password reset token</summary>
  /// <param name="size">Token chars size</param>
  /// <returns>The generated token</returns>
  public static string GeneratePasswordResetToken(int size = 32) {
    return $"{Nanoid.Generate(size: size)}.{GetUnixTime()}";
  }

  /// <summary>Verifies a password against a hash.</summary>
  /// <param name="result">Password results to verify</param>
  /// <returns>Whether the password is correct or not</returns>
  public static bool ValidatePassword(EncryptedPasswordResult result) {
    return BC.EnhancedVerify(result.Password, result.PasswordHash, HashType);
  }

  /// <summary>Generates unique authentication key</summary>
  /// <returns>The generate key</returns>
  public static string GenerateAuthKey() => Nanoid.Generate(size: 32);

  /// <summary>Generates the reset password code</summary>
  /// <returns>The generated reset code</returns>
  public static string GeneratePasswordResetCode() {
    return Nanoid.Generate(Nanoid.Alphabets.UppercaseLettersAndDigits, PasswordResetCodeSize);
  }
  /// <summary>Generates the account activation code</summary>
  /// <returns>The generated code</returns>
  public static string GenerateActivationCode() {
    return Nanoid.Generate(Nanoid.Alphabets.UppercaseLettersAndDigits, ActivationCodeSize);
  }
}