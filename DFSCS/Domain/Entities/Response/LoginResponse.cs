using API.Models.Common;
using System.Text.Json.Serialization;

namespace Domain.Entities.Response
{
    public class LoginResponse : ApiBaseResponse
    {

        [JsonPropertyOrder(2)]
        public List<Employee> userDetails { get; set; } = new();
        [JsonPropertyOrder(3)]
        public string accessToken { get; set; } = string.Empty;
        [JsonPropertyOrder(4)]
        public int expiresInSeconds { get; set; }
        [JsonPropertyOrder(5)]
        public string tokenType { get; set; } = "Bearer";

    }

}
