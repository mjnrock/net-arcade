namespace Arcade.RPG.Systems.Messaging;

using global::System;
using Arcade.RPG.Lib;
using global::System.Text.Json;

public class Envelope : Identity {
    public string Type { get; set; }
    public Message Message { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Sender { get; set; }
    public Guid? Response { get; set; } = null;

    public Envelope(string type, Message message, string sender, Guid? response = null) {
        this.Type = type;
        this.Message = message;
        this.Sender = sender;
        this.Response = response;
    }

    public static Envelope Create(string type, Message message, string sender, Guid? response = null) {
        return new Envelope(type, message, sender, response);
    }

    public static Envelope FromJson(string json) {
        var envelope = JsonSerializer.Deserialize<Envelope>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });

        envelope.Message = JsonSerializer.Deserialize<Message>(envelope.Message.ToString(), new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true
        });

        return envelope;
    }

    public string ToJson(JsonSerializerOptions? jsonSerializerOptions = null) {
        var options = jsonSerializerOptions ?? new JsonSerializerOptions {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        return JsonSerializer.Serialize(this, options);
    }
}