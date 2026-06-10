using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FatClient.dto
{
    [JsonConverter(typeof(CommandInfoConverter))]
    public class CommandInfo
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Title) ? Content : $"[{Title}] {Content}";
        }
    }

    public class CommandInfoConverter : JsonConverter<CommandInfo>
    {
        public override CommandInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new CommandInfo
                {
                    Title = string.Empty,
                    Content = reader.GetString()
                };
            }

            if (reader.TokenType == JsonTokenType.StartObject)
            {
                var commandInfo = new CommandInfo();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        return commandInfo;
                    }

                    if (reader.TokenType == JsonTokenType.PropertyName)
                    {
                        string propertyName = reader.GetString();
                        reader.Read();
                        if (string.Equals(propertyName, nameof(CommandInfo.Title), StringComparison.OrdinalIgnoreCase))
                        {
                            commandInfo.Title = reader.GetString();
                        }
                        else if (string.Equals(propertyName, nameof(CommandInfo.Content), StringComparison.OrdinalIgnoreCase))
                        {
                            commandInfo.Content = reader.GetString();
                        }
                    }
                }
            }

            throw new JsonException("Expected string or object for CommandInfo.");
        }

        public override void Write(Utf8JsonWriter writer, CommandInfo value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Title", value.Title ?? string.Empty);
            writer.WriteString("Content", value.Content ?? string.Empty);
            writer.WriteEndObject();
        }
    }
}
