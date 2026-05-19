using System.Text.Json.Serialization;

namespace Models;
public class FactResponse
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    public FactResponse(string text)
    {
        Text = text;
    }

    public FactResponse(){}
}