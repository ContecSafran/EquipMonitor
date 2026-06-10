using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EquipMonitor.dto
{
    [JsonConverter(typeof(CommandInfoConverter))]
    public class CommandInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                return Title;
            }
            if (Content != null && Content.Length > 20)
            {
                return Content.Substring(0, 20) + "...";
            }
            return Content ?? string.Empty;
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
                    Description = string.Empty,
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
                        else if (string.Equals(propertyName, nameof(CommandInfo.Description), StringComparison.OrdinalIgnoreCase))
                        {
                            commandInfo.Description = reader.GetString();
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
            writer.WriteString("Description", value.Description ?? string.Empty);
            writer.WriteString("Content", value.Content ?? string.Empty);
            writer.WriteEndObject();
        }
    }
}
