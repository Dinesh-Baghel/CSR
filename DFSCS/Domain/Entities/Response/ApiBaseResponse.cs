
using System.Text.Json.Serialization;

namespace Domain.Entities.Response
{
    public class ApiBaseResponse
    {

        [JsonPropertyOrder(0)]
        public int responseCode { get; set; } = 0;

        [JsonPropertyOrder(1)]
        public string responseMessage { get; set; } = "Success";


    }
}
