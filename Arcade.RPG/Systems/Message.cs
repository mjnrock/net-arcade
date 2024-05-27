namespace Arcade.RPG.Systems;

using global::System;
using Arcade.RPG.Lib;

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
}