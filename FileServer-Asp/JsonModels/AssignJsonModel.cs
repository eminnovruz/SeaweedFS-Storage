using System.Text.Json.Serialization;

namespace FileServer_Asp.JsonModels;

/// <summary>
/// Model representing the JSON response for an assigned file.
/// </summary>
public class AssignJsonModel
{
    /// <summary>
    /// Gets or sets the file identifier.
    /// </summary>
    [JsonPropertyName("fid")]
    public string Fid { get; set; }

    /// <summary>
    /// Gets or sets the URL for the file.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the public URL for the file.
    /// </summary>
    [JsonPropertyName("publicUrl")]
    public string PublicUrl { get; set; }

    /// <summary>
    /// Gets or sets the count of some associated value.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}
