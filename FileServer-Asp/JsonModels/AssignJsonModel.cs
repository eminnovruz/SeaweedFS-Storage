using System.Text.Json.Serialization;

namespace FileServer_Asp.JsonModels;

public class AssignJsonModel
{
    [JsonPropertyName("fid")]
    public string Fid { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("publicUrl")]
    public string PublicUrl { get; set; }
    [JsonPropertyName("count")]
    public int Count { get; set; }
}
