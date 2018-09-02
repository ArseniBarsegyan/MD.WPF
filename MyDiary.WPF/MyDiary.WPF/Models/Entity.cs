using Newtonsoft.Json;

namespace MyDiary.WPF.Models
{
    public class Entity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}