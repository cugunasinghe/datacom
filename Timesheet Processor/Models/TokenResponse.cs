﻿using System.Text.Json.Serialization;

namespace Timesheet_Processor.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiredIn { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}
