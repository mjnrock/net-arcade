namespace Arcade.RPG.Lib;

using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;

public class ColorConverter : JsonConverter<Color> {
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer) {
        writer.WriteValue($"#{value.R:X2}{value.G:X2}{value.B:X2}");
    }

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer) {
        string hex = (string)reader.Value;
        Debug.WriteLine($"Reading color: {hex}");
        if(string.IsNullOrEmpty(hex) || !hex.StartsWith("#") || hex.Length != 7) {
            throw new JsonSerializationException("Invalid color format");
        }

        return new Color(
            (byte)Convert.ToByte(hex.Substring(1, 2), 16),
            (byte)Convert.ToByte(hex.Substring(3, 2), 16),
            (byte)Convert.ToByte(hex.Substring(5, 2), 16),
            (byte)255 // Fully opaque
        );
    }
}
