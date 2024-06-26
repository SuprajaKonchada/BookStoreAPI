﻿namespace BookStoreAPI.Models;

/// <summary>
/// Represents JWT (JSON Web Token) settings used for token generation and validation.
/// </summary>
public class JwtSettings
{
    public required string SecretKey { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpiryMinutes { get; set; }
}
