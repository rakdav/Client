using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClientProject.Model
{
    public class Client
    {
        [JsonPropertyName("clientid")]
        public int Clientid { get; set; }
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; } = null!;
        [JsonPropertyName("surname")]
        public string? Surname { get; set; }
        [JsonPropertyName("lastname")]
        public string Lastname { get; set; } = null!;
        [JsonPropertyName("company")]
        public string Company { get; set; } = null!;
        [JsonPropertyName("phone")]
        public string Phone { get; set; } = null!;
        [JsonPropertyName("city")]
        public string City { get; set; } = null!;
    }
}
