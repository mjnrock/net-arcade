namespace Arcade.RPG.Systems;

using global::System;
using Arcade.RPG.Lib;
using global::System.Text.Json;

public class Message : Identity {
    public string Type { get; set; }
    public dynamic Payload { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;

    public Message(dynamic type, dynamic payload) {
        /* Cast the type to a string here to make instantiation cleaner */
        this.Type = type.ToString();

        /* Message-dependent payload */
        this.Payload = payload;
    }

    public static Message Create(string type, dynamic payload) {
        return new Message(type, payload);
    }

    public static Message FromJson(string json) {
        var message = JsonSerializer.Deserialize<Message>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });

        message.Payload = JsonSerializer.Deserialize<dynamic>(message.Payload.ToString());

        return message;
    }

    public string ToJson(JsonSerializerOptions? jsonSerializerOptions = null) {
        var options = jsonSerializerOptions ?? new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        this.Payload = JsonSerializer.Serialize<dynamic>(this.Payload, options);

        return JsonSerializer.Serialize(this, options);
    }
}